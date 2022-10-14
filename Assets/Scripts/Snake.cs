using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Snake : MonoBehaviour
{
    public enum State
    {
        Alive,
        Dead
    }
    public State _state = State.Alive;
    private Vector2 _direction = Vector2.right;
    public BoxCollider2D gridArea;
    public GameOverScreen gameOverScreen;
    public Food food;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timeText;
    // public GameAsset gameAsset;
    private float score = 0f;
    private float speed = 1f;
    private float aliveTime = 0f;
    private List<Transform> _segments = new List<Transform>();
    public Transform segmentPrefab;

    void Awake()
    {
        gameOverScreen.Hide();
        score = 0f;
        speed = 1f;
        Time.fixedDeltaTime = 0.08f;
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Snake Start");
        GameAsset gameAsset = GameObject.Find("GameAsset").GetComponent<GameAsset>();
        this.GetComponent<SpriteRenderer>().sprite = gameAsset.GetHead();
        segmentPrefab.GetComponent<SpriteRenderer>().sprite = gameAsset.GetSegment();
        _segments.Add(this.transform);
    }

    // Update is called once per frame
    void Update()
    {
        if (_state == State.Alive)
        {
            aliveTime += Time.deltaTime;
            timeText.text = Mathf.RoundToInt(aliveTime).ToString();
            scoreText.text = Mathf.RoundToInt(score).ToString();
        }
    }

    void FixedUpdate()
    {
        HandleInput();
        if (_state == State.Alive)
        {
            UpdateSegments();
            Move();
        }
        // Debug.Log("Score: " + score);
        // Debug.Log("Food Score: " + food.GetScore());
    }

    private void HandleInput()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (_direction != Vector2.left)
            {
                _direction = Vector2.right;
            }
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            if (_direction != Vector2.up)
            {
                _direction = Vector2.down;
            }
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (_direction != Vector2.right)
            {
                _direction = Vector2.left;
            }
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            if (_direction != Vector2.down)
            {
                _direction = Vector2.up;
            }
        }
    }

    private void Move()
    {
        // _direction.x *= speed;
        // _direction.y *= speed;
        if (_direction == Vector2.right)
        {
            this.transform.rotation = Quaternion.Euler(0, 0, -90);
        }
        else if (_direction == Vector2.down)
        {
            this.transform.rotation = Quaternion.Euler(0, 0, -180);
        }
        else if (_direction == Vector2.left)
        {
            this.transform.rotation = Quaternion.Euler(0, 0, -270);
        }
        else if (_direction == Vector2.up)
        {
            this.transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        this.transform.position = new Vector3(
            Mathf.Round(this.transform.position.x + _direction.x),
            Mathf.Round(this.transform.position.y + _direction.y),
            0
        );

        if (this.transform.position.x > gridArea.bounds.max.x)
        {
            this.transform.position = new Vector3(gridArea.bounds.min.x + 0.5f, this.transform.position.y, 0);
        }
        else if (this.transform.position.x < gridArea.bounds.min.x)
        {
            this.transform.position = new Vector3(gridArea.bounds.max.x - 0.5f, this.transform.position.y, 0);
        }
        if (this.transform.position.y > gridArea.bounds.max.y)
        {
            this.transform.position = new Vector3(this.transform.position.x, gridArea.bounds.min.y + 0.5f, 0);
        }
        else if (this.transform.position.y < gridArea.bounds.min.y)
        {
            this.transform.position = new Vector3(this.transform.position.x, gridArea.bounds.max.y - 0.5f, 0);
        }
    }

    private void Grow()
    {
        Transform newSegment = Instantiate(segmentPrefab);
        newSegment.position = _segments[_segments.Count - 1].position;
        _segments.Add(newSegment);
        score += food.GetScore();
        Time.fixedDeltaTime *= 0.98f;
        // speed *= 1.02f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Food")
        {
            Grow();
        }
        else if (collision.gameObject.tag == "Segment")
        {
            Die();
        }
    }

    private void UpdateSegments()
    {
        for (int i = _segments.Count - 1; i > 0; i--)
        {
            _segments[i].position = _segments[i - 1].position;
        }
        _segments[0].position = this.transform.position;
    }

    public void Die()
    {
        if (_state == State.Alive)
        {
            _state = State.Dead;
            gameOverScreen.Show(score);
            Database.CreateUser(MainMenuScreen.username, Mathf.RoundToInt(score));
        }
    }

    public bool IsAlive()
    {
        return _state == State.Alive;
    }

    public float GetScore()
    {
        return score;
    }
}
