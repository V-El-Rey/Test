using Assets.App.Scripts.Core.MainLoop;
using Assets.App.Scripts.Core.Services.Base;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ServiceLocator : Singleton<ServiceLocator>
{
	private Dictionary<Type, object> _registry = new Dictionary<Type, object>();

	public ServiceLocator()
	{
	}

	static ServiceLocator() 
	{
	}

	public void Register<T>(T serviceInstance)
	{
		_registry[typeof(T)] = serviceInstance;
	}

	public T GetService<T>()
	{
		T serviceInstance = (T)_registry[typeof(T)];
		return serviceInstance;
	}

	public bool TryGetService<T>(out T service) where T : class
	{
		service = null;
		if(_registry.TryGetValue(typeof(T), out object s)) 
			service = s as T;
		return service != null;
	}
}
