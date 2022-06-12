using System;
using UnityEngine;
using UnityEngine.UI;

public class WinGameVM : VMWidgetBase
{
	[SerializeField] private Button button;

	public static Action OnNextLevelButtonClicked;
	
	protected override void AwakeCustomActions()
	{
		base.AwakeCustomActions();
		button.onClick.AddListener(()=>
		{
			OnNextLevelButtonClicked?.Invoke();
		});
	}
	
	protected override void StartCustomActions()
	{
		base.StartCustomActions();
		TryDeactivate();
	}
}