using UnityEngine;

namespace Assets.App.Scripts.Gameplay.Events
{
	public struct PlayerDamageEvent : IGameEvent
	{
		public string DamagerTag;
	}
}