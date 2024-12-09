using Assets.App.Scripts.Core.MVC.Controllers.GameplayControllers;
using Assets.App.Scripts.Core.Services;
using Assets.App.Scripts.Core.Services.Services;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class ContextController<T> : IContextController<T> where T : IContextView
{
	public T View;
	private GameDataService _gameDataService;
	private UIContextService _uiContextService;

	public ContextController() 
	{
		_gameDataService = ServiceLocator.Instance.GetService<GameDataService>();
		_uiContextService = ServiceLocator.Instance.GetService<UIContextService>();
	}

	public async virtual UniTask OnEnter()
	{
		if(!_uiContextService.TryGetLoadedView(typeof(T), out var desiredView)) 
		{
			var loadedView  = await _gameDataService.ContextLoader.LoadAsset(typeof(T).Name);
			_uiContextService.RegisterView(loadedView);
			View = (T)loadedView;
		}
		else 
		{
			View = (T)desiredView;		
		}
		

		if (View != null) 
		{
			var complete = await View.Activate();
			await UniTask.WaitUntil(() => complete);
		}
	}

	public async virtual UniTask OnExit()
	{
		if(View != null) 
		{
			var complete = await View.Deactivate();
			await UniTask.WaitUntil(() => complete);
		}
	}
}
