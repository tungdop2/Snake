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
    private float score = 0f;
    private float speed = 1f;
    private List<Transform> _segments = new List<Transform>();
    public Transform segmentPrefab;

    void Awake()
    {
        gameOverScreen.Hide();
        score = 0f;
        speed = 1f;
    }

    // Start is called before the first frame update
    void Start()
    {
        _segments.Add(this.transform);
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
    }

    void FixedUpdate()
    {
        if (_state == State.Alive)
        {
            UpdateSegments();
            Move();
        }
        Debug.Log("Score: " + score);
        Debug.Log("Food Score: " + food.GetScore());
    }

    private void HandleInput()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            _direction = Vector2.right;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            _direction = -Vector2.up;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            _direction = -Vector2.right;
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            _direction = Vector2.up;
        }
    }

    private void Move()
    {
        // _direction.x *= speed;
        // _direction.y *= speed;
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
        scoreText.text = Mathf.RoundToInt(score).ToString();
        // speed *= 1.02f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Food")
        {
            Grow();
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
        _state = State.Dead;
        gameOverScreen.Show(score);
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
