using UnityEngine;

public class CameraPositionPlayer : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private Thief _thief;

    private Transform _target;

    private void Start()
    {
        _target = _thief.transform;
    }

    private void Update()
    {
        Vector3 newPosition = _target.position + _offset;
        transform.position = Vector3.Lerp(transform.position, newPosition, _speed * Time.deltaTime);
        transform.LookAt(_target);
    }
}
