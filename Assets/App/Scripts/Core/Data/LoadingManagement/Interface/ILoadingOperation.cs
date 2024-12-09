using System.Threading.Tasks;
using Cysharp.Threading.Tasks;

public interface ILoadingOperation
{
    string description { get; set; }
    UniTask ExecuteOperation();
}
