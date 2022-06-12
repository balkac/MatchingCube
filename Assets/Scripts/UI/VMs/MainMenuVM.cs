using System;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuVM : VMWidgetBase
{
	[SerializeField] private Button _button;

	public static Action OnPlayButtonClicked;

	protected override void AwakeCustomActions()
	{
		base.AwakeCustomActions();
		TryActivate();
		_button.onClick.AddListener(()=>
		{
			OnPlayButtonClicked?.Invoke();
		});
	}
	
}