using System;
using UnityEngine;

public class Collectible : MonoBehaviour
{
	[SerializeField] private ECollectible _collectibleType;
	public ECollectible CollectibleType => _collectibleType;

	[SerializeField] private Collider _collider;

	public Collider Collider => _collider;

	private bool _isCollected;

	public Action<Collectible> OnObstacleCollided;
	
	public bool TryCollect()
	{
		if (!_isCollected)
		{
			_isCollected = true;
			return true;
		}

		return false;
	}

	public void SetHeight(float height)
	{
		transform.localPosition = new Vector3(0, height, 0);
	}

	public void IncreaseHeight(float increaseAmount)
	{
		transform.localPosition = new Vector3(0, transform.localPosition.y + increaseAmount, 0);
	}
	public void DecreaseHeight(float decreaseAmount)
	{
		transform.localPosition = new Vector3(0, transform.localPosition.y - decreaseAmount, 0);
	}

	public void DestroyCollectible()
	{
		this.transform.parent = null;
		this.Collider.enabled = false;
		this.gameObject.SetActive(false);
	}
	
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.TryGetComponent(out Obstacle obstacle))
		{
			if (obstacle.TryCollide())
			{
				transform.parent = null;
				_collider.enabled = false;
				OnObstacleCollided?.Invoke(this);	
			}
		}
	}
}