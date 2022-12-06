using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;
public class GameManager : MonoBehaviour
{
    public Food foodPrefab;
    public BoxCollider2D gridArea;

    public float init_score = 100f;
    public float init_foodTimer = 10f;
    public int food_num = 5;
    private float timeCounter = 0f;
    bool isStart = false;
    Food[] foods;
    public Snake snake;

    bool stopUpdate = false;

    public static GameManager instance;
    public GameOverScreen gameOverScreen;
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

        if (snake.IsAlive())
        {
            // Debug.Log("Snake is " + snake._state);
            // timeCounter += Time.deltaTime;
            foods = FindObjectsOfType<Food>();
            SpawnFood();
        }
        else
        {
            GameOver();
        }
    }

    public void SpawnFood()
    {
        if (foods.Length >= food_num)
        {
            return;
        }
        Bounds bounds = gridArea.bounds;
        int[,] grid = new int[(int)bounds.size.x, (int)bounds.size.y];
        for (int i = 0; i < bounds.size.x; i++)
        {
            for (int j = 0; j < bounds.size.y; j++)
            {
                grid[i, j] = 0;
            }
        }

        foreach (Transform segment in snake.GetFullSegments())
        {
            grid[(int)segment.position.x - (int)bounds.min.x, (int)segment.position.y - (int)bounds.min.y] = 1;
        }

        foreach (Food food in foods)
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
            return;
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
            GameObject go = Instantiate(foodPrefab.gameObject, new Vector3(position.x + bounds.min.x, position.y + bounds.min.y, 0), Quaternion.identity);
            Food food = go.GetComponent<Food>();
            food.SetInitScore(init_score);
            food.SetTotalTime(init_foodTimer);
        }
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
        // gameOverScreen.Show();
        int show_score = Mathf.RoundToInt(snake.GetScore());
        int high_score = 0;
        gameOverScreen.Show(show_score, 1, 1, 1);
        List<User> users = new List<User>();

        Task t = FirebaseDatabase.DefaultInstance.GetReference("users").GetValueAsync().ContinueWithOnMainThread(task =>
        {
            DataSnapshot snapshot = task.Result;
            foreach (DataSnapshot user in snapshot.Children)
            {
                string username = user.Child("username").Value.ToString();
                int thisuserscore = int.Parse(user.Child("score").Value.ToString());
                User u = new User(username, thisuserscore);
                users.Add(u);
            }
        });

        t.ContinueWithOnMainThread(task =>
        {
            foreach (User u in users)
            {
                if (u.username == SinglePlayMenu.username)
                {
                    print("Found user " + u.username + " with score " + u.score);
                    {
                        if (u.score < show_score)
                        {
                            u.score = show_score;
                            high_score = show_score;
                            FirebaseDatabase.DefaultInstance.GetReference("users").Child(u.username).Child("score").SetValueAsync(u.score);
                        }
                        else
                        { high_score = u.score; }
                    }
                }
            }
            
            Database.CreateUser(SinglePlayMenu.username, high_score);

            
            users.Sort((x, y) => y.score.CompareTo(x.score));
            int ranking = users.FindIndex(x => x.username == SinglePlayMenu.username) + 1;
            if (ranking == 1)
            {
                gameOverScreen.Show(show_score, high_score, ranking, 1000000000);
            }
            else
            {
                gameOverScreen.Show(show_score, high_score, ranking, users[ranking - 2].score);
            }
            stopUpdate = true;
        });
    }
}
