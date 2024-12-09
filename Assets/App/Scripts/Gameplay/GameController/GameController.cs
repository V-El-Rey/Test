using Assets.App.Scripts.Core.MainLoop;
using Assets.App.Scripts.Core.MainLoop.StateMachines.GameStateMachine;
using Assets.App.Scripts.Core.Services.Services;
using Assets.App.Scripts.Gameplay.GameStates;
using Assets.App.Scripts.Gameplay.Models;

namespace Assets.App.Scripts.Core
{
	public partial class GameController : MonoSingleton<GameController>
	{
		private GameStateMachine _stateMachine;
		private EventsService _eventsService;
		private ModelsService _modelsService;

		public async void Initialize()
		{
			_stateMachine = new GameStateMachine();
			_stateMachine.RegisterState(new MainMenuState());
			_stateMachine.RegisterState(new GameplayState());

			_eventsService = ServiceLocator.Instance.GetService<EventsService>();
			_modelsService = ServiceLocator.Instance.GetService<ModelsService>();

			_modelsService.RegisterModel(new GameConfigRuntimeModel());
			_modelsService.RegisterModel(new PlayerRuntimeModel());
			_modelsService.RegisterModel(new InputActionsRuntimeModel());
			_modelsService.RegisterModel(new PatrolRuntimeModel());
			_modelsService.RegisterModel(new HunterRuntimeModel());
			_modelsService.RegisterModel(new ShooterRuntimeModel());
			_modelsService.RegisterModel(new EnemiesByAttackTypeRuntimeModel());
			_modelsService.RegisterModel(new BulletsRuntimeModel());

			SubscribeToStateChangeEvents();

			await _stateMachine.ChangeState<MainMenuState>();
		}

		private void Update()
		{
			_stateMachine.UpdateState();
		}

		private void FixedUpdate()
		{
			_stateMachine.FixedUpdateState();
		}

		private void LateUpdate()
		{
			_stateMachine.LateUpdateState();
		}
	}
}