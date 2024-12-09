using Assets.App.Scripts.Core.MainLoop.StateMachines.GameStateMachine.States.Base;
using Assets.App.Scripts.Core.Services.Services;
using Assets.App.Scripts.Gameplay.UI.ContextControllers;
using Cysharp.Threading.Tasks;

namespace Assets.App.Scripts.Gameplay.GameStates
{
	public class MainMenuState : GameState
	{
		private ScenesLoadingService _loadingService;
		private EventsService _eventsService;

		public MainMenuState()
		{
			_loadingService = ServiceLocator.Instance.GetService<ScenesLoadingService>();
			_eventsService = ServiceLocator.Instance.GetService<EventsService>();

			StateControllersManager = new ControllersManager();
			StateControllersManager.AddController(new MainMenuContextController());
		}

		public override async UniTask Load()
		{
			await base.Load();
			await _loadingService.LoadMainMenu();
			PlayerInputAssetManageService.Instance.EnableInput();
		}

		public override async UniTask Unload()
		{
			await base.Unload();
			PlayerInputAssetManageService.Instance.DisableInput();
			await _loadingService.UnloadCurrentScene();
		}
	}
}