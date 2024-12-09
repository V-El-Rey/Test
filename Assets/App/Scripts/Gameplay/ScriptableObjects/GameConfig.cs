using Assets.App.Scripts.Gameplay.Units.Enemies.Configs;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameConfig", menuName = "Configs/GameConfig")]
public class GameConfig : ScriptableObject
{
	public string EnvironmentAssetDataPath;
	public Vector3 PlayerStartPosition;
	public float PlayerSpeed;
	public float PlayerHealth;

	public List<EnemyUnit> EnemyUnits;
}
