using Assets.App.Scripts.Core.MVC.Models;
using UnityEngine;
using UnityEngine.Pool;

namespace Assets.App.Scripts.Gameplay.Models
{
	public class BulletsRuntimeModel : IRuntimeModel
	{
		public ObjectPool<GameObject> BulletsPool;

		public void Clear()
		{
			BulletsPool.Clear();
		}
	}
}