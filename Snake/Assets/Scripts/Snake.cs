using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Snake : MonoBehaviour
{
    public GameObject tile;
    public GameObject[] alltiles;
    public int numberofTiles;
    public int numberofRow;
    public int numberofColumn;
    public int distance;
    public float x1, x2, y1;
    public int t, t1 = 20;
    public int dir, dir1;
    public int curTile;
    public int numOfPart = 3;
    public bool lose = true;
    public bool ready = false;
    public bool walls;
    public Text dispscore;
    public int score;
    public GameObject end;

    // Start is called before the first frame update
    void StartGame()
    {
        if(walls == true)
        {
            numberofRow += 2;
            numberofColumn += 2;    
        }
        numberofTiles = numberofRow * numberofColumn;
        x1 = 0.5f - numberofColumn * 1.0f / 2 ;
        y1 = 0.5f + numberofRow * 1.0f / 2;
        x2 = x1;
        alltiles = new GameObject[numberofTiles];

        CreateTiles();
        FoodSpawn();
        SpawnSnake();
    }

    public void Inf(int numofColumn, int numofRow, bool wall, bool start)
    {
        numberofColumn = numofColumn;
        numberofRow = numofRow;
        walls = wall;
        StartGame();
        ready = start;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
            if (Input.GetKey("w") && dir1 != 3)
            {
                dir = 1;
                if (ready == true)
                {
                    lose = false;
                }
            }
            if (Input.GetKey("a") && dir1 != 4)
            {
                dir = 2;
                if (ready == true)
                {
                    lose = false;
                }
            }
            if (Input.GetKey("s") && dir1 != 1)
            {
                dir = 3;
                if (ready == true)
                {
                    lose = false;
                }
            }
            if (Input.GetKey("d") && dir1 != 2)
            {
                dir = 4;
                if (ready == true)
                {
                    lose = false;
                }
            }
        if (t >= t1 && lose != true)
        {
            SnakePartMove();
            if (dir == 1)
            {
                if (curTile >= numberofColumn)
                {
                    curTile -= numberofRow;
                    SnakeMove(curTile);
                }
                else
                {
                    curTile += numberofTiles - numberofColumn;
                    SnakeMove(curTile);
                }
            }
            if (dir == 2)
            {
                if (curTile % numberofColumn != 0)
                {
                    curTile -= 1;
                    SnakeMove(curTile);
                }
                else
                {
                    curTile += numberofColumn - 1;
                    SnakeMove(curTile);
                }
            }
            if (dir == 3)
            {
                if (curTile < numberofTiles - numberofColumn)
                {
                    curTile += numberofColumn;
                    SnakeMove(curTile);
                }
                else
                {
                    curTile -= numberofTiles - numberofColumn;
                    SnakeMove(curTile);
                }
            }
            if (dir == 4)
            {
                if ((curTile + 1) % numberofColumn != 0)
                {
                    curTile += 1;
                    SnakeMove(curTile);
                }
                else
                {
                    curTile -= numberofColumn - 1;
                    SnakeMove(curTile);
                }
            }
            dir1 = dir;
            t = 0;
        }
        t++;
        if (Input.GetKeyDown("escape"))
        {
            lose = !lose;
            if (lose == true) {
                dispscore.gameObject.SetActive(true);
            }
            else
            {
                dispscore.gameObject.SetActive(false);
            }
        }
    }

    void CreateTiles()
    {
        for (int createdTiles = 0; createdTiles < numberofTiles; createdTiles++)
        {
            x1 += distance;
            if (createdTiles % numberofColumn == 0)
            {
                y1 -= distance;
                x1 = x2;
            }
            alltiles[createdTiles] = Instantiate(tile, new Vector3(transform.position.x + x1, transform.position.y + y1, transform.position.z), transform.rotation);
            if (walls == true && createdTiles >= 0 && createdTiles < numberofColumn)
            {
                alltiles[createdTiles].gameObject.GetComponent<Tile>().checkNum = -4;
                alltiles[createdTiles].gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 0, 1, 1);
            }
            if (walls == true && ((createdTiles + 1) % numberofColumn == 0 || createdTiles % numberofColumn == 0))
            {
                alltiles[createdTiles].gameObject.GetComponent<Tile>().checkNum = -4;
                alltiles[createdTiles].gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 0, 1, 1);
            }
            if (walls == true && createdTiles >= numberofTiles - numberofColumn)
            {
                alltiles[createdTiles].gameObject.GetComponent<Tile>().checkNum = -4;
                alltiles[createdTiles].gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 0, 1, 1);
            }
        }
    }
    void SpawnSnake()
    {
        int randNum = Random.Range(0, numberofTiles);
        if (alltiles[randNum].gameObject.GetComponent<Tile>().checkNum == 0)
        {
            curTile = randNum;
            alltiles[randNum].gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1);
            alltiles[randNum].gameObject.GetComponent<Tile>().checkNum = 1;
        }
        else
        {
            SpawnSnake();
        }
    }
    void SnakeMove(int direct)
    {
        if (alltiles[direct].gameObject.GetComponent<Tile>().checkNum < 1 && alltiles[direct].gameObject.GetComponent<Tile>().checkNum > -3)
        {
            alltiles[direct].gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1);
            alltiles[direct].gameObject.GetComponent<Tile>().checkNum += 1;
            if (alltiles[direct].gameObject.GetComponent<Tile>().checkNum == -1)
            {
                numOfPart++;
                alltiles[direct].gameObject.GetComponent<Tile>().checkNum = 1;
                FoodSpawn();
                ScoreDraw();
            }
        }
        else
        {
            lose = true;
            end.gameObject.SetActive(true);
            end.GetComponent<End>().Message(score);
        }
    }
    void SnakePartMove()
    {
        for ( int tail = 0; tail < numberofTiles; tail++)
        {
            if(alltiles[tail].gameObject.GetComponent<Tile>().checkNum >= numOfPart)
            {
                alltiles[tail].gameObject.GetComponent<Tile>().checkNum = 0;
                alltiles[tail].gameObject.GetComponent<SpriteRenderer>().color = new Color(0.682353f, 0.682353f, 0.682353f, 1);
            }
            if (alltiles[tail].gameObject.GetComponent<Tile>().checkNum <= numOfPart && alltiles[tail].gameObject.GetComponent<Tile>().checkNum >= 1)
            {
                alltiles[tail].gameObject.GetComponent<Tile>().checkNum += 1;
            }
        }
    }
    void FoodSpawn()
    {
        int rand = Random.Range(0, numberofTiles);
        if (alltiles[rand].gameObject.GetComponent<Tile>().checkNum == 0)     
        {
            alltiles[rand].gameObject.GetComponent<Tile>().checkNum = -2;
            alltiles[rand].gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 1, 0, 1);
        }
        else
        {
            FoodSpawn();
        }
    }
    void ScoreDraw()
    {
        score += 1;
        dispscore.text = score.ToString();
        if (score < 25 && score % 5 == 0)
        {
            t1 -= 2;
        }
    }
}
