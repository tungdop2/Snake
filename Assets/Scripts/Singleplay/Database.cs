using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;

public class Database : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public static void CreateUser(string username, int score)
    {
        User user = new User(username, score);
        string json = JsonUtility.ToJson(user);
        Debug.Log(json);
        FirebaseDatabase.DefaultInstance.GetReference("users").Child(username).SetRawJsonValueAsync(json);
    }
}
