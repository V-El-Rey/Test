using Assets.App.Scripts.Core.MainLoop.StateMachines.GameStateMachine.Interface;
using Assets.App.Scripts.Core.Services.Services;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.App.Scripts.Core.MainLoop.StateMachines.GameStateMachine
{
	public class GameStateMachine
	{
		private Dictionary<Type, IGameState> _registredGameStates = new Dictionary<Type, IGameState>();
		private IGameState _currentState;
		private bool _isTransitioning;

		public void RegisterState<T>(T gameState)
		{
			_registredGameStates[typeof(T)] = (IGameState)gameState;
		}

		public async UniTask ChangeState<T>()
		{
			if (!_registredGameStates.TryGetValue(typeof(T), out IGameState newState))
				return;

			_isTransitioning = true;
			if (_currentState != null)
			{
				UIContextService.Instance.ShowLoadingScreen();
				await _currentState.Exit();
				await _currentState.Unload();
			}

			_currentState = newState;

			if (_currentState != null)
			{
				await _currentState.Load();
				await _currentState.Enter();
				UIContextService.Instance.HideLoadingScreen();
			}
			_isTransitioning = false;
		}

		public void UpdateState()
		{
			if (_isTransitioning) return;
			_currentState.Update();
		}

		public void FixedUpdateState()
		{
			if (_isTransitioning) return;
			_currentState.FixedUpdate();
		}

		public void LateUpdateState()
		{
			if (_isTransitioning) return;
			_currentState.LateUpdate();
		}
	}
}