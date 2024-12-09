using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace Assets.App.Scripts.Core.Services.Services
{
	public class ScenesLoadingService
	{
		private Dictionary<string, SceneLoadingOperation> sceneLoadingOperations = new();
		private string currentSceneId;

		public ScenesLoadingService()
		{
			sceneLoadingOperations.Add(
				AddressablePath.SCENE_MAIN_MENU,
				new SceneLoadingOperation(AddressablePath.SCENE_MAIN_MENU, LoadSceneMode.Single, "Главное меню.."
				));
			sceneLoadingOperations.Add(
				AddressablePath.SCENE_EMPTY,
				new SceneLoadingOperation(AddressablePath.SCENE_EMPTY, LoadSceneMode.Single, "Подготовка.."));
			sceneLoadingOperations.Add(
				AddressablePath.SCENE_GAME,
				new SceneLoadingOperation(AddressablePath.SCENE_GAME, LoadSceneMode.Single, "Загрузка игры.."));
			sceneLoadingOperations.Add(
				AddressablePath.SCENE_ADDITIONAL_ENVIRONMENT,
				new SceneLoadingOperation(AddressablePath.SCENE_ADDITIONAL_ENVIRONMENT, LoadSceneMode.Additive, "Загрузка окружения.."));
		}

		public async UniTask LoadAdditional(string id) 
		{
			UIContextService.Instance.SetLoadingText(sceneLoadingOperations[id].description);
			await sceneLoadingOperations[id].ExecuteOperation();
		}

		public async UniTask LoadMainMenu()
		{
			UIContextService.Instance.SetLoadingText(sceneLoadingOperations[AddressablePath.SCENE_MAIN_MENU].description);
			currentSceneId = "Main Menu";
			await sceneLoadingOperations[AddressablePath.SCENE_MAIN_MENU].ExecuteOperation();
		}

		public async UniTask LoadGameScene()
		{
			UIContextService.Instance.SetLoadingText(sceneLoadingOperations[AddressablePath.SCENE_GAME].description);
			currentSceneId = "Game";
			await sceneLoadingOperations[AddressablePath.SCENE_GAME].ExecuteOperation();
		}

		public async UniTask UnloadCurrentScene()
		{
			UIContextService.Instance.SetLoadingText(sceneLoadingOperations[AddressablePath.SCENE_EMPTY].description);
			currentSceneId = string.Empty;
			await sceneLoadingOperations[AddressablePath.SCENE_EMPTY].ExecuteOperation();
		}
	}
}