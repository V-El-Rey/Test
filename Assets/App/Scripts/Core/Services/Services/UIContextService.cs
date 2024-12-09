using Assets.App.Scripts.Core.MainLoop;
using Assets.App.Scripts.Core.MVC.UI.ContextView;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Assets.App.Scripts.Core.Services.Services
{
	public class UIContextService : MonoSingleton<UIContextService>
	{
		public Transform UIRoot;
		public TMP_Text LoadingScreenText;
		public BaseContextView UILoadingScreen;

		private Dictionary<Type, IContextView> _contextViews = new();

		public Dictionary<Type, IContextView> ViewsLoaded = new();

		public void SetLoadingText(string value) => LoadingScreenText.text = value;
		public void ClearLoadingText() => LoadingScreenText.text = string.Empty;

		public async void ShowLoadingScreen(string initialText = "")
		{
			await UILoadingScreen.Activate();
			SetLoadingText(initialText);
		}

		public async void HideLoadingScreen()
		{
			ClearLoadingText();
			await UILoadingScreen.Deactivate();
		}

		public bool TryGetLoadedView(Type viewType, out IContextView view)
		{
			return _contextViews.TryGetValue(viewType, out view);
		}

		public bool RegisterView(IContextView value)
		{
			if (!TryRegisterView(value))
			{
				Debug.LogWarning($"View of type {value.GetType()} already registred!");
				return false;
			}
			return true;
		}

		private bool TryRegisterView(IContextView view)
		{
			return _contextViews.TryAdd(view.GetType(), view);
		}
	}
}