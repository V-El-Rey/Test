using Assets.App.Scripts.Core.MVC.Controllers.GameplayControllers;
using Assets.App.Scripts.Core.Services.Services;
using Assets.App.Scripts.Gameplay.Events;
using Assets.App.Scripts.Gameplay.Models;
using Cysharp.Threading.Tasks;
using System.Linq;
using UnityEngine;

namespace Assets.App.Scripts.Gameplay.Units.Player
{
	public class PlayerDamageHandleController : GameplayController
	{
		private EventsService _eventsService;
		private PlayerRuntimeModel _playerRuntimeModel;
		private GameConfigRuntimeModel _gameConfigRuntimeModel;

		public override async UniTask OnEnter()
		{
			await base.OnEnter();

			_eventsService = ServiceLocator.Instance.GetService<EventsService>();

			if (!GetModelOfType<PlayerRuntimeModel>(out _playerRuntimeModel))
				await UniTask.FromException(new System.Exception($"Cant init model of type {typeof(PlayerRuntimeModel)}"));
			if (!GetModelOfType<GameConfigRuntimeModel>(out _gameConfigRuntimeModel))
				await UniTask.FromException(new System.Exception($"Cant init model of type {typeof(GameConfigRuntimeModel)}"));

			_eventsService.Subscribe<PlayerDamageEvent>(OnPlayerDamagedEventHandler);

			_eventsService.Emit(new EmitHealthValuesEvent()
			{
				FullHealthValue = _gameConfigRuntimeModel.GameConfig.PlayerHealth,
				NewHealthValue = _gameConfigRuntimeModel.GameConfig.PlayerHealth
			});
		}

		public override async UniTask OnExit()
		{
			_eventsService.Unsubscribe<PlayerDamageEvent>(OnPlayerDamagedEventHandler);
			await base.OnExit();
		}

		private void OnPlayerDamagedEventHandler(PlayerDamageEvent dto)
		{
			var patrolAttack = _gameConfigRuntimeModel.GameConfig.EnemyUnits.FirstOrDefault(u => u.AttackType is AttackTypeNone attack);
			var huntAttack = _gameConfigRuntimeModel.GameConfig.EnemyUnits.FirstOrDefault(u => u.AttackType is AttackTypeHunt attack);
			var shootAttack = _gameConfigRuntimeModel.GameConfig.EnemyUnits.FirstOrDefault(u => u.AttackType is AttackTypeShoot attack);

			var emitHealthValuesDto = new EmitHealthValuesEvent() { FullHealthValue = _gameConfigRuntimeModel.GameConfig.PlayerHealth };
			switch (dto.DamagerTag)
			{
				case ("Patrol"):
					emitHealthValuesDto.NewHealthValue = _playerRuntimeModel.Health -= patrolAttack != null ? patrolAttack.AttackType.DamageAmount : 10;
					break;
				case ("Hunter"):
					emitHealthValuesDto.NewHealthValue = _playerRuntimeModel.Health -= huntAttack != null ? huntAttack.AttackType.DamageAmount : 5;
					break;
				case ("Bullet"):
					emitHealthValuesDto.NewHealthValue = _playerRuntimeModel.Health -= shootAttack != null ? shootAttack.AttackType.DamageAmount : 15;
					break;
			}

			_eventsService.Emit(emitHealthValuesDto);
		}
	}
}