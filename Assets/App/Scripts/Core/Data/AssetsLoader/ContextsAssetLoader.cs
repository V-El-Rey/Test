using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class ContextsAssetLoader : AbstractAssetsLoader<IContextView>
{
	private Transform _uiRoot;

	public ContextsAssetLoader(Transform uiRoot) 
	{
		_uiRoot = uiRoot;
	}
	public async override UniTask<IContextView> LoadAsset(string id)
	{
		UniTask<GameObject> asyncOperationHandle = Addressables.LoadAssetAsync<GameObject>(id).Task.AsUniTask();
		GameObject prefabObject = await asyncOperationHandle;
		var result = GameObject.Instantiate(prefabObject, _uiRoot);
		var contextView = result.GetComponent<IContextView>();
		await UniTask.WaitUntil(() =>  result.activeInHierarchy == true );
		return contextView;
	}
}
