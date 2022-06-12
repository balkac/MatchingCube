using UnityEngine;

public class JumpRamp : MonoBehaviour
{
	[SerializeField] private Transform _targetTransform;

	public Transform TargetTransform => _targetTransform;
}