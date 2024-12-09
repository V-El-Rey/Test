using Assets.App.Scripts.Gameplay.Units.Enemies.Configs;
using UnityEngine;

[CreateAssetMenu(fileName = "AttackTypeShoot", menuName = "Configs/AttackTypeShoot")]
public class AttackTypeShoot : AttackType
{
	public float DetectionRadius;
}
