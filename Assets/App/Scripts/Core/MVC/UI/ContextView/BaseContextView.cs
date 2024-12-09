using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

namespace Assets.App.Scripts.Core.MVC.UI.ContextView
{
	public class BaseContextView : MonoBehaviour, IContextView
	{
		public VisualElement uiRoot { get; set; }
		public bool IsActive { get => gameObject.activeInHierarchy; set => gameObject.SetActive(value); }

		public BaseContextView()
		{
		}

		public virtual UniTask<bool> Activate()
		{
			IsActive = true;
			return UniTask.FromResult(true);
		}

		public virtual UniTask<bool> Deactivate()
		{
			IsActive = false;
			return UniTask.FromResult(true);
		}
	}
}