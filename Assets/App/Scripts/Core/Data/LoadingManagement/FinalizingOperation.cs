using Cysharp.Threading.Tasks;

public class FinalizingOperation : ILoadingOperation
{
    public string description { get; set; }

    public async UniTask ExecuteOperation()
    {
        await UniTask.WaitForSeconds(1);
    }
}
