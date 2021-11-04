using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[RequireComponent(typeof(Image))]
public class RestartImage : MonoBehaviour
{
    LinesSpawner _linesSpawner;
    Image _image;
    float _fadeTime = 1;

    void Awake()
    {
        _image = GetComponent<Image>();
        _linesSpawner = FindObjectOfType<LinesSpawner>();
    }

    void OnEnable()
    {
        _linesSpawner.ShowRestartImage += FadeIn;
        _linesSpawner.OutRestartImage += FadeOut;
    }

    void OnDisable()
    {
        _linesSpawner.ShowRestartImage -= FadeIn;
        _linesSpawner.OutRestartImage += FadeOut;
    }

    void Fade(float value)
    {
        _image.DOFade(value, _fadeTime);
    }

    void FadeIn()
    {
        Fade(1);
    }

    void FadeOut()
    {
        Fade(0);
    }
}
