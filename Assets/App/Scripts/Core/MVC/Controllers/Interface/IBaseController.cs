using Cysharp.Threading.Tasks;

namespace Assets.App.Scripts.Core.MVC.Controllers
{
	public interface IBaseController
	{
		UniTask OnEnter();
		UniTask OnExit();
	}
}