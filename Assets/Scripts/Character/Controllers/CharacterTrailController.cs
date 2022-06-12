using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CollectibleTypeToColor 
{
	public ECollectible CollectibleType;
	public Gradient Gradient;
}
public class CharacterTrailController : MonoBehaviour
{
	[SerializeField] private TrailRenderer _trailRenderer;
	[SerializeField] private List<CollectibleTypeToColor> _collectibleTypeToColor;
	[SerializeField] private CollectibleHandler _collectibleHandler;
	[SerializeField] private GateHandler _gateHandler;
	[SerializeField] private CollectibleMatchController _collectibleMatchController;
	[SerializeField] private Gradient _currentTrailColor;


	private void Awake()
	{
		_collectibleHandler.OnCollectibleCollected += OnCollectibleCollected;
		_collectibleHandler.OnCollectibleCollided += OnCollectibleCollided;
		_collectibleMatchController.OnCheckCompleted += OnCheckCompleted;
		_gateHandler.OnGateCollided += OnGateCollided;
		_trailRenderer.enabled = false;
	}
	
	private void OnDestroy()
	{
		_collectibleHandler.OnCollectibleCollected -= OnCollectibleCollected;
		_collectibleHandler.OnCollectibleCollided -= OnCollectibleCollided;
		_collectibleMatchController.OnCheckCompleted -= OnCheckCompleted;
		_gateHandler.OnGateCollided -= OnGateCollided;
	}

	private void OnCheckCompleted(List<Collectible> collectibles)
	{
		CheckLastCollectible(collectibles);
	}
	
	private void OnGateCollided(List<Collectible> collectibles)
	{
		CheckLastCollectible(collectibles);
	}
	private void OnCollectibleCollected(List<Collectible> collectibles)
	{
		CheckLastCollectible(collectibles);
	}

	private void OnCollectibleCollided(List<Collectible> collectibles)
	{
		CheckLastCollectible(collectibles);
	}

	private void CheckLastCollectible(List<Collectible> collectibles)
	{
		if (collectibles.Count > 0)
		{
			FindGradient(collectibles[collectibles.Count - 1].CollectibleType);
		}
		else
		{
			_trailRenderer.enabled = false;
		}
	}

	private void FindGradient(ECollectible collectibleType)
	{
		foreach (var item in _collectibleTypeToColor)
		{
			if (item.CollectibleType == collectibleType)
			{
				_currentTrailColor = item.Gradient;
				SetTrailColor(_currentTrailColor);
			}
		}
	}

	private void SetTrailColor(Gradient gradient)
	{
		_trailRenderer.enabled = true;
		_trailRenderer.colorGradient = gradient;
	}
}