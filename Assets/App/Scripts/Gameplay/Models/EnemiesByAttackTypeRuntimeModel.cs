using Assets.App.Scripts.Core.MVC.Models;
using Assets.App.Scripts.Gameplay.Units.Enemies.Configs;
using System.Collections.Generic;

namespace Assets.App.Scripts.Gameplay.Models
{
	public class EnemiesByAttackTypeRuntimeModel : IRuntimeModel
	{
		public List<EnemyObjectView> DetectingPlayerEnemies = new();
		public List<EnemyUnit> DetectingPlayerEnemiesData = new();

		public void Clear()
		{
			DetectingPlayerEnemies.Clear();
			DetectingPlayerEnemiesData.Clear();
		}
	}
}