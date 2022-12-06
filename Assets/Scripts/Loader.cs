using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loader{

    public enum Scene {
        MultiplayMenu,
        MultiplayScene,
        SingleplayMenu,
        SingleplayScene,
        Leaderboard,
        Loading,
        MainMenu,
    }

    public static void Load(Scene scene){
        SceneManager.LoadScene(scene.ToString());
    }
}
