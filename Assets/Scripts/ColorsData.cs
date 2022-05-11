using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Tile Colors", menuName = "TileColors")]
public class ColorsData : ScriptableObject
{
    [SerializeField] private Color _defaultTileColor;
    [SerializeField] private Color _snakeHeadTileColor;
    [SerializeField] private Color _snakeTileColor;
    [SerializeField] private Color _foodTileColor;
    [SerializeField] private Color _wallTileColor;

    public Color DefaultTileColor => _defaultTileColor;
    public Color SnakeHeadTileColor => _snakeHeadTileColor;
    public Color SnakeTileColor => _snakeTileColor;
    public Color FoodTileColor => _foodTileColor;
    public Color WallTileColor => _wallTileColor;
}
