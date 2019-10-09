using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Database;
using Firebase;
using Firebase.Unity.Editor;

public class Register : MonoBehaviour
{
    public DatabaseReference reference;
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
        User user = new User(PlayerUsername, PlayerPassword, PlayerEmail);
        string json = JsonUtility.ToJson(user);
        reference.Child("user").Child(PlayerUsername).SetRawJsonValueAsync(json);
    }
}
