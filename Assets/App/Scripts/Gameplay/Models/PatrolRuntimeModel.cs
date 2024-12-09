using Assets.App.Scripts.Core.MVC.Models;
using Assets.App.Scripts.Gameplay.Units.Enemies.Configs;
using System.Collections.Generic;

namespace Assets.App.Scripts.Gameplay.Models
{
	public class PatrolRuntimeModel : IRuntimeModel
	{
		public List<EnemyObjectView> Patrols = new();
		public List<EnemyUnit> PatrolsData = new();

		public void Clear()
		{
			Patrols.Clear();
			PatrolsData.Clear();
		}
	}
}