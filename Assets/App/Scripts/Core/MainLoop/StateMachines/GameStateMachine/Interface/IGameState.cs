using Cysharp.Threading.Tasks;

namespace Assets.App.Scripts.Core.MainLoop.StateMachines.GameStateMachine.Interface
{
	public interface IGameState
	{
		UniTask Enter();
		UniTask Exit();
		void Update();
		void FixedUpdate();
		void LateUpdate();
		UniTask Load();
		UniTask Unload();
	}
}