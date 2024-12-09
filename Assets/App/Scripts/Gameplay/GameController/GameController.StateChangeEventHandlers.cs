using Assets.App.Scripts.Core.Services.Services;
using Assets.App.Scripts.Gameplay.Events;
using Assets.App.Scripts.Gameplay.GameStates;
using System;

namespace Assets.App.Scripts.Core
{
	public partial class GameController
	{
		private void SubscribeToStateChangeEvents()
		{
			_eventsService.Subscribe<StartGameEvent>(OnStartGameEventHandler);
			_eventsService.Subscribe<StartMainMenuEvent>(OnStartMainMenuEventHandler);
		}

		private async void OnStartMainMenuEventHandler(StartMainMenuEvent dto)
		{
			ServiceLocator.Instance.GetService<UIContextService>().ShowLoadingScreen();
			await _stateMachine.ChangeState<MainMenuState>();
		}

		private async void OnStartGameEventHandler(StartGameEvent dto)
		{
			ServiceLocator.Instance.GetService<UIContextService>().ShowLoadingScreen();
			await _stateMachine.ChangeState<GameplayState>();
		}
	}
}

