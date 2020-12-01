using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public GameObject ok;
    public InputField numofColumn;
    public InputField numofRow;
    public Toggle walls;
    public GameObject gameManager;
    public bool start = true;
    int numberofColumn, numberofRow;


    public void OK()
    {
        if (numofColumn.text != "")
        {
            numberofColumn = int.Parse(numofColumn.text);
        }
        else
        {
            numberofColumn = 10;
        }
        if (numofRow.text != "")
        {
            numberofRow = int.Parse(numofRow.text);
        }
        else
        {
            numberofRow = 10;
        }
        if (numberofColumn < 10)
        {
            numberofColumn = 10;
        }
        if (numberofRow < 10)
        {
            numberofRow = 10;
        }
        gameManager.GetComponent<Snake>().Inf(numberofColumn, numberofRow, walls.isOn, start);
        gameObject.SetActive(false);
    }
}
