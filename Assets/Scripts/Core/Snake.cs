using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Snake : MonoBehaviour
{
    [SerializeField] private Tile _tile;
    [SerializeField] private int _distance = 1;
    [SerializeField] private float _snakeTimeStep = 1f;
    [Space]
    [SerializeField] private int _numOfPart = 3;
    [Space]
    [SerializeField] private Text _dispscore;
    [SerializeField] private LoseScreen _loseScreen;
    [Space]
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private GridBuilder _gridBuilder;
    [Space]
    [SerializeField] private ColorsData _colorsData;

    Tile[] alltiles;

    [SerializeField]private int _numberOfTiles;
    private int _numberOfRow;
    private int _numberOfColumn;
    private int _curTile;
    private int _prevTile;
    private int _score;

    [SerializeField] private float _currentTimeStep;

    private bool _paused = true;

    private Coroutine _snakeStep;
    private void OnEnable()
    {
        _playerController.OnEscClick += ShowPause;
    }
    private void OnDisable()
    {
        _playerController.OnEscClick -= ShowPause;
    }
    private void StartGame()
    {
        _currentTimeStep = _snakeTimeStep;
        FoodSpawn();
        SpawnSnake();
        _paused = false;
        _snakeStep = StartCoroutine(SnakeStep());
    }

    public void SetData(int numOfColumn, int numOfRow, bool walls)
    {
        _numberOfColumn = numOfColumn;
        _numberOfRow = numOfRow;
        if (walls)
        {
            _numberOfRow += 2;
            _numberOfColumn += 2;
        }
        _numberOfTiles = _numberOfRow * _numberOfColumn;
        Debug.Log(_numberOfTiles);
        alltiles = _gridBuilder.CreateGrid(_numberOfRow, _numberOfColumn, _colorsData, walls);
        StartGame();
    }

    // Update is called once per frame
    private IEnumerator SnakeStep()
    {
        while (!_paused)
        {
            {
                SnakePartMove();
                _prevTile = _curTile;
                if (_playerController.GetDirection == Directions.Up)
                {
                    if (_curTile >= _numberOfColumn)
                    {
                        _curTile -= _numberOfColumn;
                        SnakeMove();
                    }
                    else
                    {
                        _curTile += _numberOfTiles - _numberOfColumn;
                        SnakeMove();
                    }
                }
                if (_playerController.GetDirection == Directions.Left)
                {
                    if (_curTile % _numberOfColumn != 0)
                    {
                        _curTile -= 1;
                        SnakeMove();
                    }
                    else
                    {
                        _curTile += _numberOfColumn - 1;
                        SnakeMove();
                    }
                }
                if (_playerController.GetDirection == Directions.Down)
                {
                    if (_curTile < _numberOfTiles - _numberOfColumn)
                    {
                        _curTile += _numberOfColumn;
                        SnakeMove();
                    }
                    else
                    {
                        _curTile -= _numberOfTiles - _numberOfColumn;
                        SnakeMove();
                    }
                }
                if (_playerController.GetDirection == Directions.Right)
                {
                    if ((_curTile + 1) % _numberOfColumn != 0)
                    {
                        _curTile += 1;
                        SnakeMove();
                    }
                    else
                    {
                        _curTile -= _numberOfColumn - 1;
                        SnakeMove();
                    }
                }
                _playerController.IsReady = true;
                yield return new WaitForSeconds(_currentTimeStep);
            }
        }
    }
    private void SpawnSnake()
    {
        int randNum = Random.Range(0, _numberOfTiles);
        if (alltiles[randNum].CheckNum == 0)
        {
            _curTile = randNum;
            alltiles[randNum].SetTileColor(_colorsData.SnakeHeadTileColor);
            alltiles[randNum].CheckNum = 1;
        }
        else
        {
            SpawnSnake();
        }
    }
    private void SnakeMove()
    {
        Debug.Log(_curTile);
        if (alltiles[_curTile].CheckNum < 1 && alltiles[_curTile].CheckNum > -3)
        {
            alltiles[_curTile].SetTileColor(_colorsData.SnakeHeadTileColor);
            alltiles[_prevTile].SetTileColor(_colorsData.SnakeTileColor);
            alltiles[_curTile].CheckNum += 1;
            TryToEat();
        }
        else
        {
            _paused = true;
            _loseScreen.gameObject.SetActive(true);
            _loseScreen.Message(_score);
        }
    }
    private void TryToEat() 
    {
        if (alltiles[_curTile].CheckNum == -1)
        {
            _numOfPart++;
            alltiles[_curTile].CheckNum = 1;
            FoodSpawn();
            ScoreDraw();
        }
    }
    private void SnakePartMove()
    {
        for ( int tail = 0; tail < _numberOfTiles; tail++)
        {
            if (alltiles[tail].CheckNum >= _numOfPart)
            {
                alltiles[tail].CheckNum = 0;
                alltiles[tail].SetTileColor(_colorsData.DefaultTileColor);
            }
            if (alltiles[tail].CheckNum <= _numOfPart && alltiles[tail].CheckNum >= 1)
            {
                alltiles[tail].CheckNum += 1;
            }
        }
    }
    private void FoodSpawn()
    {
        int rand = Random.Range(0, _numberOfTiles);
        if (alltiles[rand].CheckNum == 0)     
        {
            alltiles[rand].CheckNum = -2;
            alltiles[rand].SetTileColor(_colorsData.FoodTileColor);
        }
        else
        {
            FoodSpawn();
        }
    }
    private void ScoreDraw()
    {
        _score += 1;
        _dispscore.text = _score.ToString();
        if (_score < 50)
        {
            _currentTimeStep -= _snakeTimeStep * 0.01f;
        }
    }
    private void ShowPause() 
    {
        _dispscore.gameObject.SetActive(!_dispscore.gameObject.activeSelf);
        if (_dispscore.gameObject.activeSelf) 
        {
            _paused = true;
            StopCoroutine(_snakeStep);
        }
        else
        {
            _paused = false;
            _snakeStep = StartCoroutine(SnakeStep());
        }
    }
}
