using UnityEngine;

public class DoorOpenTrigger : MonoBehaviour
{
    [SerializeField] private Door _door;

    private bool _hasOpener;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<DoorOpener>(out _))
            _hasOpener = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<DoorOpener>(out _))
            _hasOpener = false;
    }

    private void Update()
    {
        if (_hasOpener && !_door.isOpen && Input.GetKeyDown(KeyCode.E))
        {
            _door.Open();
        }
    }
}
