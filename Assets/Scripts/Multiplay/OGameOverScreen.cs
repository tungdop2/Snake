using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OGameOverScreen : MonoBehaviour
{
    public TextMeshProUGUI stateText;


    public void Show(string state)
    {
        stateText.text = state;

        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void OnRestartButtonPressed()
    {
        Loader.Load(Loader.Scene.MultiplayScene);
    }

    public void OnMenuButtonPressed()
    {
        Loader.Load(Loader.Scene.MultiplayMenu);
    }
}
