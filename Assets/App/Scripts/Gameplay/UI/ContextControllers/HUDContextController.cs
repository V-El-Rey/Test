using Assets.App.Scripts.Core.Services.Services;
using Assets.App.Scripts.Gameplay.Events;
using Assets.App.Scripts.Gameplay.Models;
using Assets.App.Scripts.Gameplay.UI.Views;
using Cysharp.Threading.Tasks;

namespace Assets.App.Scripts.Gameplay.UI.ContextControllers
{
	public class HUDContextController : ContextController<HUDContextView>
	{
		private EventsService _eventService;
		private PlayerRuntimeModel _playerRuntimeModel;

		public override async UniTask OnEnter()
		{
			await base.OnEnter();
			_eventService = ServiceLocator.Instance.GetService<EventsService>();
			_eventService.Subscribe<EmitHealthValuesEvent>(OnHealthValuesEmittedHandler);
		}

		public override async UniTask OnExit()
		{
			_eventService.Unsubscribe<EmitHealthValuesEvent>(OnHealthValuesEmittedHandler);
			await base.OnExit();
		}

		private void OnHealthValuesEmittedHandler(EmitHealthValuesEvent dto)
		{
			View.FillerImage.fillAmount = dto.NewHealthValue / dto.FullHealthValue;
		}
	}
}