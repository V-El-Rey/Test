using Assets.App.Scripts.Core.MVC.Controllers.GameplayControllers;
using Assets.App.Scripts.Core.Services.Services;
using Assets.App.Scripts.Gameplay.Events;
using Assets.App.Scripts.Gameplay.Models;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets.App.Scripts.Gameplay.Units.Player
{
	public class PlayerCollisionHandleController : GameplayController
	{
		private EventsService _eventsService;
		private PlayerRuntimeModel _playerRuntimeModel;
		private BulletsRuntimeModel _bulletsRuntimeModel;

		public override async UniTask OnEnter()
		{
			await base.OnEnter();

			_eventsService = ServiceLocator.Instance.GetService<EventsService>();

			if (!GetModelOfType<PlayerRuntimeModel>(out _playerRuntimeModel))
				await UniTask.FromException(new System.Exception($"Cant init model of type {typeof(PlayerRuntimeModel)}"));
			if (!GetModelOfType<BulletsRuntimeModel>(out _bulletsRuntimeModel))
				await UniTask.FromException(new System.Exception($"Cant init model of type {typeof(BulletsRuntimeModel)}"));

			_playerRuntimeModel.PlayerView.OnCollision += OnPlayerCollisionHandler;
		}

		public override UniTask OnExit()
		{
			_playerRuntimeModel.PlayerView.OnCollision -= OnPlayerCollisionHandler;
			return base.OnExit();
		}

		private void OnPlayerCollisionHandler(Collision collision)
		{
			if (collision.transform.CompareTag("Untagged"))
				return;
			if (collision.transform.CompareTag("Bullet"))
				_bulletsRuntimeModel.BulletsPool.Release(collision.gameObject);
				

			_eventsService.Emit(new PlayerDamageEvent() { DamagerTag = collision.transform.tag });
		}
	}
}