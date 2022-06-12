using UnityEngine;

public class CameraTarget : MonoBehaviour
{
    [SerializeField] private Transform _target;

    [SerializeField] private Vector3 _distance;

    [SerializeField] private bool _isLockX;

    private Vector3 _targetPosition;
    void LateUpdate()
    {
        if (_isLockX)
        {
            _targetPosition = new Vector3( transform.position.x, (_target.position + _distance).y,
                (_target.position + _distance).z);
            this.transform.position = Vector3.Lerp(this.transform.position, _targetPosition, Time.deltaTime);
        }
        else
        {
            this.transform.position = Vector3.Lerp(this.transform.position, _target.position + _distance, Time.deltaTime);
        }
        
    }
}
