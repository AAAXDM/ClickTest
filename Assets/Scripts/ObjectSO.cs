using UnityEngine;

[CreateAssetMenu(fileName = "ObjectSO", menuName = "ObjectSO", order = 51)]
public class ObjectSO : ScriptableObject
{
    [SerializeField] PictureData[] _pictures;

    public PictureData[] Pictures => _pictures;
}
