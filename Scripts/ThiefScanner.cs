using UnityEngine;
using UnityEngine.Events;

public class ThiefScanner : MonoBehaviour
{
    [SerializeField] private UnityEvent _alarmOn;
    [SerializeField] private UnityEvent _alarmOff;

    private void OnTriggerEnter(Collider other)
	{
		if (other.GetComponent<Thief>())
			_alarmOn?.Invoke();
	}

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Thief>())
            _alarmOff?.Invoke();
    }
}

