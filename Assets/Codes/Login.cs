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
    public static bool parch = false;

    public void OnSubmitLoginButton()
    {
        PlayerUsername = LoginUsernameText.text;
        PlayerPassword = LoginPasswordText.text;
        LoginToDatabase(PlayerUsername, PlayerPassword);
    }

    public void What()
    {
        Debug.Log("whot");
        PlayerPrefs.SetInt("n",3);
        Debug.Log(PlayerPrefs.GetInt("n").ToString());
        SceneManager.LoadScene("Scenes/MenuScene");

    }

    IEnumerator Wait()
    {
        yield return new WaitForSecondsRealtime(4);
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
                        User userlogged = new User(dictUser["username"].ToString(), dictUser["password"].ToString(),
                            dictUser["email"].ToString());
                        Debug.Log("what");
                        
                        parch = true;
                        Debug.Log("whet");
                    }
                }
            }
            
            
        });
        Wait();
        Debug.Log("whit");
        if (parch)
        {
            Debug.Log("Whyt");
            What();
        }
    }
}