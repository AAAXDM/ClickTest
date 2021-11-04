using UnityEngine;

public class Line : MonoBehaviour
{
    Square[] _squares;

    public Square[] Squares => _squares;
    void Awake()
    {
        _squares = GetComponentsInChildren<Square>();
    }
}
