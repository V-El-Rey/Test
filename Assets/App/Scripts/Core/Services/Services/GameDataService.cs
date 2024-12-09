using Assets.App.Scripts.Core.Data.AssetsLoader;
using UnityEngine;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace Assets.App.Scripts.Core.Services
{
	public class GameDataService
	{
		public AddressableAssetLoader<GameObject> AssetLoader;
		public AddressableAssetLoader<ScriptableObject> ConfigsLoader;
		public ContextsAssetLoader ContextLoader;
		public ObjectViewAssetLoader ObjectViewAssetLoader;
		public SceneAssetLoader<SceneInstance> SceneLoader;

		public GameDataService()
		{
		}
	}
}