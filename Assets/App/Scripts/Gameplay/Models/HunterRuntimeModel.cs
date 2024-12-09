using Assets.App.Scripts.Core.MVC.Models;
using Assets.App.Scripts.Gameplay.Units.Enemies.Configs;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.App.Scripts.Gameplay.Models
{
	public class HunterRuntimeModel : IRuntimeModel
	{
		public List<EnemyObjectView> Hunters = new();
		public List<EnemyUnit> HuntersData = new();

		public void Clear()
		{
			Hunters.Clear();
			HuntersData.Clear();
		}
	}
}