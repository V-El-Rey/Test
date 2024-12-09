using Assets.App.Scripts.Core.MainLoop;
using Unity.Cinemachine;
using UnityEngine;

namespace Assets.App.Scripts.Core.Services.Services
{
    public class CameraService : MonoSingleton<CameraService>
    {
        public Transform CameraRoot;
        public CinemachineBrain CinemachineBrain;
    }
}