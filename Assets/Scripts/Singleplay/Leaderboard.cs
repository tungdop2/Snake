using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using TMPro;

public class Leaderboard : MonoBehaviour
{
    public Button playButton;
    public Button backButton;

    public List<TextMeshProUGUI> leads;

    // Start is called before the first frame update
    void Awake()
    {
        playButton.onClick.AddListener(() => OnPlayButtonPressed());
        backButton.onClick.AddListener(() => OnBackButtonPressed());

        for (int i = 0; i < leads.Count; i++)
        {
            leads[i].text = "";
        }
    }

    // Update is called once per frame
    void Start()
    {
        List<User> users = new List<User>();

        Task t = FirebaseDatabase.DefaultInstance.GetReference("users").GetValueAsync().ContinueWithOnMainThread(task =>
        {
            DataSnapshot snapshot = task.Result;
            foreach (DataSnapshot user in snapshot.Children)
            {
                string username = user.Child("username").Value.ToString();
                int thisuserscore = int.Parse(user.Child("score").Value.ToString());
                User u = new User(username, thisuserscore);
                users.Add(u);
            }
        });

        t.ContinueWithOnMainThread(task =>
        {
            Debug.Log("Number of users: " + users.Count);      
            users.Sort((x, y) => y.score.CompareTo(x.score));
            for (int i = 0; i < Mathf.Min(3, users.Count); i++)
            {
                Debug.Log(users[i].username + " " + users[i].score);
                leads[i].text = (i + 1) + ". " + users[i].username + " " + users[i].score;
            }
        });
    }

    void OnPlayButtonPressed()
    {
        Loader.Load(Loader.Scene.SingleplayScene);
    }

    void OnBackButtonPressed()
    {
        Loader.Load(Loader.Scene.SingleplayMenu);
    }

}
