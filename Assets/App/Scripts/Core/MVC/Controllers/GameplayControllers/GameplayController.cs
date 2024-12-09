using Assets.App.Scripts.Core.Services.Services;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets.App.Scripts.Core.MVC.Controllers.GameplayControllers
{
	public class GameplayController : IGameplayController
	{
		private ModelsService _modelsService;

		public virtual UniTask OnEnter()
		{
			_modelsService = ServiceLocator.Instance.GetService<ModelsService>();
			return UniTask.CompletedTask;
		}

		public virtual UniTask OnExit()
		{
			return UniTask.CompletedTask;
		}

		public bool GetModelOfType<T>(out T model)
		{
			if (!_modelsService.TryGetModel<T>(out model))
			{
				Debug.LogError($"Cannot initialize model of type {typeof(T)}");
				return false;
			}
			return true;
		}
	}
}