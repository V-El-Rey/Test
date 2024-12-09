using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.App.Scripts.Core.Services.Services
{
	public class EventsService
	{
		private readonly Dictionary<Type, List<Delegate>> _eventListeners = new Dictionary<Type, List<Delegate>>();

		public void Subscribe<T>(Action<T> listener) where T : IGameEvent 
		{
			var eventType = typeof(T);

			if (!_eventListeners.ContainsKey(eventType))
			{
				_eventListeners[eventType] = new List<Delegate>();
			}

			_eventListeners[eventType].Add(listener);
		}

		public void Unsubscribe<T>(Action<T> listener) where T : IGameEvent
		{
			var eventType = typeof(T);

			if (_eventListeners.ContainsKey(eventType))
			{
				_eventListeners[eventType].Remove(listener);
			}
		}

		public void Emit<T>(T gameEvent) where T : IGameEvent
		{
			var eventType = gameEvent.GetType();
			if (_eventListeners.ContainsKey(eventType))
			{
				var listenersCopy = new List<Delegate>(_eventListeners[eventType]);

				foreach (var listener in listenersCopy)
				{
					(listener as Action<T>)?.Invoke(gameEvent);
				}
			}
		}
	}
}