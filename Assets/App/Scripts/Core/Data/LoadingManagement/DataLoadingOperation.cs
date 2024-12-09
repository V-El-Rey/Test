using Assets.App.Scripts.Core.Services;
using Cysharp.Threading.Tasks;

public class DataLoadingOperation : ILoadingOperation
{
    private GameDataService _gameData;

    public DataLoadingOperation(GameDataService gameData)
    {
        _gameData = gameData;
    }
    
    //�������� �������� ��� UI
	public string description { get => "�������� ������"; set { } }


	public async UniTask ExecuteOperation()
    {
        await UniTask.WaitForSeconds(1);
		await UniTask.WaitForSeconds(20);
	}
}
