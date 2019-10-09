using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Database;
using Firebase;
using Firebase.Unity.Editor;
using UnityEngine.SceneManagement;

public class MenuHandling : MonoBehaviour
{
    public GameObject cat;
    public Button profileButton;
    private string current_user_username;
    public InputField UsernameInputField;
    public static string FriendUsername;
    public DatabaseReference reference;

    void Start()
    {
        cat.SetActive(false);
        current_user_username = PlayerPrefs.GetString("username");
        profileButton.GetComponentInChildren<Text>().text = current_user_username;
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://class-wars.firebaseio.com/.json");
        reference = FirebaseDatabase.DefaultInstance.RootReference;
        CheckFriendships(current_user_username);

    }
    public void Profile_picPressed()
    {
        cat.SetActive(true);
    }

    public void Profile_close()
    {
        cat.SetActive(false);
    }

    public void onClickAddFriendButton()
    {
        FriendUsername = UsernameInputField.text;
        AddFriendDB();
    }

    public void Logout_btn()
    {

    }
    
    private void AddFriendDB()
    {
        Friendship friendship = new Friendship(current_user_username, FriendUsername);
        string json = JsonUtility.ToJson(friendship);
        reference.Child("friendship").Child(current_user_username + " | " + FriendUsername).SetRawJsonValueAsync(json);
    }
    
    public void CheckFriendships(string u)
    {
        
        var friendship_refs = FirebaseDatabase.DefaultInstance.GetReference("friendship");
        friendship_refs.OrderByChild("username2").EqualTo(u).GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                // Handle the error...
                Debug.Log("Error");
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                foreach (DataSnapshot friendship in snapshot.Children)
                {
                    IDictionary dictFriend = (IDictionary) friendship.Value;
                    Debug.Log(dictFriend["username1"] + "<- User1");
                    Debug.Log(dictFriend["username2"] + "<- User2");
                    Debug.Log(dictFriend["status"] + "<- status");
                }
            }
            
            
        });
    }

}
