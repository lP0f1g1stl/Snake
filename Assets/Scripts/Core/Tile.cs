using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    public int CheckNum { get; set; }

    public void SetTileColor(Color color) 
    {
        _spriteRenderer.color = color;
    }
}

