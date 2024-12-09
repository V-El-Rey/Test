using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.App.Scripts.Gameplay.UI.Views
{
	public class MainMenuContextView : MonoBehaviour, IContextView
	{
		public Button StartGameButton;
		public bool IsActive { get => gameObject.activeInHierarchy; set => gameObject.SetActive(value); }

		public UniTask<bool> Activate()
		{
			IsActive = true;
			return UniTask.FromResult(true);
		}

		public UniTask<bool> Deactivate()
		{
			IsActive = false;
			return UniTask.FromResult(true);
		}
	}
}