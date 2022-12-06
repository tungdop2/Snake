using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverScreen : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highscoreText;
    public TextMeshProUGUI rankingText;
    public TextMeshProUGUI nextscoreText;


    public void Show(int score, int highscore, int ranking, int nextscore)
    {
        scoreText.text = "Score: " + score;
        highscoreText.text = "High score: " + highscore;
        rankingText.text = "Rank: " + ranking;
        nextscoreText.text = "Next score: " + nextscore;

        gameObject.SetActive(true);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void OnRestartButtonPressed()
    {
        Loader.Load(Loader.Scene.SingleplayScene);
    }

    public void OnMenuButtonPressed()
    {
        Loader.Load(Loader.Scene.SingleplayMenu);
    }
}
