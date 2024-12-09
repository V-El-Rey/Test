using Assets.App.Scripts.Core.Services.Services;
using Assets.App.Scripts.Gameplay.Events;
using Assets.App.Scripts.Gameplay.UI.Views;
using Cysharp.Threading.Tasks;

namespace Assets.App.Scripts.Gameplay.UI.ContextControllers
{
	public class MainMenuContextController : ContextController<MainMenuContextView>
	{
		private EventsService _eventsService;

		public MainMenuContextController() : base()
		{
			_eventsService = ServiceLocator.Instance.GetService<EventsService>();
		}

		public override async UniTask OnEnter()
		{
			await base.OnEnter();
			View.StartGameButton.onClick.AddListener(OnStartGameButtonClickedHandler);
		}

		public override async UniTask OnExit()
		{
			View.StartGameButton.onClick.RemoveAllListeners();
			await base.OnExit();
		}

		private void OnStartGameButtonClickedHandler()
		{
			_eventsService.Emit(new StartGameEvent());
		}
	}
}