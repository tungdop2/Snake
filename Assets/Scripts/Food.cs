using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Food : MonoBehaviour
{
    public BoxCollider2D gridArea;
    public Snake snake;
    // Start is called before the first frame update
    public float totalTime = 10f;
    private float aliveTime;
    // public TextMeshProUGUI remainTimeText;
    private float score = 100f;
    private float cur_score = 0f;
    void Start()
    {
        Respawn();
    }

    // Update is called once per frame
    void Update()
    {
        // scale the food down over time
        aliveTime += Time.deltaTime;
        // remainTimeText.text = (totalTime - aliveTime);
        float scale = 1f - aliveTime / totalTime;
        if (scale < 0)
            snake.Die();
        if (snake.IsAlive())
        {
            transform.localScale = new Vector3(scale, scale, 1f);
            transform.Rotate(0, 0, 360 * Time.deltaTime);
            score = 100f * scale;
        }
        // Debug.Log(score);
        cur_score = score;
    }

    private void Respawn()
    {
        Bounds bounds = gridArea.bounds;
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);
        this.transform.position = new Vector3(Mathf.Round(x), Mathf.Round(y), 0);

        aliveTime = 0;
        transform.localScale = Vector3.one;
        score = 100f;
        totalTime *= 0.98f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Respawn();
        }
    }

    public float GetScore()
    {
        return cur_score;
    }
}
