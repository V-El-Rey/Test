using Assets.App.Scripts.Core.MVC.Controllers;
using Assets.App.Scripts.Core.MVC.Controllers.GameplayControllers;
using Assets.App.Scripts.Core.Services.Services;
using Assets.App.Scripts.Gameplay.Models;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets.App.Scripts.Gameplay.Input
{
	public class GatheringInputController : GameplayController, IUpdatable
	{
		private InputActionsRuntimeModel _inputActionsRuntimeModel;

		private PlayerInputAssetManageService _playerInputService;

		public override async UniTask OnEnter()
		{
			await base.OnEnter();
			_playerInputService = ServiceLocator.Instance.GetService<PlayerInputAssetManageService>();

			if (!GetModelOfType<InputActionsRuntimeModel>(out _inputActionsRuntimeModel))
				await UniTask.FromException(new System.Exception($"Cant init model of type {typeof(InputActionsRuntimeModel)}"));

			if (!TrySetUpPlayerInputActions())
				await UniTask.FromException(new System.Exception("Cant set up input actions"));

			await UniTask.CompletedTask;
		}

		public void OnUpdate()
		{
			UpdateInput();
		}

		private void UpdateInput()
		{
			_inputActionsRuntimeModel.KeyboardInput = _inputActionsRuntimeModel.InputActions["Move"].ReadValue<Vector2>();
		}

		private bool TrySetUpPlayerInputActions()
		{
			if (!_playerInputService.TryGetInputAction("Move", out var wasdAction))
				return false;
			if (!_inputActionsRuntimeModel.InputActions.TryAdd("Move", wasdAction))
				return false;
			return true;
		}
	}
}