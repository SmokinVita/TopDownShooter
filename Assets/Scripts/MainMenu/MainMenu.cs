using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Image _blackPanel;
    [SerializeField] private float _fadeSpeed = 2f;
    private float _fadeTime = 0f;
    private bool _canFade = true;

    public void StartGame()
    {
        //_canFade = true;
        //SceneManager.LoadScene(1);
        StartCoroutine(FadeInRoutine());
    }

    //Grab panel UI and increase Alpha to fade to black before changing scene
    private void Update()
    {
        if (_canFade == true)
        {
            _blackPanel.material.color = new Color(1, 1, 1, Mathf.SmoothStep(0, 1, _fadeSpeed + Time.time));
        }
    }

    IEnumerator FadeInRoutine()
    {
        while (_blackPanel.color.a != 1)
        {
            _fadeTime += Time.time + _fadeSpeed;
            _blackPanel.color = new Color(1, 1, 1, Mathf.Lerp(0, 1, _fadeTime));
            return null;
        }
        return null;
    }

}
