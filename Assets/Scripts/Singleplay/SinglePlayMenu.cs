using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SinglePlayMenu : MonoBehaviour
{
    public TMP_InputField usernameInput;
    public static string username;

    public Button playButton;
    public Button leaderboardButton;
    public Button backButton;

    void Awake()
    {
        usernameInput.text = "Noob" + Random.Range(0, 100000).ToString();
        playButton.onClick.AddListener(() => OnPlayButtonPressed());
        leaderboardButton.onClick.AddListener(() => OnLeaderboardButtonPressed());
        backButton.onClick.AddListener(() => OnBackButtonPressed());
    }

    void OnPlayButtonPressed()
    {
        username = usernameInput.text;
        Loader.Load(Loader.Scene.SingleplayScene);
    }

    void OnLeaderboardButtonPressed()
    {
        Loader.Load(Loader.Scene.Leaderboard);
    }

    void OnBackButtonPressed()
    {
        Loader.Load(Loader.Scene.MainMenu);
    }
}
