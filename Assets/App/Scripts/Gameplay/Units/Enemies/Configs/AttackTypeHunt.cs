using Assets.App.Scripts.Gameplay.Units.Enemies.Configs;
using UnityEngine;

[CreateAssetMenu(fileName = "AttackTypeHunt", menuName = "Configs/AttackTypeHunt")]
public class AttackTypeHunt : AttackType
{
	public float DetectionRadius;
}
