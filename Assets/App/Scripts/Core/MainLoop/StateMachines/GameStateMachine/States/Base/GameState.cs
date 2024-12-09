using Assets.App.Scripts.Core.MainLoop.StateMachines.GameStateMachine.Interface;
using Assets.App.Scripts.Core.Services.Services;
using Cysharp.Threading.Tasks;

namespace Assets.App.Scripts.Core.MainLoop.StateMachines.GameStateMachine.States.Base
{
	public abstract class GameState : IGameState
	{
		protected ControllersManager StateControllersManager { get; set; }

		public virtual async UniTask Enter()
		{
			await StateControllersManager.OnEnterControllersExecute();
			UIContextService.Instance.ClearLoadingText();
		}

		public virtual async UniTask Exit()
		{
			await StateControllersManager.OnExitControllersExecute();
			UIContextService.Instance.ClearLoadingText();
		}
		public virtual void Update() { }
		public virtual void FixedUpdate() { }
		public virtual void LateUpdate() { }

		public virtual UniTask Load()
		{
			return UniTask.CompletedTask;
		}

		public virtual UniTask Unload()
		{
			return UniTask.CompletedTask;
		}
	}
}