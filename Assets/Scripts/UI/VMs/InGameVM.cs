using System;

public class InGameVM : VMWidgetBase
{
	private LevelInfoUI _levelInfoUI;

	private void Init()
	{
		_levelInfoUI = GetComponentInChildren<LevelInfoUI>();
		_levelInfoUI.Init();
	}
	
	protected override void TryActivateCustomActions()
	{
		base.TryActivateCustomActions();
		Init();
	}
	
	protected override void StartCustomActions()
	{
		base.StartCustomActions();
		TryDeactivate();
	}
}