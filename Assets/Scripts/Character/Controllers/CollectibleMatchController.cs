using System;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleMatchController : MonoBehaviour
{
	[SerializeField] private CollectibleHandler _collectibleHandler;

	[SerializeField] private GateHandler _gateHandler;
	
	[SerializeField] private Transform _characterVisual;
	
	[SerializeField] private int _matchCubeCount = 3;
	
	public Action<int> OnCollectiblesMatched;
	public Action<List<Collectible>> OnCheckCompleted;
	
	private int _serialMatcing = 0;

	public int SerialMatcing
	{
		get => _serialMatcing;
		set => _serialMatcing = value;
	}

	private void Awake()
	{
		_collectibleHandler.OnCollectibleCollected += OnCollectibleCollected;
		_gateHandler.OnGateCollided += OnGateCollided;
	}
	
	private void OnDestroy()
	{
		_collectibleHandler.OnCollectibleCollected -= OnCollectibleCollected;
		_gateHandler.OnGateCollided -= OnGateCollided;
	}

	private void OnGateCollided(List<Collectible> collectibles)
	{
		Check(collectibles);
	}
	private void OnCollectibleCollected(List<Collectible> collectibles)
	{
		// if (Check(collectibles))
		// {
		// 	_serialMatcing++;
		// 	OnCollectiblesMatched?.Invoke(_serialMatcing);
		// }
		Check(collectibles);
	}

	private bool Check(List<Collectible> collectibles)
	{
		int collectiblesCount = collectibles.Count;
		if (collectiblesCount < _matchCubeCount)
		{
			return false;
		}
		int matchingCollectiblesCount = 1;
		List<Collectible> matchingCollectibles = new List<Collectible>();
		for (int i = 0; i < collectiblesCount-1; i++)
		{
			matchingCollectibles.Add(collectibles[i]);
			if (collectibles[i].CollectibleType == collectibles[i + 1].CollectibleType)
			{
				matchingCollectiblesCount++;
				matchingCollectibles.Add(collectibles[i+1]);
			}
			else
			{
				matchingCollectiblesCount = 1;
				matchingCollectibles = new List<Collectible>();
				continue;
			}

			if (matchingCollectiblesCount == _matchCubeCount)
			{
				foreach (var collectible in matchingCollectibles)
				{
					collectibles.Remove(collectible);
					collectible.DestroyCollectible();
				}
				_characterVisual.localPosition = new Vector3(_characterVisual.localPosition.x,
					_characterVisual.localPosition.y - _collectibleHandler.AmountOfRise * _matchCubeCount, _characterVisual.localPosition.z);

				// for (int j = 0; j < collectibles.Count; j++)
				// {
				// 	collectibles[j].SetHeight(j*_collectibleHandler.AmountOfRise);
				// }
				for (int j = 0; j < collectibles.Count; j++)
				{
					float height = (collectibles.Count - 1 - j) * _collectibleHandler.AmountOfRise;
					collectibles[j].SetHeight(height);
				}
				
				Check(collectibles);
				_serialMatcing++;
				OnCollectiblesMatched?.Invoke(_serialMatcing);
				OnCheckCompleted?.Invoke(collectibles);
				return true;
			}
			
		}

		return false;
	}
	
}