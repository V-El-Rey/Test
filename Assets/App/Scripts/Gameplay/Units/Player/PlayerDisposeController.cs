using Assets.App.Scripts.Core.Data.AssetsLoader;
using Assets.App.Scripts.Core.MVC.Controllers.GameplayControllers;
using Assets.App.Scripts.Core.Services;
using Assets.App.Scripts.Gameplay.Models;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets.App.Scripts.Gameplay.Units.Player
{
	public class PlayerDisposeController : GameplayController
	{
		private PlayerRuntimeModel _playerRuntimeModel;

		public override async UniTask OnEnter()
		{
			await base.OnEnter();

			if (!GetModelOfType<PlayerRuntimeModel>(out _playerRuntimeModel))
				await UniTask.FromException(new System.Exception($"Cant init model of type {typeof(PlayerRuntimeModel)}"));
		}

		public override UniTask OnExit()
		{
			GameObject.Destroy(_playerRuntimeModel.PlayerView.gameObject);
			return base.OnExit();
		}
	}
}