using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Database;
using Firebase;
using Firebase.Auth;
using Firebase.Unity.Editor;
using UnityEngine.SceneManagement;

public class MenuHandling : MonoBehaviour
{
    public GameObject cat;
    public Button profileButton;
    public InputField UsernameInputField;
    public static string OtherFriendCode;

    public Text friendCodeText;

    private FirebaseAuth _auth;
    private FirebaseUser _currentUser;
    public DatabaseReference reference;

    private void Awake()
    {

    }

    void Start()
    {
        _auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        _currentUser = _auth.CurrentUser;
        cat.SetActive(false);
        profileButton.GetComponentInChildren<Text>().text = _currentUser.DisplayName;
        friendCodeText.text = "Your friend code is: " + _currentUser.UserId;
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://class-wars.firebaseio.com/.json");
        reference = FirebaseDatabase.DefaultInstance.RootReference;
        CheckFriendships(_currentUser.UserId);


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
        OtherFriendCode = UsernameInputField.text;
        AddFriendDB();
    }

    public void Logout_btn()
    {
        _auth.SignOut();
        SceneManager.LoadScene("Scenes/LoginScene");
    }

    private void AddFriendDB()
    {
        Friendship friendship = new Friendship(_currentUser.UserId, OtherFriendCode);
        string json = JsonUtility.ToJson(friendship);
        reference.Child("friendship").Child(_currentUser.UserId + " | " + OtherFriendCode).SetRawJsonValueAsync(json);
    }

    public void CheckFriendships(string u)
    {

        var friendshipRefs = FirebaseDatabase.DefaultInstance.GetReference("friendship");
        friendshipRefs.OrderByChild("user2_id").EqualTo(u).GetValueAsync().ContinueWith(task =>
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
                    IDictionary dictFriend = (IDictionary)friendship.Value;
                    Debug.Log(dictFriend["user1_id"] + "<- User1");
                    Debug.Log(dictFriend["user2_id"] + "<- User2");
                    Debug.Log(dictFriend["status"] + "<- status");
                }
            }


        });
    }

    public class ExampleItemView
    {
        public Text usernameText;

        public ExampleItemView(Transform rootView)
        {
            usernameText = rootView.Find("TitlePanel/UsernameText").GetComponent<Text>();
        }
    }

    public class ExampleItemModel
    {
        public string username;
    }

}