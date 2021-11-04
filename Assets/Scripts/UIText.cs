using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[RequireComponent(typeof(Text))]
public class UIText : MonoBehaviour
{
    Text _text;
    ObjectSpawner _objectSpawner;
    float _fadeTime = 1f;

    void Awake()
    {
        _text = GetComponent<Text>();
        _objectSpawner = FindObjectOfType<ObjectSpawner>();
    }

    void OnEnable()
    {
        _objectSpawner.StopInstantiate += GetText;
    }

    void OnDisable()
    {
        _objectSpawner.StopInstantiate += GetText;
    }

    void GetText()
    {
        _text.text = "Find " + _objectSpawner.WinObject.Name;
    }

    public void Fade(float value)
    {
        _text.DOFade(value, _fadeTime);
    }
}
