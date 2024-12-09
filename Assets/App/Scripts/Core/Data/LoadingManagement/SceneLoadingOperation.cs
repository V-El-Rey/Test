using Assets.App.Scripts.Core.Services;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

public class SceneLoadingOperation : ILoadingOperation
{
    public string description { get; set; }
    private string _sceneDataPath;
    private LoadSceneMode _sceneMode;

    private GameDataService _gameDataService;

    public SceneLoadingOperation(string sceneDataPath, LoadSceneMode sceneMode, string description = "")
    {
        if(description != "")
            this.description = description;
        _sceneDataPath = sceneDataPath;
        _sceneMode = sceneMode;

        _gameDataService = ServiceLocator.Instance.GetService<GameDataService>();
    }

    public async UniTask ExecuteOperation()
    {
		await _gameDataService.SceneLoader.LoadScene(_sceneDataPath, _sceneMode);
	}
}
