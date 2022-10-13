using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    void Awake()
    {
        Debug.Log("MainMenu Awake");
    }
    public void OnPlayButtonPressed()
    {
        Debug.Log("Play button pressed");
        SceneManager.LoadScene("GameScene");
    }
}
