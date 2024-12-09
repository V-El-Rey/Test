using System;
using UnityEngine;

namespace Assets.App.Scripts.Gameplay.Units.Enemies.Configs
{
	[Serializable]
	public class EnemyUnit
	{
		public Vector3 StartPosition;

		public AttackType AttackType;
		public MovementType MovementType;

		public float UnitSpeed;
	}
}