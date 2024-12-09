using Assets.App.Scripts.Core.MVC.Models;
using Assets.App.Scripts.Gameplay.Units.Player;

namespace Assets.App.Scripts.Gameplay.Models
{
	public class PlayerRuntimeModel : IRuntimeModel
	{
		public PlayerObjectView PlayerView;
		public float Health;

		public void Clear()
		{
			PlayerView = null;
		}
	}
}