using UnityEngine;

public class DoorOpenTrigger : MonoBehaviour
{
    [SerializeField] private Door _door;

    private bool _hasOpener;
    private bool _isOpened;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<DoorOpener>())
            _hasOpener = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<DoorOpener>())
            _hasOpener = false;
    }

    private void Update()
    {
        if (_isOpened)
            return;

        if (_hasOpener && Input.GetKeyDown(KeyCode.E))
        {
            _door.OpenDoor();
            _hasOpener = true;
        }
    }
}
