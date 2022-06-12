using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleHandler : MonoBehaviour
{
	[SerializeField] private Transform _characterVisual;

	[SerializeField] private float _amountOfRise = 0.5f;

	[SerializeField] private FeverModeController _feverModeController;
	public float AmountOfRise => _amountOfRise;
	
	private List<Collectible> _collectedCollectibles = new List<Collectible>();

	public List<Collectible> CollectedCollectibles
	{
		get => _collectedCollectibles;
		set => _collectedCollectibles = value;
	}


	public Action<List<Collectible>> OnCollectibleCollected;

	public Action<List<Collectible>> OnCollectibleCollided;

	
	
	private bool _isFeverModeActive;
	private void Awake()
	{
		_feverModeController.OnFeverModeActivated += OnFeverModeActivated;
	}

	private void OnFeverModeActivated(float feverModeDuration)
	{
		StartCoroutine(FeverModeRoutine(feverModeDuration));
	}

	private IEnumerator FeverModeRoutine(float feverModeDuration)
	{
		_isFeverModeActive = true;
		
		yield return new WaitForSeconds(feverModeDuration);

		_isFeverModeActive = false;

		yield return null;
	}


	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.TryGetComponent(out Collectible collectible))
		{
			if (collectible.TryCollect())
			{
				collectible.OnObstacleCollided += OnObstacleCollided;
				_characterVisual.localPosition = new Vector3(_characterVisual.localPosition.x,
					_characterVisual.localPosition.y+_amountOfRise, _characterVisual.localPosition.z);
				foreach (var collectedCollectible in _collectedCollectibles)
				{
					collectedCollectible.IncreaseHeight(_amountOfRise);
				}
				_collectedCollectibles.Add(collectible);
				collectible.gameObject.transform.parent = this.transform;
				collectible.SetHeight(0f);
				
				OnCollectibleCollected?.Invoke(_collectedCollectibles);
			}
			
		}
	}

	private void OnObstacleCollided(Collectible collectible)
	{
		if (_isFeverModeActive)
		{
			return;
		}

		collectible.OnObstacleCollided -= OnObstacleCollided;
		_collectedCollectibles.Remove(collectible);
		foreach (var collectedCollectible in _collectedCollectibles)
		{
			collectedCollectible.DecreaseHeight(_amountOfRise);
		}
		
		_characterVisual.localPosition = new Vector3(_characterVisual.localPosition.x,
			_characterVisual.localPosition.y - _amountOfRise, _characterVisual.localPosition.z);
		
		OnCollectibleCollided?.Invoke(_collectedCollectibles);
	}

	private void OnDestroy()
	{
		foreach (var collectible in _collectedCollectibles)
		{
			collectible.OnObstacleCollided -= OnObstacleCollided;
		}
		_feverModeController.OnFeverModeActivated -= OnFeverModeActivated;
		StopAllCoroutines();
	}

	
}