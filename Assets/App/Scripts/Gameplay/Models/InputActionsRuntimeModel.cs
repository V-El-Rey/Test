using Assets.App.Scripts.Core.MVC.Models;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.App.Scripts.Gameplay.Models
{
	public class InputActionsRuntimeModel : IRuntimeModel
	{
		public Dictionary<string, InputAction> InputActions = new();
		public Vector2 KeyboardInput { get; set; }

		public void Clear()
		{
			InputActions.Clear();
		}
	}
}