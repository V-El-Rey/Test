using System.Collections.Generic;
using UnityEngine;

namespace Assets.App.Scripts.Gameplay.Units.Enemies.Configs
{
	[CreateAssetMenu(fileName = "MovementTypePatrol", menuName = "Configs/MovementTypePatrol")]
	public class MovementTypePatrol : MovementType
	{
		public List<Vector3> Waypoints;
	}
}