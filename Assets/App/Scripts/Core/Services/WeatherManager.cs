using Assets.App.Scripts.Core.MainLoop;
using System;
using UnityEngine;

public class WeatherManager : MonoSingleton<WeatherManager>
{
	[Range(0, 24)]
	public float TimeOfDay;

	public float orbitSpeed = 1.0f;

	public float DayTimeSecs;
	public float NightTimeSecs;

	public Light Sun;
	public Light Moon;

	public bool isNight;

	public void Update()
	{
		TimeOfDay += Time.deltaTime * (!isNight ? orbitSpeed * 0.25f : orbitSpeed);
		if (TimeOfDay > 24.0f) 
		{
			TimeOfDay = 0;
		}
		UpdateTime();
	}

	public void UpdateTime() 
	{
		float alpha = TimeOfDay / 24.0f;
		float sunRotation = Mathf.Lerp(-90, 270, alpha);
		float moonRotation = sunRotation - 180;
		Sun.transform.rotation = Quaternion.Euler(sunRotation, -150.0f, 0);
		Moon.transform.rotation = Quaternion.Euler(moonRotation, -150.0f, 0);

		CheckNightDayTransition();
	}

	private void CheckNightDayTransition()
	{
		if (isNight) 
		{
			if(Moon.transform.rotation.eulerAngles.x > 180) 
			{
				StartDay();
			}
		}
		else 
		{
			if (Sun.transform.rotation.eulerAngles.x > 180)
			{
				StartNight();
			}
		}

	}

	private void StartDay() 
	{
		isNight = false;
		Sun.shadows = LightShadows.Soft;
		Moon.shadows = LightShadows.None;
	}

	private void StartNight() 
	{
		isNight = true;
		Moon.shadows = LightShadows.Soft;
		Sun.shadows = LightShadows.None;
	}

	private void OnValidate()
	{
		UpdateTime();
	}

}
