using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Square : MonoBehaviour
{
    SpriteRenderer _spriteRenderer;
    float _distance;
    float _coefficient = 0.2f;

    public float Distance => _distance;

    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _distance = _spriteRenderer.size.x / 2 - _coefficient;
    }
}
