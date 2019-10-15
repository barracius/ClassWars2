using System;
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
    private int count;
    private int size = -1;
    public bool parch;
    public bool parch2;
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

    void Start()
    {
        _auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        _currentUser = _auth.CurrentUser;
        cat.SetActive(false);
        profileButton.GetComponentInChildren<Text>().text = _currentUser.DisplayName;
        friendCodeText.text = "Your friend code is: " + _currentUser.UserId;
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://class-wars.firebaseio.com/.json");
        reference = FirebaseDatabase.DefaultInstance.RootReference;
        CheckFriendships1(_currentUser.UserId);
    }

    private void Update()
    {
        if (parch)
        {
            CheckFriendships2(_currentUser.UserId);
            parch = false;
        }
        if (parch2)
        {
            CheckFriendsNames();
            parch2 = false;
        }

        if (size == count)
        {
            FillFriendList();
            count = -2;
        }
        
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

    public void CheckFriendships1(string u)
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
                    if (dictFriend["status"].ToString() != "Rejected")
                    {
                        asdtemp.Add(dictFriend["user1_id"]);
                        asdtemp.Add(dictFriend["user2_id"]);
                        asdtemp.Add(dictFriend["status"]);
                        friends.Add(asdtemp);
                    }
                }
                

                parch = true;

            }
        });
        
    }
    public void CheckFriendships2(string u)
    {
        var friendshipRefs = FirebaseDatabase.DefaultInstance.GetReference("friendship");
        friendshipRefs.OrderByChild("user1_id").EqualTo(u).GetValueAsync().ContinueWith(task =>
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
                    if (dictFriend["status"].ToString() == "Friends" )
                    {
                        asdtemp.Add(dictFriend["user2_id"]);
                        asdtemp.Add(dictFriend["user1_id"]);
                        asdtemp.Add(dictFriend["status"]);
                        friends.Add(asdtemp);
                    }
                }
                

                parch2 = true;

            }
        });
        
    }
    
    public void CheckFriendsNames()
    {
        var userRefs = FirebaseDatabase.DefaultInstance.GetReference("user");
        //Debug.Log("ACA2");
        size = friends.Count;
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
                            //Debug.Log(array[2]);
                            ArrayList asdtemp2 = new ArrayList();
                            asdtemp2.Add(dictUser["username"]);
                            asdtemp2.Add(dictUser["id"]);
                            asdtemp2.Add(array[2]);
                            friendsNames.Add(asdtemp2);
                        }
                    }
                    System.Threading.Interlocked.Increment(ref count);

                });
        }

        
    }
    
    void FillFriendList()
    {
        RectTransform parent = verticalLayoutGroup.GetComponent<RectTransform>();
        foreach (ArrayList array in friendsNames)
        {
            GameObject friend = Instantiate(FriendsPF, parent.transform, true);
            LoadFriendPFData script = friend.GetComponent<LoadFriendPFData>();
            //Debug.Log(array[2]);
            if (array[2].ToString() == "Friends")
            {
                script.HideButtons();
            }
            script.AssignData(array[1].ToString(), array[0].ToString(), _currentUser.UserId);
            script.ChangeUsernameText();
        }
    }
}