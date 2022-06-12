using System;
using System.Collections.Generic;
using UnityEngine;

public class FeverModeController : MonoBehaviour
{
	[SerializeField] private CollectibleMatchController _collectibleMatchController;

	[SerializeField] private int _feverModeNeededCount = 3;

	[SerializeField] private float _feverModeActiveDuration = 2f;
	
	public Action<float> OnFeverModeActivated;
	private void Awake()
	{
		_collectibleMatchController.OnCollectiblesMatched += OnCollectiblesMatched;
	}

	private void OnDestroy()
	{
		_collectibleMatchController.OnCollectiblesMatched -= OnCollectiblesMatched;
	}

	private void OnCollectiblesMatched(int serialMatchingCount)
	{
		if (serialMatchingCount == _feverModeNeededCount)
		{
			_collectibleMatchController.SerialMatcing = 0;
			OnFeverModeActivated?.Invoke(_feverModeActiveDuration);
		}
	}
}