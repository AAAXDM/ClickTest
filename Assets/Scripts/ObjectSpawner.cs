using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] ObjectSO[] _objectsSO;
    List<Line> _lines;
    ObjectSO _objectsType;
    LinesSpawner _lineSpawner;
    List<Vector3> _points = new List<Vector3>();
    List<int> _random = new List<int>();
    Vector3 _scale = new Vector3(0.4f, 0.4f,1);
    List<Object> _objects = new List<Object>();
    List<string> _winObjects = new List<string>();
    Object _winObject;

    public Object WinObject => _winObject;
    public delegate void VoidDelegate();
    public event VoidDelegate StopInstantiate;

    void Awake()
    {
        _lineSpawner = FindObjectOfType<LinesSpawner>();
    }

    void OnEnable()
    {
        _lineSpawner.EndSpawn += SpawnObjects;
        _lineSpawner.NextLevel += FinishLevel;
        _lineSpawner.ClearWinObjects += ClearWinObjectsList;
    }

    void OnDisable()
    {
        _lineSpawner.EndSpawn -= SpawnObjects;
        _lineSpawner.NextLevel -= FinishLevel;
        _lineSpawner.ClearWinObjects -= ClearWinObjectsList;
    }

    void ChooseObjects()
    {
        int coef = Random.Range(0, _objectsSO.Length);
        _objectsType = _objectsSO[coef];
    }

    void GetInstantiatePoints()
    {
        _lines = _lineSpawner.Lines;
        if(_objectsType.Pictures.Length < _lines.Count*_lines[0].Squares.Length)
        {
            Debug.LogError("Not enough objects!");
        }
        for(int i = 0; i < _lines.Count; i++)
        {
            Square[] squares =  _lines[i].Squares;
            for(int j = 0; j < squares.Length; j++)
            {
                _points.Add(squares[j].transform.position);
            }
        }
    }

    void SpawnObjects()
    {
        ChooseObjects();
        GetInstantiatePoints();
        for (int i = 0; i < _points.Count; i++)
        {

            int random = int.MinValue;
            bool canInstantiate = false;
            while (!canInstantiate)
            {
                random  =  Random.Range(0, _objectsType.Pictures.Length);
                if (!_random.Contains(random)) canInstantiate = true;
            }
            _random.Add(random);
            InstantiateObject(random, i);
        }
        FindWinObject();
        StopInstantiate();
    }

    void InstantiateObject(int random, int iteration)
    {
        GameObject sprite = new GameObject();
        SpriteRenderer spriteRenderer = sprite.AddComponent<SpriteRenderer>();
        Object obj = sprite.AddComponent<Object>();
        BoxCollider2D collider = sprite.AddComponent<BoxCollider2D>();
        collider.size = spriteRenderer.bounds.size;
        _objects.Add(obj);
        spriteRenderer.sprite = _objectsType.Pictures[random].Picture;
        obj.AddName(_objectsType.Pictures[random].Name);
        collider.size = spriteRenderer.size;
        sprite.transform.position = _points[iteration];
        sprite.transform.localScale = _scale;
        if (_lineSpawner.Level == 1)
        {
            obj.Bounce();
        }
    }

    void FindWinObject()
    {
        bool canUse = false;
        while (!canUse)
        {
            int random = Random.Range(0, _objects.Count);
            _winObject = _objects[random];
            if (!_winObjects.Contains(_winObject.Name)) canUse = true;
        }
        _winObjects.Add(WinObject.Name);
    }

    void FinishLevel()
    {
        _winObject = null;
        _points.Clear();
        _random.Clear();
        for(int i = 0; i < _objects.Count; i ++)
        {
            Destroy(_objects[i].gameObject);
        }
        _objects.Clear();
    }

    void ClearWinObjectsList()
    {
        _winObjects.Clear();
    }
}
