using Assets.App.Scripts.Core.Data.AssetsLoader;
using Assets.App.Scripts.Core.MVC.Controllers.GameplayControllers;
using Assets.App.Scripts.Core.Services;
using Assets.App.Scripts.Gameplay.Models;
using Cysharp.Threading.Tasks;

namespace Assets.App.Scripts.Gameplay.Units.Enemies.Gameplay
{
	public class EnemiesInitializationController : GameplayController
	{
		private ObjectViewAssetLoader _objectViewAssetLoader;
		private GameConfigRuntimeModel _gameConfigRuntimeModel;

		private PatrolRuntimeModel _patrolRuntimeModel;
		private HunterRuntimeModel _hunterRuntimeModel;
		private ShooterRuntimeModel _shooterRuntimeModel;
		private EnemiesByAttackTypeRuntimeModel _enemiesByAttackTypeRuntimeModel;

		public override async UniTask OnEnter()
		{
			await base.OnEnter();
			_objectViewAssetLoader = ServiceLocator.Instance.GetService<GameDataService>().ObjectViewAssetLoader;

			if (!GetModelOfType<GameConfigRuntimeModel>(out _gameConfigRuntimeModel))
				await UniTask.FromException(new System.Exception($"Cant init model of type {typeof(GameConfigRuntimeModel)}"));

			if (!GetModelOfType<PatrolRuntimeModel>(out _patrolRuntimeModel))
				await UniTask.FromException(new System.Exception($"Cant init model of type {typeof(PatrolRuntimeModel)}"));
			if (!GetModelOfType<HunterRuntimeModel>(out _hunterRuntimeModel))
				await UniTask.FromException(new System.Exception($"Cant init model of type {typeof(HunterRuntimeModel)}"));
			if (!GetModelOfType<ShooterRuntimeModel>(out _shooterRuntimeModel))
				await UniTask.FromException(new System.Exception($"Cant init model of type {typeof(ShooterRuntimeModel)}"));

			if (!GetModelOfType<EnemiesByAttackTypeRuntimeModel>(out _enemiesByAttackTypeRuntimeModel))
				await UniTask.FromException(new System.Exception($"Cant init model of type {typeof(EnemiesByAttackTypeRuntimeModel)}"));

			await InitializeEnemies();
		}

		private async UniTask InitializeEnemies()
		{
			foreach (var enemyConfig in _gameConfigRuntimeModel.GameConfig.EnemyUnits)
			{
				if (enemyConfig.AttackType is AttackTypeNone patroller)
				{
					var enemy = await _objectViewAssetLoader.LoadAsset(AddressablePath.ENEMY_PATROL) as EnemyObjectView;
					enemy.Transform.position = enemyConfig.StartPosition;
					enemy.IsMoving = true;
					enemy.IsAttacking = false;
					enemy.WaypointIndex = 0;
					_patrolRuntimeModel.Patrols.Add(enemy);
					_patrolRuntimeModel.PatrolsData.Add(enemyConfig);
				}
				else if (enemyConfig.AttackType is AttackTypeShoot shooter)
				{
					var enemy = await _objectViewAssetLoader.LoadAsset(AddressablePath.ENEMY_SHOOTER) as EnemyObjectView;
					enemy.Transform.position = enemyConfig.StartPosition;
					enemy.IsMoving = false;
					enemy.IsAttacking = false;
					_shooterRuntimeModel.Shooters.Add(enemy);
					_shooterRuntimeModel.ShootersData.Add(enemyConfig);
					_enemiesByAttackTypeRuntimeModel.DetectingPlayerEnemies.Add(enemy);
					_enemiesByAttackTypeRuntimeModel.DetectingPlayerEnemiesData.Add(enemyConfig);

				}
				else if (enemyConfig.AttackType is AttackTypeHunt hunter)
				{
					var enemy = await _objectViewAssetLoader.LoadAsset(AddressablePath.ENEMY_HUNTER) as EnemyObjectView;
					enemy.Transform.position = enemyConfig.StartPosition;
					enemy.IsMoving = false;
					enemy.IsAttacking = false;
					_hunterRuntimeModel.Hunters.Add(enemy);
					_hunterRuntimeModel.HuntersData.Add(enemyConfig);
					_enemiesByAttackTypeRuntimeModel.DetectingPlayerEnemies.Add(enemy);
					_enemiesByAttackTypeRuntimeModel.DetectingPlayerEnemiesData.Add(enemyConfig);
				}
			}
		}
	}
}