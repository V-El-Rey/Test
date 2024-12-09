using Cysharp.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine.AddressableAssets;

public abstract class AbstractAssetsLoader<T> : IAddresableLoader<T>
{
    public abstract UniTask<T> LoadAsset(string id);
}
