using System;
using UnityEngine;

public class EnemyObjectView : BaseObjectView
{
	public int WaypointIndex;
	public bool IsMoving;
	public bool IsAttacking;
    public Rigidbody Rigidbody;
	public Action<EnemyObjectView, Collision> OnCollision;

	private void OnCollisionEnter(Collision collision)
	{
		Debug.Log("enemy collided");
		OnCollision?.Invoke(this, collision);
	}
}
