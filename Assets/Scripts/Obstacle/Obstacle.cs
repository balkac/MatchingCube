using UnityEngine;

public enum EObstacle
{
	Single = 1,
	Double = 2,
}
public class Obstacle : MonoBehaviour
{
	[SerializeField] private Collider _collider;

	[SerializeField] private EObstacle _obstacleType;
	
	private bool _isCollided;

	public bool TryCollide()
	{
		if (!_isCollided)
		{
			_isCollided = true;
			_collider.enabled = false;
			return true;
		}

		return false;
	}

	public bool CheckFailCondition(int collectibleCount)
	{
		return collectibleCount < (int) _obstacleType;
	}
}