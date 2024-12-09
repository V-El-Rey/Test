using Assets.App.Scripts.Core.Data.AssetsLoader;
using Assets.App.Scripts.Core.MVC.Controllers.GameplayControllers;
using Assets.App.Scripts.Core.Services;
using Assets.App.Scripts.Core.Services.Services;
using Assets.App.Scripts.Gameplay.Models;
using Assets.App.Scripts.Gameplay.Units.Player;
using Cysharp.Threading.Tasks;
using Unity.Cinemachine;
using UnityEngine;

namespace Assets.App.Scripts.Gameplay.Units.Camera
{
	public class CameraInitializationController : GameplayController
	{
		private CameraService _cameraService;

		private PlayerRuntimeModel _playerViewRuntimeModel;
		private ObjectViewAssetLoader _objectViewAssetLoader;

		private CinemachineCamera _cameraObject;
		private BaseObjectView _cam;

		public override async UniTask OnEnter()
		{
			await base.OnEnter();
			_cameraService = ServiceLocator.Instance.GetService<CameraService>();
			_objectViewAssetLoader = ServiceLocator.Instance.GetService<GameDataService>().ObjectViewAssetLoader;

			if (!GetModelOfType<PlayerRuntimeModel>(out _playerViewRuntimeModel))
				await UniTask.FromException(new System.Exception($"Cant init model of type {typeof(PlayerRuntimeModel)}"));

			_cam = await _objectViewAssetLoader.LoadAsset(AddressablePath.PLAYER_CAMERA, _cameraService.CameraRoot);
			_cameraObject = _cam.GetComponent<CinemachineCamera>();

			_cameraObject.Follow = _playerViewRuntimeModel.PlayerView.Transform;
		}

		public override UniTask OnExit()
		{
			GameObject.Destroy(_cam.gameObject);
			return base.OnExit();
		}
	}
}