using System.Collections;
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
    private float _masterVolume, _bgmVolume, _sfxVolume;
    [SerializeField] private AudioSource _sfxSource;

    private void Start()
    {
        _masterMixer.GetFloat("MasterVolume", out _masterVolume);
        _masterMixer.GetFloat("BGMVolume", out _bgmVolume);
        _masterMixer.GetFloat("SFXVolume", out _sfxVolume);

        if (UIManager.Instance == null)
            return;

        UIManager.Instance.SetSliders(_masterVolume, _bgmVolume, _sfxVolume);
    }

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

    public void SetMaserVolume(float sliderValue)
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

    public void SetSFX(AudioClip sfxClip)
    {
        _sfxSource.clip = sfxClip;
    }

    public void PlaySFX()
    {
        _sfxSource.Play();
    }
}
