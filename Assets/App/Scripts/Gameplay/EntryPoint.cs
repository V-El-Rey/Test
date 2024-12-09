using Assets.App.Scripts.Core;
using Assets.App.Scripts.Core.Data.AssetsLoader;
using Assets.App.Scripts.Core.Services;
using Assets.App.Scripts.Core.Services.Services;
using UnityEngine;
using UnityEngine.ResourceManagement.ResourceProviders;

public class EntryPoint : MonoBehaviour
{
	// Start is called before the first frame update
	private void Start()
	{
		var modelsService = new ModelsService();
		ServiceLocator.Instance.Register(modelsService);


		var eventsService = new EventsService();
		ServiceLocator.Instance.Register(eventsService);

		var uiContextService = UIContextService.Instance;
		var gameDataService = new GameDataService();

		gameDataService.AssetLoader = new AddressableAssetLoader<GameObject>();
		gameDataService.ConfigsLoader = new AddressableAssetLoader<ScriptableObject>();
		gameDataService.SceneLoader = new SceneAssetLoader<SceneInstance>();
		gameDataService.ObjectViewAssetLoader = new ObjectViewAssetLoader();
		gameDataService.ContextLoader = new ContextsAssetLoader(uiContextService.UIRoot);

		ServiceLocator.Instance.Register(gameDataService);
		ServiceLocator.Instance.Register(uiContextService);

		var sceneLoadingService = new ScenesLoadingService();
		ServiceLocator.Instance.Register(sceneLoadingService);

		ServiceLocator.Instance.Register(CameraService.Instance);
		PlayerInputAssetManageService.Instance.InitializeManager();
		ServiceLocator.Instance.Register(PlayerInputAssetManageService.Instance);

		GameController.Instance.Initialize();
	}
}
