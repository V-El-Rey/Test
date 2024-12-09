using Assets.App.Scripts.Core.MVC.Models;
using Assets.App.Scripts.Gameplay.Units.Enemies.Configs;
using System.Collections.Generic;

namespace Assets.App.Scripts.Gameplay.Models
{
	public class ShooterRuntimeModel : IRuntimeModel
	{
		public List<EnemyObjectView> Shooters = new();
		public List<EnemyUnit> ShootersData = new();

		public void Clear()
		{
			Shooters.Clear();
			ShootersData.Clear();
		}
	}
}