using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

public class SceneAssetLoader<T> : AbstractAssetsLoader<T>
{
    private LoadSceneMode m_loadSceneMode = LoadSceneMode.Single;
	private Dictionary<string, SceneInstance> _loadedAdditiveScenes;

	public SceneAssetLoader()
	{
		_loadedAdditiveScenes ??= new Dictionary<string, SceneInstance>();
	}

	public async UniTask UnloadScene(string sceneId)
    {
        if (_loadedAdditiveScenes.ContainsKey(sceneId))
        {
            var sceneInstance = _loadedAdditiveScenes[sceneId];
            var asyncOperationHandle = Addressables.UnloadSceneAsync(sceneInstance);
            _loadedAdditiveScenes.Remove(sceneId);
            await asyncOperationHandle;
        }
    }
    public async UniTask LoadScene(string id, LoadSceneMode loadSceneMode = LoadSceneMode.Single)
    {
        m_loadSceneMode = loadSceneMode;
        await LoadAsset(id);
    }
    public override async UniTask<T> LoadAsset(string id)
    {
        var asyncOperationHandle = Addressables.LoadSceneAsync(id, m_loadSceneMode);
        await asyncOperationHandle.ToUniTask();
        _loadedAdditiveScenes.TryAdd(id, asyncOperationHandle.Result);
        return default;
    }
}
