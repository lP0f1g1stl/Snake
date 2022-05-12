using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private Button _startButton;
    [SerializeField] private InputField _numOfColumn;
    [SerializeField] private InputField _numOfRow;
    [SerializeField] private Toggle _walls;
    [SerializeField] private Snake _gameManager;
    private int numberOfColumn, numberOfRow;

    private void Start()
    {
        _startButton.onClick.AddListener(SetGameSettings);
    }
    private void SetGameSettings()
    {
        SetNumOfColumn();
        SetNumOfRow();
        _gameManager.SetData(numberOfColumn, numberOfRow, _walls.isOn);
        gameObject.SetActive(false);
    }

    private void SetNumOfColumn() 
    {
        if (_numOfColumn.text != "")
        {
            numberOfColumn = int.Parse(_numOfColumn.text);
        }
        else
        {
            numberOfColumn = 10;
        }
        if (numberOfColumn < 10)
        {
            numberOfColumn = 10;
        }
        if (numberOfColumn > 25)
        {
            numberOfColumn = 25;
        }
    }
    private void SetNumOfRow() 
    {
        if (_numOfRow.text != "")
        {
            numberOfRow = int.Parse(_numOfRow.text);
        }
        else
        {
            numberOfRow = 10;
        }
        if (numberOfRow < 10)
        {
            numberOfRow = 10;
        }
        if (numberOfRow > 25)
        {
            numberOfRow = 25;
        }
    }
}
