using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Database;
using Firebase;
using Firebase.Unity.Editor;

public class Register : MonoBehaviour
{
    public DatabaseReference reference;
    private Firebase.Auth.FirebaseUser _newUser;

    private void awake()
    {
        //Firebase.Auth.FirebaseAuth auth = Firebase.Auth.FirebaseAuth.DefaultInstance;

    }
    private void Start()
    {

        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://class-wars.firebaseio.com/.json");
        reference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public InputField RegisterUsernameText;
    public InputField RegisterPasswordText;
    public InputField RegisterEmailText;

    public static string PlayerUsername;
    public static string PlayerPassword;
    public static string PlayerEmail = null;

    public void OnSubmitRegisterButton()
    {
        PlayerUsername = RegisterUsernameText.text;
        PlayerPassword = RegisterPasswordText.text;
        PlayerEmail = RegisterEmailText.text;
        PostToDatabase();
    }
    private void PostToDatabase()
    {
        Firebase.Auth.FirebaseAuth auth = Firebase.Auth.FirebaseAuth.DefaultInstance;

        auth.CreateUserWithEmailAndPasswordAsync(PlayerEmail, PlayerPassword).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
            }

            //Firebase user has been created.
            _newUser = task.Result;
            Debug.LogFormat("Firebase user created successfully: {0} ({1})"
                    , _newUser.DisplayName, _newUser.UserId);
        });

        /*Firebase.Auth.Credential credential = Firebase.Auth.EmailAuthProvider.GetCredential(PlayerEmail, PlayerPassword);
        auth.SignInWithCredentialAsync(credential).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogError("SignInWithCredentialAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("SignInWithCredentialAsync encountered an error: " + task.Exception);
                return;
            }

            _newUser = task.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})",
                _newUser.DisplayName, _newUser.UserId);
            if (_newUser != null)
            {
                User user = new User(PlayerUsername, _newUser.UserId);
                string json = JsonUtility.ToJson(user);
                reference.Child("user").Child(_newUser.UserId).SetRawJsonValueAsync(json);
                Firebase.Auth.UserProfile profile = new Firebase.Auth.UserProfile
                {
                    DisplayName = PlayerUsername
                };
                _newUser.UpdateUserProfileAsync(profile).ContinueWith(tarea =>
                {
                    if (tarea.IsCanceled)
                    {
                        Debug.LogError("UpdateUserProfileAsync was canceled.");
                        return;
                    }

                    if (tarea.IsFaulted)
                    {
                        Debug.LogError("UpdateUserProfileAsync encountered an error: " + tarea.Exception);
                        return;
                    }

                    Debug.Log("User profile updated successfully.");
                    Debug.Log(_newUser.DisplayName);
                });
            }
        });*/
    }
}