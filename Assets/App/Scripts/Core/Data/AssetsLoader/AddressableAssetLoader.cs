using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;

public class AddressableAssetLoader<T> : AbstractAssetsLoader<T>
{
    public override async UniTask<T> LoadAsset(string id)
    {
        UniTask<T> asyncOperationHandle = Addressables.LoadAssetAsync<T>(id).Task.AsUniTask();
        T result = await asyncOperationHandle;
        return result;
    }
}
