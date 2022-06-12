using System;
using DG.Tweening;
using UnityEngine;

public class JumpBehaviour : MonoBehaviour
{
	[SerializeField] private Transform _characterTransform;
	
	public Action OnJumpStarted;

	public Action OnJumpStopped;

	private void OnTriggerEnter(Collider other)
	{
		if(other.TryGetComponent(out JumpRamp jumpRamp))
		{
			OnJumpStarted?.Invoke();
			_characterTransform.DOJump(jumpRamp.TargetTransform.position, 10f, 1, 2f).OnComplete(
				() =>
				{
					OnJumpStopped?.Invoke();
				}
			);
		}
	}
}