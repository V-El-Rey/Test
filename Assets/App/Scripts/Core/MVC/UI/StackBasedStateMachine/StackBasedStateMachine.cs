using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace Assets.App.Scripts.Core.MVC.UI.StackBasedStateMachine
{
	public class StackBasedStateMachine
	{
		private Dictionary<Type, IScreen> _registredScreens = new Dictionary<Type, IScreen>();
		private Stack<IScreen> _screenStack = new Stack<IScreen>();
		//private bool _isTransitioning;

		public void RegisterScreen<T>(T screen)
		{
			_registredScreens[typeof(T)] = (IScreen)screen;
		}

		public async UniTask ChangeScreen<T>()
		{
			if (!_registredScreens.TryGetValue(typeof(T), out IScreen newScreen))
				return;

			//_isTransitioning = true;
			if (_screenStack.Count > 0)
			{
				await _screenStack.Peek().Exit();
			}

			_screenStack.Push(newScreen);
			await newScreen.Enter();
			//_isTransitioning = false;
		}

        public async UniTask Back()
		{
			if (_screenStack.Count > 1)
			{
				var currentScreen = _screenStack.Pop();
				await currentScreen.Exit();

				await _screenStack.Peek().Enter();
			}
		}

		public IScreen GetCurrentScreen()
		{
			return _screenStack.Count > 0 ? _screenStack.Peek() : null;
		}

		public void OnUpdateExecute() 
		{
			GetCurrentScreen().OnUpdateExecute();
		}
	}
}