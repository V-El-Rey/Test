using Assets.App.Scripts.Core.MVC.Controllers.GameplayControllers;
using Assets.App.Scripts.Gameplay.Models;
using Assets.App.Scripts.Gameplay.Units.Enemies.Configs;
using Cysharp.Threading.Tasks;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

namespace Assets.App.Scripts.Gameplay.Units.Enemies.Gameplay
{
	public class EnemyPatrollersMovementController : GameplayController, IFixedUpdatable
	{
		private PatrolRuntimeModel _patrolRuntimeModel;
		private GameConfigRuntimeModel _gameConfigRuntimeModel;

		public override async UniTask OnEnter()
		{
			await base.OnEnter();

			if (!GetModelOfType<PatrolRuntimeModel>(out _patrolRuntimeModel))
				await UniTask.FromException(new System.Exception($"Cant init model of type {typeof(PatrolRuntimeModel)}"));

			if (!GetModelOfType<GameConfigRuntimeModel>(out _gameConfigRuntimeModel))
				await UniTask.FromException(new System.Exception($"Cant init model of type {typeof(GameConfigRuntimeModel)}"));
		}

		public void OnFixedUpdate()
		{
			for(int i = 0; i < _patrolRuntimeModel.Patrols.Count; i++) 
			{
				var patroller = _patrolRuntimeModel.Patrols[i];
				var patrollerData = _patrolRuntimeModel.PatrolsData[i];

				if (!patroller.IsMoving)
					continue;
				var movementVector = (((MovementTypePatrol)patrollerData.MovementType).Waypoints[patroller.WaypointIndex] - patroller.Transform.position).normalized;

				var distance = Vector3.Distance(patroller.Transform.position, ((MovementTypePatrol)patrollerData.MovementType).Waypoints[patroller.WaypointIndex]);
				var rotationVector = Quaternion.LookRotation(movementVector);
				rotationVector = Quaternion.RotateTowards
					(
						patroller.Transform.rotation,
						rotationVector,
						360 * Time.fixedDeltaTime
					);

				patroller.Rigidbody.MovePosition
				(
					patroller.Rigidbody.position + movementVector * patrollerData.UnitSpeed * Time.fixedDeltaTime
				);
				patroller.Rigidbody.MoveRotation
				(
					rotationVector
				);

				if (distance <= 0.05f) 
				{
					patroller.WaypointIndex++;
					if (patroller.WaypointIndex >= ((MovementTypePatrol)patrollerData.MovementType).Waypoints.Count)
						patroller.WaypointIndex = 0;
				}

			}
		}
	}
}