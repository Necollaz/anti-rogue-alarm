using System.Collections;
using UnityEngine;

public static class PlayerAnimationData
{
    public static class Params
    {
        public static readonly int Run = Animator.StringToHash("isRun");
        public static readonly int Walk = Animator.StringToHash("isWalk");
        public static readonly int Attack = Animator.StringToHash("isAttack");
    }
}

public class Thief : MonoBehaviour
{
    private const string Horizontal = nameof(Horizontal);
    private const string Vertical = nameof(Vertical);

    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _runSpeedMultiplier;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _reloadTime;
    [SerializeField] private AlarmSystem _alarmSystem;

    private Rigidbody _rigidbody;
    private Animator _animator;
    private Door _door;

    private bool _canHit = true;
    private bool _isRunning = false;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Move();
        CheckDoor();
        SetupAnimations();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AlarmZone"))
        {
            _alarmSystem.IncreaseVolume();
        }
        else if (other.CompareTag("Door"))
            _door = other.GetComponent<Door>();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("AlarmZone"))
        {
            _alarmSystem.DecreaseVolume();
        }
        else if (other.CompareTag("Door"))
            _door = null;
    }

    private void Move()
    {
        float verticalDirection = Input.GetAxis(Vertical);
        float horizontalDirection = Input.GetAxis(Horizontal);
        Vector3 movement = new Vector3(horizontalDirection, 0f, verticalDirection).normalized * _moveSpeed * Time.deltaTime;

        if(Input.GetKey(KeyCode.LeftShift) && (verticalDirection != 0 || horizontalDirection != 0))
        {
            _isRunning = true;
            movement *= _runSpeedMultiplier;
        }
        else
        {
            _isRunning = false;
        }

        _rigidbody.velocity = new Vector3(movement.x, _rigidbody.velocity.y, movement.z);
        transform.Translate(movement, Space.World);

        UpdateAnimationParameters(movement);
    }

    private void UpdateAnimationParameters(Vector3 movement)
    {
        if (movement != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(_rigidbody.velocity);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
        }
    }

    private void SetupAnimations()
    {
        _animator.SetBool(PlayerAnimationData.Params.Run, _isRunning);
        _animator.SetBool(PlayerAnimationData.Params.Walk, !_isRunning && _rigidbody.velocity.magnitude > 0);

        if (Input.GetMouseButton(0) && _canHit)
        {
            _animator.SetTrigger(PlayerAnimationData.Params.Attack);
            StartCoroutine(Reload());
        }
    }

    private IEnumerator Reload()
    {
        _canHit = false;
        yield return new WaitForSeconds(_reloadTime);
        _canHit = true;
    }

    private void CheckDoor()
    {
        if(Input.GetKeyDown(KeyCode.E) && _door != null)
        {
            if (!_door.isOpen)
                _door.Open();
        }
    }
}
