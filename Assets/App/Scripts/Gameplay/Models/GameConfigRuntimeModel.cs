using Assets.App.Scripts.Core.MVC.Models;

namespace Assets.App.Scripts.Gameplay.Models
{
	public class GameConfigRuntimeModel : IRuntimeModel
	{
		public GameConfig GameConfig;

		public void Clear()
		{
			GameConfig = null;
		}
	}
}