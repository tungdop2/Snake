using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// using Photon.Pun;

public class MainMenu : MonoBehaviour
{
    public Button singleplayButton;
    public Button multiplayButton;

    void Awake()
    {
        singleplayButton.onClick.AddListener(() => OnSinglePlayButtonPressed());
        multiplayButton.onClick.AddListener(() => OnMultiPlayButtonPressed());
    }

    public void OnSinglePlayButtonPressed()
    {
        Loader.Load(Loader.Scene.SingleplayMenu);
    }

    public void OnMultiPlayButtonPressed()
    {
        Loader.Load(Loader.Scene.Loading);
    }
}
