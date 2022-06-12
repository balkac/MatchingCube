using System;
using System.Collections.Generic;
using UnityEngine;

public class GateHandler : MonoBehaviour
{
	[SerializeField] private CollectibleHandler _collectibleHandler;
	
	public Action<List<Collectible>> OnGateCollided;
	private void OnTriggerEnter(Collider other)
	{
		if (other.TryGetComponent(out BaseGate gate))
		{
			if (gate.TryCollide(_collectibleHandler.CollectedCollectibles, _collectibleHandler.AmountOfRise))
			{
				OnGateCollided?.Invoke(_collectibleHandler.CollectedCollectibles);
			}
		}
	}
}