using Cysharp.Threading.Tasks;

namespace Assets.App.Scripts.Core.MVC.UI
{
	public interface IScreen
	{
		UniTask Enter();
		UniTask Exit();
		void OnUpdateExecute();
	}
}