using Assets.App.Scripts.Core.MVC.Controllers.GameplayControllers;
using Assets.App.Scripts.Gameplay.Models;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets.App.Scripts.Gameplay.Units.Enemies.Gameplay
{
	public class EnemiesCollisionsController : GameplayController
	{
		private PatrolRuntimeModel _patrolRuntimeModel;
		private HunterRuntimeModel _hunterRuntimeModel;

		public override async UniTask OnEnter()
		{
			await base.OnEnter();

			if (!GetModelOfType<PatrolRuntimeModel>(out _patrolRuntimeModel))
				await UniTask.FromException(new System.Exception($"Cant init model of type {typeof(PatrolRuntimeModel)}"));
			if (!GetModelOfType<HunterRuntimeModel>(out _hunterRuntimeModel))
				await UniTask.FromException(new System.Exception($"Cant init model of type {typeof(HunterRuntimeModel)}"));

			foreach (var patroller in _patrolRuntimeModel.Patrols)
			{
				patroller.OnCollision += OnEnemyCollisionHandler;
			}
			foreach (var hunter in _hunterRuntimeModel.Hunters)
			{
				hunter.OnCollision += OnEnemyCollisionHandler;
			}
		}

		public override UniTask OnExit()
		{
			foreach (var patroller in _patrolRuntimeModel.Patrols)
			{
				patroller.OnCollision -= OnEnemyCollisionHandler;
			}
			foreach (var hunter in _hunterRuntimeModel.Hunters)
			{
				hunter.OnCollision -= OnEnemyCollisionHandler;
			}
			return base.OnExit();
		}


		private void OnEnemyCollisionHandler(EnemyObjectView enemy, Collision collision)
		{
			if (collision.transform.CompareTag("Player"))
				enemy.gameObject.SetActive(false);
		}
	}
}