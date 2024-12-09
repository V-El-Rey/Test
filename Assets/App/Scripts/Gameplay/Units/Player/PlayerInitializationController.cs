using Assets.App.Scripts.Core.Data.AssetsLoader;
using Assets.App.Scripts.Core.MVC.Controllers.GameplayControllers;
using Assets.App.Scripts.Core.Services;
using Assets.App.Scripts.Gameplay.Models;
using Cysharp.Threading.Tasks;

namespace Assets.App.Scripts.Gameplay.Units.Player
{
	public class PlayerInitializationController : GameplayController
	{
		private GameConfigRuntimeModel _gameConfigRuntimeModel;
		private PlayerRuntimeModel _playerRuntimeModel;
		private ObjectViewAssetLoader _objectViewAssetLoader;

		public PlayerInitializationController()
		{
		}

		public override async UniTask OnEnter()
		{
			await base.OnEnter();
			_objectViewAssetLoader = ServiceLocator.Instance.GetService<GameDataService>().ObjectViewAssetLoader;

			if (!GetModelOfType<GameConfigRuntimeModel>(out _gameConfigRuntimeModel))
				await UniTask.FromException(new System.Exception($"Cant init model of type {typeof(GameConfigRuntimeModel)}"));
			if (!GetModelOfType<PlayerRuntimeModel>(out _playerRuntimeModel))
				await UniTask.FromException(new System.Exception($"Cant init model of type {typeof(PlayerRuntimeModel)}"));

			_playerRuntimeModel.PlayerView = await _objectViewAssetLoader.LoadAsset(AddressablePath.PLAYER) as PlayerObjectView;
			_playerRuntimeModel.PlayerView.Transform.position = _gameConfigRuntimeModel.GameConfig.PlayerStartPosition;
			_playerRuntimeModel.Health = _gameConfigRuntimeModel.GameConfig.PlayerHealth;
		}
	}
}