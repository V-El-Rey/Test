using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Assets.App.Scripts.Core.Data.AssetsLoader
{
    public class ObjectViewAssetLoader : AbstractAssetsLoader<BaseObjectView>
    {
        public async override UniTask<BaseObjectView> LoadAsset(string id)
        {
            UniTask<GameObject> asyncOperationHandle = Addressables.LoadAssetAsync<GameObject>(id).Task.AsUniTask();
            GameObject prefabObject = await asyncOperationHandle;
            var result = GameObject.Instantiate(prefabObject);
            var baseObjectView = result.GetComponent<BaseObjectView>();
            await UniTask.WaitUntil(() => result.activeInHierarchy == true);
            return baseObjectView;
        }

        public async UniTask<BaseObjectView> LoadAsset(string id, Transform root) 
        {
            var view = await LoadAsset(id);
            view.Transform.SetParent(root);
            view.Transform.position = Vector3.zero;
            view.transform.rotation = Quaternion.identity;
            return view;
        }
    }
}