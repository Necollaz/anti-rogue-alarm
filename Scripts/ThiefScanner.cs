using UnityEngine;

public class ThiefScanner : MonoBehaviour
{
    private AlarmSystem _alarmSystem;

    private void Awake()
    {
        _alarmSystem = GetComponent<AlarmSystem>();

        if (_alarmSystem == null)
            Debug.Log("AlarmZone component not found on the object");
    }

    private void OnTriggerEnter(Collider other)
	{
        if (other.TryGetComponent<Thief>(out _))
            _alarmSystem?.IncreaseVolume();
	}

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Thief>(out _))
            _alarmSystem?.DecreaseVolume();
    }
}

