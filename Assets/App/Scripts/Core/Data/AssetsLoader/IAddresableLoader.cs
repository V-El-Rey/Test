using Cysharp.Threading.Tasks;

public interface IAddresableLoader<T>
{
    UniTask<T> LoadAsset(string id);
}
