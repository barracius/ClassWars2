using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Database;
using Firebase;
using Firebase.Unity.Editor;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour
{
    public DatabaseReference reference;
    private void Start()
    {
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://class-wars.firebaseio.com/.json");
        reference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public InputField LoginUsernameText;
    public InputField LoginPasswordText;

    public static string PlayerUsername;
    public static string PlayerPassword;
    public static bool isLoggedin = false;
    private User current_user;

    public void OnSubmitLoginButton()
    {
        PlayerUsername = LoginUsernameText.text;
        PlayerPassword = LoginPasswordText.text;
        LoginToDatabase(PlayerUsername, PlayerPassword);
    }

    public void onLogin(User user)
    {
        Debug.Log("whot");
        PlayerPrefs.SetString("username", user.username);
        Debug.Log(PlayerPrefs.GetString("username"));
        SceneManager.LoadScene("Scenes/MenuScene");

    }

    public void LoginToDatabase(string u, string p)
    {
        
        var userref = FirebaseDatabase.DefaultInstance.GetReference("user");
        userref.OrderByChild("username").EqualTo(u).GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                // Handle the error...
                Debug.Log("Error");
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                foreach (DataSnapshot user in snapshot.Children)
                {
                    IDictionary dictUser = (IDictionary) user.Value;
                    if (u == dictUser["username"].ToString() && p == dictUser["password"].ToString())
                    {
                        current_user = new User(dictUser["username"].ToString(), dictUser["password"].ToString(),
                            dictUser["email"].ToString());
                        isLoggedin = true;
                    }
                }
            }
            
            
        });
        if (isLoggedin)
        {
            onLogin(current_user);
        }
    }
}