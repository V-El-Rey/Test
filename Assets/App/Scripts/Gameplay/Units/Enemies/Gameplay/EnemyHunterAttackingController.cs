using Assets.App.Scripts.Core.MVC.Controllers;
using Assets.App.Scripts.Core.MVC.Controllers.GameplayControllers;
using Assets.App.Scripts.Gameplay.Models;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets.App.Scripts.Gameplay.Units.Enemies.Gameplay
{
	public class EnemyHunterAttackingController : GameplayController, IFixedUpdatable
	{
		private HunterRuntimeModel _hunterRuntimeModel;
		private PlayerRuntimeModel _playerRuntimeModel;

		public override async UniTask OnEnter()
		{
			await base.OnEnter();

			if (!GetModelOfType<HunterRuntimeModel>(out _hunterRuntimeModel))
				await UniTask.FromException(new System.Exception($"Cant init model of type {typeof(HunterRuntimeModel)}"));
			if (!GetModelOfType<PlayerRuntimeModel>(out _playerRuntimeModel))
				await UniTask.FromException(new System.Exception($"Cant init model of type {typeof(PlayerRuntimeModel)}"));
		}

		public void OnFixedUpdate()
		{
			for (int i = 0; i < _hunterRuntimeModel.Hunters.Count; i++)
			{
				var hunter = _hunterRuntimeModel.Hunters[i];
				var hunterData = _hunterRuntimeModel.HuntersData[i];

				if (hunter.IsAttacking) 
				{
					var movementVector = (_playerRuntimeModel.PlayerView.Transform.position - hunter.Transform.position).normalized;

					var rotationVector = Quaternion.LookRotation(movementVector);
					rotationVector = Quaternion.RotateTowards
						(
							hunter.Transform.rotation,
							rotationVector,
							360 * Time.fixedDeltaTime
						);

					hunter.Rigidbody.MovePosition
						(
							hunter.Rigidbody.position + movementVector * hunterData.UnitSpeed * Time.fixedDeltaTime
						);
					hunter.Rigidbody.MoveRotation
						(
							rotationVector
						);
				}
			}
		}
	}
}