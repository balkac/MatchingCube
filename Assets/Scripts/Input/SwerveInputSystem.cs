using System;
using UnityEngine;

public class SwerveInputSystem : Singleton<SwerveInputSystem>
{
	private float _lastFrameFingerPositionX;
	private float _delta;
	public float Delta => _delta;
	
	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			_lastFrameFingerPositionX = Input.mousePosition.x;
		}
		
		else if (Input.GetMouseButton(0))
		{
			_delta = Input.mousePosition.x - _lastFrameFingerPositionX;
			_lastFrameFingerPositionX = Input.mousePosition.x;
		}
		
		else if (Input.GetMouseButtonUp(0))
		{
			_delta = 0f;
		}
	}
}