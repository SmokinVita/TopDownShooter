using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    [SerializeField] private CanvasGroup _fadePanel;
    [SerializeField] private float _fadeSpeed;
    public float _currentFadeTime = 0.0f;


    public float _maxNumber = 100;
    public float _minNumber = 0;
    public float _percentage;

    public void StartGame()
    {
        StartCoroutine(Fade());
    }

    IEnumerator Fade()
    {
        while (_fadePanel.alpha < 1)
        {
            _fadePanel.alpha = Mathf.SmoothStep(0, 1, _currentFadeTime / _fadeSpeed);
            _currentFadeTime += Time.deltaTime;
            yield return null;
        }
        SceneManager.LoadScene(1);
        yield return null;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            _percentage = _minNumber / _maxNumber * 100;

            Debug.Log(_percentage.ToString());
        }
    }


}
