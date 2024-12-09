using Assets.App.Scripts.Core.MVC.Controllers.GameplayControllers;
using Assets.App.Scripts.Core.Services.Services;
using Assets.App.Scripts.Gameplay.Events;
using Assets.App.Scripts.Gameplay.Models;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets.App.Scripts.Gameplay.Units.Player
{
	public class PlayerDeathController : GameplayController
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

			_eventsService.Subscribe<EmitHealthValuesEvent>(OnHealthValuesEmittedEventHandler);
		}


		public override async UniTask OnExit()
		{
			_eventsService.Unsubscribe<EmitHealthValuesEvent>(OnHealthValuesEmittedEventHandler);
			await base.OnExit();
		}

		private void OnHealthValuesEmittedEventHandler(EmitHealthValuesEvent dto)
		{
			if (dto.NewHealthValue <= 0.0f)
			{
				_eventsService.Emit(new StartMainMenuEvent());
			}
		}
	}
}