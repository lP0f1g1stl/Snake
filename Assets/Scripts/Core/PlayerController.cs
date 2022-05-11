using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Directions _currentDirection;
    public Directions GetDirection => _currentDirection;
    public Action OnEscClick;
    public bool IsReady { get; set; }
    void Update()
    {
        if (IsReady)
        {
            if (Input.GetKeyDown("w") && _currentDirection != Directions.Down)
            {
                ChangeDirection(Directions.Up);
            }
            if (Input.GetKeyDown("a") && _currentDirection != Directions.Right)
            {
                ChangeDirection(Directions.Left);
            }
            if (Input.GetKeyDown("s") && _currentDirection != Directions.Up)
            {
                ChangeDirection(Directions.Down);
            }
            if (Input.GetKeyDown("d") && _currentDirection != Directions.Left)
            {
                ChangeDirection(Directions.Right);
            }
        }
        if (Input.GetKeyDown("escape"))
        {
            OnEscClick?.Invoke();
        }
    }
    private void ChangeDirection(Directions direction)
    {
        _currentDirection = direction;
        IsReady = false;
    }
}
