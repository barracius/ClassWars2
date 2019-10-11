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
    private Firebase.Auth.FirebaseAuth auth;
    private Firebase.Auth.FirebaseUser newUser;
    private FirebaseApp app = FirebaseApp.DefaultInstance;

    void Awake()
    {
        // Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        // {
        //     var dependencyStatus = task.Result;
        //     if (dependencyStatus == Firebase.DependencyStatus.Available)
        //     {
        //         // Create and hold a reference to your FirebaseApp,
        //         // where app is a Firebase.FirebaseApp property of your application class.
        //         //FirebaseApp app = FirebaseApp.DefaultInstance;

        //         // Set a flag here to indicate whether Firebase is ready to use by your app.
        //     }
        //     else
        //     {
        //         UnityEngine.Debug.LogError(System.String.Format(
        //         "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
        //         // Firebase Unity SDK is not safe to use here.
        //     }
        // });
    }
    private void Start()
    {
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://class-wars.firebaseio.com/.json");
        reference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    private void Update()
    {
        Firebase.Auth.FirebaseAuth auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        Firebase.Auth.FirebaseUser user = auth.CurrentUser;
        if (user != null)
        {
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

    }

}