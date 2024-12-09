using Cysharp.Threading.Tasks;
using UnityEngine.UIElements;

public interface IContextView
{
    bool IsActive { get; set; }
    UniTask<bool> Activate();
	UniTask<bool> Deactivate();
}
