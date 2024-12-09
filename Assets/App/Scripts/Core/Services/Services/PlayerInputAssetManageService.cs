using Assets.App.Scripts.Core.MainLoop;
using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

namespace Assets.App.Scripts.Core.Services.Services
{
    public class PlayerInputAssetManageService : MonoSingleton<PlayerInputAssetManageService>
    {
        public PlayerInput PlayerInput;
        public InputSystemUIInputModule InputModule;
        private AddressableAssetLoader<InputActionAsset> actionLoader;

        protected override void SingletonAwakened()
        {
            base.SingletonAwakened();
            actionLoader = new AddressableAssetLoader<InputActionAsset>();
        }

        public void InitializeManager()
        {
            InputModule.ActivateModule();
            PlayerInput.actions.Enable();
            PlayerInput.ActivateInput();
        }

        public void SetMainMenuInputActionAsset()
        {
        }

        public void EnableInput()
        {
            InputModule.ActivateModule();
            PlayerInput.actions.Enable();
            PlayerInput.ActivateInput();
        }

        public void DisableInput()
        {
            InputModule.DeactivateModule();
            PlayerInput.actions.Disable();
            PlayerInput.DeactivateInput();
        }

        public bool TryGetInputAction(string id, out InputAction value)
        {
            try 
            {
				value = PlayerInput.actions[id];
				return true;
            }
            catch(Exception e) 
            {
                Debug.LogException(e);
				value = null;
				return false;
			}
        }

        private async UniTask<InputActionAsset> LoadInputActionAsset(string assetId)
        {
            return await actionLoader.LoadAsset(assetId);
        }
    }
}