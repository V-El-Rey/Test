using System;
using UnityEngine;

namespace Assets.App.Scripts.Gameplay.Units.Player
{
	public class PlayerObjectView : BaseObjectView
	{
		public Action<Collision> OnCollision;
		public Rigidbody Rigidbody;

		private void OnCollisionEnter(Collision collision)
		{
			OnCollision?.Invoke(collision);
		}
	}
}