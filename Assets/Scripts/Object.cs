using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;

public class Object : MonoBehaviour, IPointerDownHandler
{
    string _name;
    ObjectSpawner _objectSpawner;
    LinesSpawner _linesSpawner;
    Vector3 _positionDifference = new Vector3(0, 0.2f, 0);
    ParticleSystem _particle;
    float _duration = 0.7f;
    int _vibrato = 4;
    float _bounceTime = 0.8f;
    float _changeLevelTime = 1.2f;
    float _shakeForce = 0.2f;
    bool _isWin;

    public string Name => _name;

    void Awake()
    {
        _objectSpawner = FindObjectOfType<ObjectSpawner>();
        _linesSpawner = FindObjectOfType<LinesSpawner>();
        _particle = FindObjectOfType<ParticleSystem>();
    }

    public void AddName(string name)
    {
        _name = name;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_name == _objectSpawner.WinObject.Name)
        {

            _isWin = true;
            WinBounce();
        }
        else
        {
            _isWin = false;
            LooseBounce();
        }
        Invoke(nameof(GoToNextLevel), _changeLevelTime);
    }

    void GoToNextLevel()
    {
        _linesSpawner.GoToNextLevel(_isWin);
    }
    public void Bounce()
    {
        Vector3 endPosition = transform.position;
        transform.position = transform.position + _positionDifference;
        transform.DOMove(endPosition, _bounceTime).SetEase(Ease.OutBounce);
    }

    void LooseBounce()
    {
        transform.DOShakePosition(_duration, _shakeForce).SetEase(Ease.InBounce);
    }

    void WinBounce()
    {
        _particle.gameObject.transform.position = transform.position;
        _particle.Play();
        Bounce();
    }  
}
