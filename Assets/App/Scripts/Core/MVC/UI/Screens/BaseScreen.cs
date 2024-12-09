using Assets.App.Scripts.Core.MVC.Controllers;
using Cysharp.Threading.Tasks;

namespace Assets.App.Scripts.Core.MVC.UI
{
	public class BaseScreen : IScreen
	{
		public BaseScreen()
		{
			ControllersManager = new ControllersManager();
		}

		private ControllersManager ControllersManager { get; set; }

		public async virtual UniTask Enter()
		{
			await ControllersManager.OnEnterControllersExecute();
		}

		public async virtual UniTask Exit()
		{
			await ControllersManager.OnExitControllersExecute();
		}

		public virtual void OnUpdateExecute()
		{
			ControllersManager.OnUpdateControllerExecute();
		}

		protected void AddController(IBaseController controller)
		{
			ControllersManager.AddController(controller);
		}
	}
}