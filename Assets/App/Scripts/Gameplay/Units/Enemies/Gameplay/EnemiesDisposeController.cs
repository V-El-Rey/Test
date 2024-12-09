using Assets.App.Scripts.Core.Data.AssetsLoader;
using Assets.App.Scripts.Core.MVC.Controllers.GameplayControllers;
using Assets.App.Scripts.Core.Services;
using Assets.App.Scripts.Gameplay.Models;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets.App.Scripts.Gameplay.Units.Enemies.Gameplay
{
	public class EnemiesDisposeController : GameplayController
	{

		private PatrolRuntimeModel _patrolRuntimeModel;
		private HunterRuntimeModel _hunterRuntimeModel;
		private ShooterRuntimeModel _shooterRuntimeModel;

		public override async UniTask OnEnter()
		{
			await base.OnEnter();

			if (!GetModelOfType<PatrolRuntimeModel>(out _patrolRuntimeModel))
				await UniTask.FromException(new System.Exception($"Cant init model of type {typeof(PatrolRuntimeModel)}"));
			if (!GetModelOfType<HunterRuntimeModel>(out _hunterRuntimeModel))
				await UniTask.FromException(new System.Exception($"Cant init model of type {typeof(HunterRuntimeModel)}"));
			if (!GetModelOfType<ShooterRuntimeModel>(out _shooterRuntimeModel))
				await UniTask.FromException(new System.Exception($"Cant init model of type {typeof(ShooterRuntimeModel)}"));
		}

		public override UniTask OnExit()
		{
			foreach(var patroller in _patrolRuntimeModel.Patrols) 
			{
				GameObject.Destroy(patroller.gameObject);
			}
			foreach(var hunter in _hunterRuntimeModel.Hunters) 
			{
				GameObject.Destroy(hunter.gameObject);
			}
			foreach(var shooter in _shooterRuntimeModel.Shooters) 
			{
				GameObject.Destroy(shooter.gameObject);
			}
			return base.OnExit();
		}

	}
}