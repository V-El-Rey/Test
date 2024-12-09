using Assets.App.Scripts.Core.MVC.Controllers;
using Assets.App.Scripts.Core.MVC.Controllers.GameplayControllers;
using Assets.App.Scripts.Gameplay.Models;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets.App.Scripts.Gameplay.Units.Enemies.Gameplay
{
	public class EnemyPlayerDetectingController : GameplayController, IUpdatable
	{
		private EnemiesByAttackTypeRuntimeModel _enemiesByAttackRuntimeModel;
		private PlayerRuntimeModel _playerRuntimeModel;

		public override async UniTask OnEnter()
		{
			await base.OnEnter();

			if (!GetModelOfType<EnemiesByAttackTypeRuntimeModel>(out _enemiesByAttackRuntimeModel))
				await UniTask.FromException(new System.Exception($"Cant init model of type {typeof(EnemiesByAttackTypeRuntimeModel)}"));
			if (!GetModelOfType<PlayerRuntimeModel>(out _playerRuntimeModel))
				await UniTask.FromException(new System.Exception($"Cant init model of type {typeof(PlayerRuntimeModel)}"));
		}

		public void OnUpdate()
		{
			for (int i = 0; i < _enemiesByAttackRuntimeModel.DetectingPlayerEnemies.Count; i++)
			{
				var enemy = _enemiesByAttackRuntimeModel.DetectingPlayerEnemies[i];
				var enemyData = _enemiesByAttackRuntimeModel.DetectingPlayerEnemiesData[i];

				var distance = Vector3.Distance(_playerRuntimeModel.PlayerView.Transform.position, enemy.Transform.position);
				if (enemyData.AttackType is AttackTypeHunt hunt)
				{
					if (distance < hunt.DetectionRadius) 
					{
						enemy.IsAttacking = true;
						enemy.IsMoving = false;
					}
					else
					{
						enemy.IsAttacking = false;
						enemy.IsMoving = true;
					}
				}
				if (enemyData.AttackType is AttackTypeShoot shoot)
				{
					if (distance < shoot.DetectionRadius) 
					{
						enemy.IsAttacking = true;
						enemy.IsMoving = false;
					}
					else 
					{
						enemy.IsAttacking = false;
						enemy.IsMoving = true;
					}
				}
			}
	}
}
}