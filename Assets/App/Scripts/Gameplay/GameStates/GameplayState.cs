using Assets.App.Scripts.Core.MainLoop.StateMachines.GameStateMachine.States.Base;
using Assets.App.Scripts.Core.Services;
using Assets.App.Scripts.Core.Services.Services;
using Assets.App.Scripts.Gameplay.Input;
using Assets.App.Scripts.Gameplay.Models;
using Assets.App.Scripts.Gameplay.UI.ContextControllers;
using Assets.App.Scripts.Gameplay.Units.Camera;
using Assets.App.Scripts.Gameplay.Units.Enemies.Gameplay;
using Assets.App.Scripts.Gameplay.Units.Player;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class GameplayState : GameState
{
	private ScenesLoadingService _loadingService;
	private GameDataService _gameDataService;
	private ModelsService _modelsService;
	private EventsService _eventsService;

	public GameplayState()
	{
		_loadingService = ServiceLocator.Instance.GetService<ScenesLoadingService>();
		_gameDataService = ServiceLocator.Instance.GetService<GameDataService>();
		_eventsService = ServiceLocator.Instance.GetService<EventsService>();
		_modelsService = ServiceLocator.Instance.GetService<ModelsService>();

		StateControllersManager = new ControllersManager();
		StateControllersManager.AddController(new HUDContextController());

		StateControllersManager.AddController(new PlayerInitializationController());
		StateControllersManager.AddController(new CameraInitializationController());
		StateControllersManager.AddController(new EnemiesInitializationController());
		StateControllersManager.AddController(new BulletsPoolInitializeController());

		StateControllersManager.AddController(new GatheringInputController());
		StateControllersManager.AddController(new PlayerMovementController());
		StateControllersManager.AddController(new PlayerCollisionHandleController());
		StateControllersManager.AddController(new PlayerDamageHandleController());
		StateControllersManager.AddController(new PlayerDeathController());
		StateControllersManager.AddController(new PlayerDisposeController());

		StateControllersManager.AddController(new EnemiesCollisionsController());
		StateControllersManager.AddController(new EnemyPatrollersMovementController());
		StateControllersManager.AddController(new EnemyPlayerDetectingController());
		StateControllersManager.AddController(new EnemyHunterAttackingController());
		StateControllersManager.AddController(new EnemyShootersAttackingController());

		StateControllersManager.AddController(new EnemiesDisposeController());
	}

	public override async UniTask Exit()
	{
		await base.Exit();
		_modelsService.ClearModels();
	}

	public override async UniTask Load()
	{
		var gameConfig = await _gameDataService.ConfigsLoader.LoadAsset(AddressablePath.CONFIG_MAIN_CONFIG) as GameConfig;

		if(!_modelsService.TryGetModel<GameConfigRuntimeModel>(out var configModel)) 
		{
			Debug.LogError($"Cant load model {typeof(GameConfigRuntimeModel)}");
			return;
		}
		else 
		{
			configModel.GameConfig = gameConfig;
		}

		await base.Load();
		await _loadingService.LoadGameScene();
		await _loadingService.LoadAdditional(configModel.GameConfig.EnvironmentAssetDataPath);
		PlayerInputAssetManageService.Instance.EnableInput();
	}

	public override async UniTask Unload()
	{
		await base.Unload();
		PlayerInputAssetManageService.Instance.DisableInput();
		await _loadingService.UnloadCurrentScene();
	}

	public override void Update()
	{
		base.Update();
		StateControllersManager.OnUpdateControllerExecute();
	}

	public override void FixedUpdate()
	{
		base.FixedUpdate();
		StateControllersManager.OnFixedUpdateControllersExecute();
	}
}
