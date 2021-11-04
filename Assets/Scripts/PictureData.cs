using UnityEngine;

[System.Serializable]
public class PictureData 
{
    [SerializeField] Sprite _picture;
    [SerializeField] string _name;

    public Sprite Picture => _picture;
    public string Name => _name;
}
