using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridBuilder : MonoBehaviour
{
    [SerializeField] private Tile _tile;
    [SerializeField] private int _distance = 1;

    private float x1, x2, y1;
    public Tile[] CreateGrid(int numberOfRow, int numberOfColumn, ColorsData colorsData, bool walls)
    {
        int numberOfTiles = numberOfRow * numberOfColumn;
        x1 = 0.5f - numberOfColumn * 1.0f / 2;
        y1 = 0.5f + numberOfRow * 1.0f / 2;
        x2 = x1;
        Tile[] alltiles = new Tile[numberOfTiles];

        CreateTiles(numberOfTiles, numberOfColumn, colorsData, alltiles, walls);
        return alltiles;
    }
        private void CreateTiles(int _numberOfTiles, int _numberOfColumn, ColorsData colorsData, Tile[] alltiles, bool walls)
    {
        for (int createdTiles = 0; createdTiles < _numberOfTiles; createdTiles++)
        {
            x1 += _distance;
            if (createdTiles % _numberOfColumn == 0)
            {
                y1 -= _distance;
                x1 = x2;
            }
            alltiles[createdTiles] = Instantiate(_tile, new Vector3(transform.position.x + x1, transform.position.y + y1, transform.position.z), transform.rotation);
            if (walls)
            {
                TryToCreateWall(_numberOfTiles, _numberOfColumn, colorsData, alltiles, createdTiles);
            }
        }
    }
    private void TryToCreateWall(int _numberOfTiles, int _numberOfColumn, ColorsData colorsData, Tile[] alltiles, int createdTiles) 
    {
        if (createdTiles >= 0 && createdTiles < _numberOfColumn)
        {
            alltiles[createdTiles].CheckNum = -4;
            alltiles[createdTiles].SetTileColor(colorsData.WallTileColor);
        }
        if (((createdTiles + 1) % _numberOfColumn == 0 || createdTiles % _numberOfColumn == 0))
        {
            alltiles[createdTiles].CheckNum = -4;
            alltiles[createdTiles].SetTileColor(colorsData.WallTileColor);
        }
        if (createdTiles >= _numberOfTiles - _numberOfColumn)
        {
            alltiles[createdTiles].CheckNum = -4;
            alltiles[createdTiles].SetTileColor(colorsData.WallTileColor);
        }
    }
}
