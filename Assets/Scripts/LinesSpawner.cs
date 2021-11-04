using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class LinesSpawner : MonoBehaviour
{
    [SerializeField] GameObject _line;
    [SerializeField] float _maxLevel;
    List<Line> _lines = new List<Line>();
    Button _restart;
    UIText _text;
    float _levelNumber = 1;
    float _distance;
    float _coefficient = 0.5f;

    public List<Line> Lines => _lines;
    public float Distance => _distance;
    public delegate void VoidDelegate();
    public event VoidDelegate EndSpawn;
    public event VoidDelegate NextLevel;
    public event VoidDelegate ShowRestartImage;
    public event VoidDelegate OutRestartImage;
    public event VoidDelegate ClearWinObjects;
    public float Level => _levelNumber;

    void Start()
    {
        _restart = FindObjectOfType<Button>();
        _text = FindObjectOfType<UIText>();
        StartGame();
    }

    void SpawnLines()
    {
        if (_levelNumber == 1)
        {
            Vector2 position = Vector3.zero;
            Line line =  InstantiateLine(position);
            _distance = line.Squares[0].Distance;
        }
        else
        {
            float coef = _levelNumber / 2 - _coefficient;
            Vector2 position = CalculateSpawnPosition(coef);
            for (int i = 0; i < _levelNumber; i++)
            {
                InstantiateLine(position);
                coef--;
                position = CalculateSpawnPosition(coef);               
            }
        }
        EndSpawn();
    }

    Vector3 CalculateSpawnPosition(float coef)
    {
        return new Vector3(0, _distance * coef);
    }

    Line InstantiateLine(Vector2 position)
    {
        GameObject lineObj = Instantiate(_line, position, transform.rotation);
        Line line = lineObj.GetComponent<Line>();
        _lines.Add(line);
        return line;
    }

    public void GoToNextLevel(bool isWin)
    {
        NextLevel();
        for(int i = 0; i < _lines.Count; i ++)
        {
            Destroy(_lines[i].gameObject);
        }
        _lines.Clear();
        if (_levelNumber < _maxLevel && isWin)
        {
            _levelNumber++;
            SpawnLines();
        }
        else
        {
            _levelNumber = 1;
            _text.Fade(0);
            ShowRestart();
        }
    }

    void ShowRestart()
    {
        _restart.gameObject.SetActive(true);
        ShowRestartImage();
    }

    public void StartGame()
    {
        ClearWinObjects();
        _restart.gameObject.SetActive(false);
        _text.Fade(1);
        OutRestartImage();
        SpawnLines();
    }
}
