using Assets.App.Scripts.Core.Data.AssetsLoader;
using Assets.App.Scripts.Core.MVC.Controllers.GameplayControllers;
using Assets.App.Scripts.Core.Services;
using Assets.App.Scripts.Gameplay.Models;
using Cysharp.Threading.Tasks;
using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.App.Scripts.Gameplay.Units.Enemies.Gameplay
{
	public class BulletsPoolInitializeController : GameplayController
	{
		private ObjectViewAssetLoader _objectViewAssetLoader;
		private BulletsRuntimeModel _bulletsRuntimeModel;

		private EnemyObjectView _bulletView;

		public override async UniTask OnEnter()
		{
			await base.OnEnter();

			_objectViewAssetLoader = ServiceLocator.Instance.GetService<GameDataService>().ObjectViewAssetLoader;

			if (!GetModelOfType<BulletsRuntimeModel>(out _bulletsRuntimeModel))
				await UniTask.FromException(new System.Exception($"Cant init model of type {typeof(BulletsRuntimeModel)}"));

			_bulletView = await _objectViewAssetLoader.LoadAsset(AddressablePath.ENEMY_BULLET) as EnemyObjectView;
			_bulletView.gameObject.SetActive(false);

			_bulletsRuntimeModel.BulletsPool = new UnityEngine.Pool.ObjectPool<GameObject>
				(
					CreatePooledItem,
					OnTakeFromPool,
					OnReturnedToPool,
					OnDestroyPoolObject,
					true,
					10,
					100
				);
		}

		private GameObject CreatePooledItem()
		{
			var item = GameObject.Instantiate(_bulletView.gameObject);
			return item;
		}

		private void OnDestroyPoolObject(GameObject view)
		{
			GameObject.Destroy(view.gameObject);
		}

		private void OnReturnedToPool(GameObject view)
		{
			view.gameObject.SetActive(false);
		}

		private void OnTakeFromPool(GameObject view)
		{
			view.gameObject.SetActive(true);
		}
	}
}