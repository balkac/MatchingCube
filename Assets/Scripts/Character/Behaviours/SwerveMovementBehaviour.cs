using UnityEngine;

public class SwerveMovementBehaviour : MonoBehaviour
{
	[SerializeField] private JumpBehaviour _jumpBehaviour;
	[SerializeField] private Transform _characterTransform;
	[SerializeField] private float _swerveSpeed = 0.5f;
	[SerializeField] private float _zSpeed = 5f;
	private SwerveInputSystem _swerveInputSystem;
	public float ZSpeed
	{
		get => _zSpeed;
		set => _zSpeed = value;
	}

	[SerializeField] private float _maxSwerveAmount = 1f;
	[SerializeField] private bool _isMovementActive = false;
	private void Awake()
	{
		_swerveInputSystem = SwerveInputSystem.Instance;
		_jumpBehaviour.OnJumpStarted += OnJumpStarted;
		_jumpBehaviour.OnJumpStopped += OnJumpStopped;
		GameStateController.OnLevelStarted += OnLevelStarted;
		ObstacleHandler.OnCharacterFailed += OnCharacterFailed;
		Finishline.OnFinishLineTriggered += OnFinishLineTriggered;
	}
	
	private void OnDestroy()
	{
		_jumpBehaviour.OnJumpStarted -= OnJumpStarted;
		_jumpBehaviour.OnJumpStopped -= OnJumpStopped;
		GameStateController.OnLevelStarted -= OnLevelStarted;
		ObstacleHandler.OnCharacterFailed -= OnCharacterFailed;
		Finishline.OnFinishLineTriggered -= OnFinishLineTriggered;
	}

	private void OnFinishLineTriggered()
	{
		_isMovementActive = false;
	}

	private void OnLevelStarted()
	{
		_isMovementActive = true;
	}

	private void OnJumpStopped()
	{
		_isMovementActive = true;
	}

	private void OnJumpStarted()
	{
		_isMovementActive = false;
	}

	private void OnCharacterFailed()
	{
		_isMovementActive = false;
	}
	private void Update()
	{
		if (_isMovementActive)
		{
			Move();	
		}
	}

	private void Move()
	{
		float swerveAmount = Time.deltaTime * _swerveSpeed * _swerveInputSystem.Delta;
		swerveAmount = Mathf.Clamp(swerveAmount, -_maxSwerveAmount, _maxSwerveAmount);
		_characterTransform.Translate(swerveAmount, 0, _zSpeed * Time.deltaTime);
	}

	private void LateUpdate()
	{
		KeepOnPlatform();
	}

	private void KeepOnPlatform()
	{
		var characterPosition = _characterTransform.position;

		if (characterPosition.x < -3f)
		{
			_characterTransform.position = new Vector3(-3f,
				_characterTransform.position.y, characterPosition.z);
		}
		else if (characterPosition.x > 3f)
		{
			_characterTransform.position = new Vector3(3f,
				_characterTransform.position.y, characterPosition.z);
		}
	}
}