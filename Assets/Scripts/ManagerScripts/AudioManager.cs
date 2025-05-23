using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoSingleton<AudioManager>
{
    [SerializeField] private AudioSource _masterSource;
    [SerializeField] private float _musicFadeSpeed = 2f;
    [SerializeField] private float _currentMasterVolume;
    private float _fadeTime = 0.0f;

    //holder's for Master, BGM, and SFX tracks
    [SerializeField] private AudioMixer _masterMixer;
    [SerializeField] private AudioSource _sfxSource;

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

    public void SetMaserVolume( float sliderValue)
    {
        //grab 
        _masterMixer.SetFloat("MasterVolume", sliderValue);
    }

    public void SetBGMVolume(float sliderValue)
    {
        //set BGM Mixer float to Slider info!
        _masterMixer.SetFloat("BGMVolume", sliderValue);
    }

    public void SetSFXVolume(float sliderValue)
    {
        //sfx mixer
        _masterMixer.SetFloat("SFXVolume", sliderValue);
    }

    public void PlaySFX(AudioClip sfxClip)
    {
        _sfxSource.clip = sfxClip;
        _sfxSource.Play();
    }

    
}
