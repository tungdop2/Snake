using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Food : MonoBehaviour
{
    [SerializeField] public float totalTime = 10f;
    private float aliveTime;
    private float score = -1f;
    private float init_score = 100f;

    void Start()
    {
        Destroy(gameObject, totalTime);
    }

    void Update()
    {
        aliveTime += Time.deltaTime;
        float scale = 1f - aliveTime / totalTime;
        transform.localScale = new Vector3(scale, scale, 1f);
        // transform.Rotate(0, 0, 360 * Time.deltaTime);
        score = init_score * scale;
    }

    public float GetScore()
    {
        return score;
    }

    public void SetInitScore(float score)
    {
        this.init_score = score;
    }

    public void SetTotalTime(float time)
    {
        this.totalTime = time;
    }
}
