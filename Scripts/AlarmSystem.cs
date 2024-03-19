using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AlarmSystem : MonoBehaviour
{
	[SerializeField] private AudioSource _alarmSound;
    [SerializeField] private float _soundStep;

    private Coroutine _changeVolume;
    private float _maxVolume = 1;
    private float _minVolume = 0;

    private void Start()
    {
        _alarmSound = GetComponent<AudioSource>();
        _alarmSound.volume = _minVolume;
        _alarmSound.loop = true;
        _alarmSound.Stop();
    }

    public void IncreaseVolume()
    {
        _alarmSound.Play();
        Stop();
        _changeVolume = StartCoroutine(ChangeVolume(_maxVolume));
    }

    public void DecreaseVolume()
    {
        Stop();
        _changeVolume = StartCoroutine(ChangeVolume(_minVolume));
    }

    private void OnDisable()
    {
        Stop();
    }

    private void Stop()
    {
        if (_changeVolume != null)
            StopCoroutine(_changeVolume);
    }

    private IEnumerator ChangeVolume(float value)
    {
        while (!Mathf.Approximately(_alarmSound.volume, value))
        {
            _alarmSound.volume = Mathf.MoveTowards(_alarmSound.volume, value, _soundStep * Time.deltaTime);

            yield return null;
        }

        if (Mathf.Approximately(value, _minVolume))
            _alarmSound.Stop();
    }
}

