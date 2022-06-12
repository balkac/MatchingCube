using System;
using UnityEngine;

public abstract class VMWidgetBase : MonoBehaviour
{
	[SerializeField] private GameObject _parentCanvas;

	private void Awake()
	{
		AwakeCustomActions();
	}

	private void Start()
	{
		StartCustomActions();
	}

	protected virtual void StartCustomActions()
	{
	}
	protected virtual void AwakeCustomActions()
	{
	}
	protected virtual void TryActivateCustomActions()
	{
	}

	protected virtual void TryDeactivateCustomActions()
	{
	}
	
	public void TryActivate()
	{
		_parentCanvas.SetActive(true);
		TryActivateCustomActions();
	}

	public void TryDeactivate()
	{
		_parentCanvas.SetActive(false);
		TryDeactivateCustomActions();
	}
	
}