﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Schema;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Database;
using Firebase;
using Firebase.Auth;
using Firebase.Unity.Editor;
using UnityEditor;
using UnityEngine.SceneManagement;

public class MenuHandling : MonoBehaviour
{
    public GameObject cat;
    public Button profileButton;
    public InputField UsernameInputField;
    private static string OtherFriendCode;

    public Text friendCodeText;
    private FirebaseAuth _auth;
    private FirebaseUser _currentUser;
    public DatabaseReference reference;
    public RectTransform prefab;
    public ScrollRect scrollView;
    public RectTransform content;

    public ArrayList friends = new ArrayList();
    public ArrayList friendsNames = new ArrayList();

    public VerticalLayoutGroup verticalLayoutGroup;
    public GameObject FriendsPF;
    
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
        StartCoroutine(Asdasd());
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
        Friendship friendship = new Friendship(_currentUser.UserId, OtherFriendCode,"Stand By");
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
                foreach (DataSnapshot friendship in snapshot.Children.ToArray())
                {
                    IDictionary dictFriend = (IDictionary) friendship.Value;
                    //Debug.Log(dictFriend["user1_id"] + "<- User1");
                    //Debug.Log(dictFriend["user2_id"] + "<- User2");
                    //Debug.Log(dictFriend["status"] + "<- status");
                    ArrayList asdtemp = new ArrayList();
                    asdtemp.Add(dictFriend["user1_id"]);
                    asdtemp.Add(dictFriend["user2_id"]);
                    asdtemp.Add(dictFriend["status"]);
                    friends.Add(asdtemp);
                }
                CheckFriendsNames();
            }
        });
    }

    public void CheckFriendsNames()
    {
        var userRefs = FirebaseDatabase.DefaultInstance.GetReference("user");
        Debug.Log("ACA2");
        Debug.Log(friends.ToArray()[1]);
        Debug.Log(friends.ToArray()[0]);
        foreach (ArrayList array in friends)
        {
            userRefs.OrderByChild("id").EqualTo(array[0].ToString()).GetValueAsync().ContinueWith(
                task =>
                {
                    if (task.IsFaulted)
                    {
                        // Handle the error...
                        Debug.Log("Error");
                    }
                    else if (task.IsCompleted)
                    {
                        DataSnapshot snapshot2 = task.Result;
                        foreach (DataSnapshot user in snapshot2.Children)
                        {
                            IDictionary dictUser = (IDictionary) user.Value;
                            //Debug.Log(dictUser["username"] + " <- Username");
                            //Debug.Log(dictUser["id"] + " <- Id");
                            ArrayList asdtemp2 = new ArrayList();
                            asdtemp2.Add(dictUser["username"]);
                            asdtemp2.Add(dictUser["id"]);
                            friendsNames.Add(asdtemp2);
                        }
                    }
                    
                });
        }

        
    }
    
    //funcion debug
    IEnumerator Asdasd()
    {
        yield return new WaitForSeconds(1);
        
        RectTransform parent = verticalLayoutGroup.GetComponent<RectTransform>();
        foreach (ArrayList array in friendsNames)
        {
            GameObject friend = Instantiate(FriendsPF, parent.transform, true);
            LoadFriendPFData script = friend.GetComponent<LoadFriendPFData>();
            script.ChangeUsernameText(array[0].ToString());
            script.AssignID(array[1].ToString());
        }
    }
}