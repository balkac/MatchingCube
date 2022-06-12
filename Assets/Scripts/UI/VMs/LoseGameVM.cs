using System;
using UnityEngine;
using UnityEngine.UI;

public class LoseGameVM : VMWidgetBase
{
	[SerializeField] private Button _button;

	public static Action OnReplayButtonClicked;
	
	protected override void AwakeCustomActions()
	{
		base.AwakeCustomActions();
		_button.onClick.AddListener(()=>
		{
			OnReplayButtonClicked?.Invoke();
		});
	}
	
	protected override void StartCustomActions()
	{
		base.StartCustomActions();
		TryDeactivate();
	}
}