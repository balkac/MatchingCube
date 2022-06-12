using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseGate : MonoBehaviour
{
	public bool IsCollided { get; set; }
	protected abstract void OnEnterCustomActions(List<Collectible> collectedCollectibles, float amountOfRise);
	protected virtual void Deactivate()
	{
		IsCollided = true;
	}

	public Action<BaseGate> OnEntered;
	public bool TryCollide(List<Collectible> collectedCollectibles,float amountOfRise)
	{
		if (IsCollided)
		{
			return false;
		}
		Deactivate();
		OnEnterCustomActions(collectedCollectibles,amountOfRise);
		OnEntered?.Invoke(this);
		return true;

	}

}