using Assets.App.Scripts.Core.MVC.Controllers;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;

public class ControllersManager
{
	private List<IBaseController> m_controllers;

	public ControllersManager()
	{
		m_controllers = new List<IBaseController>();
	}

	public async UniTask OnEnterControllersExecute()
	{
		foreach (var controller in m_controllers)
		{
			await controller.OnEnter();
		}
	}

	public async UniTask OnExitControllersExecute()
	{
		foreach (var controller in m_controllers)
		{
			await controller.OnExit();
		}

	}

	public void OnUpdateControllerExecute()
	{
		foreach (var controller in m_controllers)
		{
			if (controller is IUpdatable controllerInstance)
			{
				controllerInstance.OnUpdate();
			}
		}
	}

	public void OnFixedUpdateControllersExecute() 
	{
		foreach (var controller in m_controllers)
		{
			if (controller is IFixedUpdatable controllerInstance)
			{
				controllerInstance.OnFixedUpdate();
			}
		}
	}


	public void AddController(IBaseController controller)
	{
		m_controllers.Add(controller);
	}
}
