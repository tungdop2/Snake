using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class OGameManager : MonoBehaviour
{
    public OFood oFoodPrefab;
    public BoxCollider2D gridArea;

    public float score = 100f;
    public float foodTimer = 10f;
    public int food_num = 2;
    bool stopUpdate = false;
    OFood[] foods;
    OSnake[] snakes;
    PhotonView view;
    bool win = true;

    public static OGameManager instance;
    public OGameOverScreen gameOverScreen;
    void Awake()
    {
        gameOverScreen.Hide();
        instance = this;
    }

    void Start()
    {
    }

    void FixedUpdate()
    {
        if (stopUpdate)
        {
            return;
        }

        if (isOver())
        {
            // GameOver();
        }
        else
        {
            // timeCounter += Time.deltaTime;
            snakes = FindObjectsOfType<OSnake>();
            // don't start until there is at least 2 snakes
            foods = FindObjectsOfType<OFood>();
            if (snakes.Length >= 2)
            {
                SpawnFood();
            }
        }
    }

    public void SpawnFood()
    {
        if (foods.Length >= food_num)
        {
            return ;
        }
        Bounds bounds = gridArea.bounds;
        int[,] grid  = new int[(int)bounds.size.x, (int)bounds.size.y];
        for (int i = 0; i < bounds.size.x; i++)
        {
            for (int j = 0; j < bounds.size.y; j++)
            {
                grid[i, j] = 0;
            }
        }

        foreach (OSnake snake in snakes)
        {
            foreach (Transform segment in snake.GetFullSegments())
            {
                grid[(int)segment.position.x - (int)bounds.min.x, (int)segment.position.y - (int)bounds.min.y] = 1;
            }
        }

        foreach (OFood food in foods)
        {
            grid[(int)food.transform.position.x - (int)bounds.min.x, (int)food.transform.position.y - (int)bounds.min.y] = 1;
        }

        List<Vector2> emptyCells = new List<Vector2>();
        for (int i = 0; i < bounds.size.x; i++)
        {
            for (int j = 0; j < bounds.size.y; j++)
            {
                if (grid[i, j] == 0)
                {
                    emptyCells.Add(new Vector2(i, j));
                }
            }
        }

        if (emptyCells.Count == 0)
        {
            return ;
        }

        for (int i = 0; i < emptyCells.Count; i++)
        {
            Vector2 temp = emptyCells[i];
            int randomIndex = Random.Range(i, emptyCells.Count);
            emptyCells[i] = emptyCells[randomIndex];
            emptyCells[randomIndex] = temp;
        }

        for (int i = 0; i < food_num - foods.Length; i++)
        {
            Vector2 position = emptyCells[i];
            GameObject go = (GameObject) PhotonNetwork.Instantiate(oFoodPrefab.name, new Vector2(position.x + bounds.min.x, position.y + bounds.min.y), Quaternion.identity);
            OFood food = go.GetComponent<OFood>();
            food.SetInitScore(score);
            food.SetTotalTime(foodTimer);
        }
    }

    bool isOver()
    {
        for (int i = 0; i < snakes.Length; i++)
        {
            if (!snakes[i].IsAlive())
            {
                if (snakes[i].IsMine())
                {
                    win = false;
                }
                return true;
            }
        }
        return false;
    }

    void GameOver()
    {
        if (win)
        {
            gameOverScreen.Show("You Win!");
        }
        else
        {
            gameOverScreen.Show("You Lose!");
        }
        stopUpdate = true;
    }
}
