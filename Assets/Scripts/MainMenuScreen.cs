using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenuScreen : MonoBehaviour
{
    public TMP_InputField usernameInput;
    public static string username;
    public void OnPlayButtonPressed()
    {
        username = usernameInput.text;
        if (username == "")
        {
            username = "Noob" + Random.Range(0, 100000).ToString();
        }
        Loader.Load(Loader.Scene.GameScene);
    }
}
