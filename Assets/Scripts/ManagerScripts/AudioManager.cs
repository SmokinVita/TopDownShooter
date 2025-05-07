using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource _masterSource;
    [SerializeField] private float _musicFadeSpeed = 2f;
    [SerializeField] private float _currentMasterVolume;
    private float _fadeTime = 0.0f;

    public void StartFade()
    {
        StartCoroutine(FadeRoutine());
    }

    IEnumerator FadeRoutine()
    {
        _currentMasterVolume = _masterSource.volume;
        while (_masterSource.volume > 0)
        {
            _masterSource.volume = Mathf.SmoothStep(_currentMasterVolume, 0, _fadeTime / _musicFadeSpeed);
            _currentMasterVolume -= Time.deltaTime;
            yield return null;
        }
    }
}
