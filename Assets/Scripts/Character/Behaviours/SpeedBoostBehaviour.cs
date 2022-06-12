using System.Collections;
using UnityEngine;

public class SpeedBoostBehaviour : MonoBehaviour
{
	[SerializeField] private SwerveMovementBehaviour _swerveMovementBehaviour;

	[SerializeField] private FeverModeController _feverModeController;
	
	[SerializeField] private float _boostPercentage;
	
	[SerializeField] private float _boostTime;

	private float _firstSpeed;
	private void Awake()
	{
		_feverModeController.OnFeverModeActivated += OnFeverModeActivated;
		_firstSpeed = _swerveMovementBehaviour.ZSpeed;
	}

	private void OnFeverModeActivated(float feverModeDuration)
	{
		StartCoroutine(BoostRoutine(feverModeDuration));
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.TryGetComponent(out SpeedBoostFloor speedBoostFloor))
		{
			StartCoroutine(BoostRoutine());
		}
	}

	private void OnDestroy()
	{
		StopAllCoroutines();
	}

	private IEnumerator BoostRoutine(float feverModeDuration)
	{
		var delay = new WaitForSeconds(feverModeDuration);
		_swerveMovementBehaviour.ZSpeed += _swerveMovementBehaviour.ZSpeed * _boostPercentage / 100f;
		yield return delay;

		_swerveMovementBehaviour.ZSpeed = _firstSpeed;
		yield return null;
	}
	
	private IEnumerator BoostRoutine()
	{
		var delay = new WaitForSeconds(_boostTime);
		_swerveMovementBehaviour.ZSpeed += _swerveMovementBehaviour.ZSpeed * _boostPercentage / 100f;
		yield return delay;

		_swerveMovementBehaviour.ZSpeed = _firstSpeed;
		yield return null;
	}
}