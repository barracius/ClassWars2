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
    // private FirebaseApp.Auth.FirebaseAuth auth;


    // void Awake(){

    // }
    private void Start()
    {
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://class-wars.firebaseio.com/.json");
        reference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    private void Update()
    {
        Firebase.Auth.FirebaseAuth auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        Firebase.Auth.FirebaseUser user = auth.CurrentUser;
        if (user != null && user.DisplayName != "") {
            SceneManager.LoadScene("Scenes/MenuScene");
        }
    }

    public InputField LoginEmailText;
    public InputField LoginPasswordText;

    public static string PlayerEmail;
    public static string PlayerPassword;
    public static bool isLoggedin = false;
    private User current_user;

    public void OnSubmitLoginButton()
    {
        PlayerEmail = LoginEmailText.text;
        PlayerPassword = LoginPasswordText.text;
        LoginToDatabase(PlayerEmail, PlayerPassword);
    }

    public void onLogin(User user)
    {

        // Debug.Log("whot");
        // PlayerPrefs.SetString("username", user.username);
        // Debug.Log(PlayerPrefs.GetString("username"));
        //SceneManager.LoadScene("Scenes/MenuScene");

    }

    public void LoginToDatabase(string u, string p)
    {
        Firebase.Auth.FirebaseAuth auth = Firebase.Auth.FirebaseAuth.DefaultInstance;

        auth.SignInWithEmailAndPasswordAsync(PlayerEmail, PlayerPassword).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);
            }

            //Firebase user has been created.
            Firebase.Auth.FirebaseUser newUser = task.Result;
            Debug.LogFormat("Firebase user logged in successfully: {0} ({1})"
                    , newUser.DisplayName, newUser.UserId);
        });

        // var userref = FirebaseDatabase.DefaultInstance.GetReference("user");
        // userref.OrderByChild("username").EqualTo(u).GetValueAsync().ContinueWith(task =>
        // {
        //     if (task.IsFaulted)
        //     {
        //         // Handle the error...
        //         Debug.Log("Error");
        //     }
        //     else if (task.IsCompleted)
        //     {
        //         DataSnapshot snapshot = task.Result;
        //         foreach (DataSnapshot user in snapshot.Children)
        //         {
        //             IDictionary dictUser = (IDictionary)user.Value;
        //             if (u == dictUser["username"].ToString() && p == dictUser["password"].ToString())
        //             {
        //                 current_user = new User(dictUser["username"].ToString(), dictUser["password"].ToString(),
        //                     dictUser["email"].ToString());
        //                 isLoggedin = true;
        //             }
        //         }
        //     }


        // });
    }
    
}