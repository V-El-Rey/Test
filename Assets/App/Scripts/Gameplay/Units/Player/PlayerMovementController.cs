using Assets.App.Scripts.Core.MVC.Controllers.GameplayControllers;
using Assets.App.Scripts.Gameplay.Models;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets.App.Scripts.Gameplay.Units.Player
{
	public class PlayerMovementController : GameplayController, IFixedUpdatable
	{
		private GameConfigRuntimeModel _gameConfigRuntimeModel;
		private PlayerRuntimeModel _playerRuntimeModel;
		private InputActionsRuntimeModel _inputActionsRuntimeModel;

		public override async UniTask OnEnter()
		{
			await base.OnEnter();

			if (!GetModelOfType<GameConfigRuntimeModel>(out _gameConfigRuntimeModel))
				await UniTask.FromException(new System.Exception($"Cant init model of type {typeof(GameConfigRuntimeModel)}"));
			if (!GetModelOfType<PlayerRuntimeModel>(out _playerRuntimeModel))
				await UniTask.FromException(new System.Exception($"Cant init model of type {typeof(PlayerRuntimeModel)}"));
			if (!GetModelOfType<InputActionsRuntimeModel>(out _inputActionsRuntimeModel))
				await UniTask.FromException(new System.Exception($"Cant init model of type {typeof(InputActionsRuntimeModel)}"));
		}

		public void OnFixedUpdate()
		{
			var movementVector = new Vector3(_inputActionsRuntimeModel.KeyboardInput.x, 0.0f, _inputActionsRuntimeModel.KeyboardInput.y).normalized;

			if (movementVector == Vector3.zero) 
			{
				return;
			}

			var rotationVector = Quaternion.LookRotation(movementVector);
			rotationVector = Quaternion.RotateTowards
				(
					_playerRuntimeModel.PlayerView.Transform.rotation,
					rotationVector,
					360 * Time.fixedDeltaTime
				);

			_playerRuntimeModel.PlayerView.Rigidbody.MovePosition
				(
					_playerRuntimeModel.PlayerView.Rigidbody.position + movementVector * _gameConfigRuntimeModel.GameConfig.PlayerSpeed * Time.fixedDeltaTime
				);
			_playerRuntimeModel.PlayerView.Rigidbody.MoveRotation
				(
					rotationVector
				);
		}
	}
}