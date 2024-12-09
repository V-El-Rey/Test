using Assets.App.Scripts.Core.MVC.Controllers.GameplayControllers;
using Assets.App.Scripts.Gameplay.Models;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets.App.Scripts.Gameplay.Units.Enemies.Gameplay
{
	public class EnemyShootersAttackingController : GameplayController, IFixedUpdatable
	{
		private ShooterRuntimeModel _shootersRuntimeModel;
		private BulletsRuntimeModel _bulletsRuntimeModel;
		private PlayerRuntimeModel _playerRuntimeModel;

		private float FireRate = 5f;

		public override async UniTask OnEnter()
		{
			await base.OnEnter();

			if (!GetModelOfType(out _shootersRuntimeModel))
				await UniTask.FromException(new System.Exception($"Cant init model of type {typeof(ShooterRuntimeModel)}"));
			if (!GetModelOfType(out _playerRuntimeModel))
				await UniTask.FromException(new System.Exception($"Cant init model of type {typeof(PlayerRuntimeModel)}"));
			if (!GetModelOfType<BulletsRuntimeModel>(out _bulletsRuntimeModel))
				await UniTask.FromException(new System.Exception($"Cant init model of type {typeof(BulletsRuntimeModel)}"));

		}

		public void OnFixedUpdate()
		{
			for (int i = 0; i < _shootersRuntimeModel.Shooters.Count; i++)
			{
				var shooter = _shootersRuntimeModel.Shooters[i];
				var shooterData = _shootersRuntimeModel.ShootersData[i];

				if (shooter.IsAttacking)
				{
					var movementVector = (_playerRuntimeModel.PlayerView.Transform.position - shooter.Transform.position).normalized;

					var rotationVector = Quaternion.LookRotation(movementVector);
					rotationVector = Quaternion.RotateTowards
						(
							shooter.Transform.rotation,
							rotationVector,
							360 * Time.fixedDeltaTime
						);
					shooter.Rigidbody.MoveRotation
						(
							rotationVector
						);
					FireRate -= Time.fixedDeltaTime;
					if(FireRate < 0) 
					{
						var bullet = _bulletsRuntimeModel.BulletsPool.Get();
						bullet.transform.position = shooter.Transform.position + shooter.Transform.forward * 1.5f + shooter.Transform.up * 2f;
						bullet.GetComponent<Rigidbody>().AddForce(movementVector * 1000, ForceMode.Force);
						FireRate = 5f;
					}
				}
			}
		}
	}
}