using System;
using UnityEngine;

public class ObstacleHandler : MonoBehaviour
{
	[SerializeField] private CollectibleHandler _collectibleHandler;
	
	public static Action OnCharacterFailed;
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.TryGetComponent(out Obstacle obstacle))
		{
			if(obstacle.CheckFailCondition(_collectibleHandler.CollectedCollectibles.Count))
			{
				OnCharacterFailed?.Invoke();
			}
		}
	}
	
}