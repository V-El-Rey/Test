using Assets.App.Scripts.Core.Services.Services;
using Cysharp.Threading.Tasks;
using System.Linq;
using UnityEngine.SceneManagement;

public class UILoadingOperation : ILoadingOperation
{
	public string description { get => ""; set { } }

	private string m_sceneDataPath;
	private LoadSceneMode m_sceneMode;

	public UILoadingOperation(string sceneDataPath, LoadSceneMode sceneMode)
	{
		m_sceneDataPath = sceneDataPath;
		m_sceneMode = sceneMode;
	}

	public UniTask ExecuteOperation()
	{
		return UniTask.CompletedTask;
		//await GameDataService.Instanse.SceneLoader.LoadScene(m_sceneDataPath, m_sceneMode);
	}

#pragma warning disable CS1998 // В асинхронном методе отсутствуют операторы await, будет выполнен синхронный метод
	public async UniTask<UIContextService> GetContextManager()
#pragma warning restore CS1998 // В асинхронном методе отсутствуют операторы await, будет выполнен синхронный метод
	{
		//if(!GameDataService.Instanse.LoadedAdditiveScenes.TryGetValue(m_sceneDataPath, out var sceneInstance))
		//	return null;
		//return  sceneInstance.Scene.GetRootGameObjects().FirstOrDefault(x => x.name == "UIContext").GetComponent<UIContextManager>();
		return null;
	}
}
