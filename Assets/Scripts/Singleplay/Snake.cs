using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
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
    private Vector2 _direction;
    private Vector2 _trueposition;
    private float score = 0f;
    private List<Transform> _segments = new List<Transform>();
    public Transform segmentPrefab;
    public GameObject scoreEffect;
    [SerializeField] private float _speed = 10f;

    void Awake()
    {
        int rand_x = Random.Range(-10, 10);
        int rand_y = Random.Range(-10, 10);
        transform.position = new Vector3(rand_x, rand_y, 0);
        List<Vector2> directions = new List<Vector2> { Vector2.up, Vector2.down, Vector2.left, Vector2.right };
        _direction = directions[Random.Range(0, 4)];
        _trueposition = transform.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        // GameAsset gameAsset = GameObject.Find("GameAsset").GetComponent<GameAsset>();
        // GetComponent<SpriteRenderer>().sprite = gameAsset.GetHead();
        // segmentPrefab.GetComponent<SpriteRenderer>().sprite = gameAsset.GetSegment();
        _segments.Add(this.transform);
    }

    // Update is called once per frame
    // void Update()
    // {
    //     HandleInput();
    //     if (_state == State.Alive)
    //     {
            // UpdateSegments();
    //         Move();
    //     }
    // }

    void FixedUpdate()
    {
        HandleInput();
        if (_state == State.Alive)
        {
            UpdateSegments();
            Move();
        }
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
        if (_direction == Vector2.right)
        {
            transform.rotation = Quaternion.Euler(0, 0, -90);
        }
        else if (_direction == Vector2.down)
        {
            transform.rotation = Quaternion.Euler(0, 0, -180);
        }
        else if (_direction == Vector2.left)
        {
            transform.rotation = Quaternion.Euler(0, 0, -270);
        }
        else if (_direction == Vector2.up)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        _trueposition.x += _direction.x * _speed * Time.deltaTime;
        _trueposition.y += _direction.y * _speed * Time.deltaTime;

        if (_trueposition.x > GameManager.instance.gridArea.bounds.max.x)
        {
            _trueposition.x = GameManager.instance.gridArea.bounds.min.x + 0.5f;
        }
        else if (_trueposition.x < GameManager.instance.gridArea.bounds.min.x)
        {
            _trueposition.x = GameManager.instance.gridArea.bounds.max.x - 0.5f;
        }
        else if (_trueposition.y > GameManager.instance.gridArea.bounds.max.y)
        {
            _trueposition.y = GameManager.instance.gridArea.bounds.min.y + 0.5f;
        }
        else if (_trueposition.y < GameManager.instance.gridArea.bounds.min.y)
        {
            _trueposition.y = GameManager.instance.gridArea.bounds.max.y - 0.5f;
        }

        transform.position = new Vector3(
            Mathf.Round(_trueposition.x),
            Mathf.Round(_trueposition.y),
            0
        );
    }

    private void Grow(Collider2D collision)
    {
        Debug.Log("Eat food");
        Transform newSegment = Instantiate(segmentPrefab);
        GameObject popupscore = Instantiate(scoreEffect, transform.position, Quaternion.identity) as GameObject;
        Food food = collision.GetComponent<Food>();
        popupscore.transform.GetChild(0).GetComponent<TextMesh>().text = "+" + Mathf.RoundToInt(food.GetScore()).ToString();
        newSegment.position = _segments[_segments.Count - 1].position;
        _segments.Add(newSegment);
        // _speed += 1f;
        score += food.GetScore();
        Destroy(collision.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Food")
        {
            Grow(collision);
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

    public void SetDirection(Vector2 direction)
    {
        _direction = direction;
    }

    public Vector2 GetDirection()
    {
        return _direction;
    }

    public void SetSpeed(float speed)
    {
        _speed = speed;
    }

    public List<Transform> GetFullSegments()
    {
        List<Transform> fullSegments = new List<Transform>();
        fullSegments.Add(transform);
        fullSegments.AddRange(_segments);
        return fullSegments;
    }
}
