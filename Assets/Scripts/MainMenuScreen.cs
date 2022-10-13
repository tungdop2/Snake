using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MainMenuScreen : MonoBehaviour
{
    public void OnPlayButtonPressed()
    {
        Loader.Load(Loader.Scene.GameScene);
    }
}
