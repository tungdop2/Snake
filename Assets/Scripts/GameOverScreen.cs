using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverScreen : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    public void Show(float score)
    {
        scoreText.text = "Score: " + Mathf.RoundToInt(score);
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void OnRestartButtonPressed()
    {
        Loader.Load(Loader.Scene.GameScene);
    }
}
