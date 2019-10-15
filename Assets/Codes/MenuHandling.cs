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
    public GameObject newGameCanvas;
    public Button profileButton;
    public InputField UsernameInputField;
    private static string OtherFriendCode;

    public Text friendCodeText;
    private FirebaseAuth _auth;
    private FirebaseUser _currentUser;
    public DatabaseReference reference;

    public ArrayList dbNotRejectedFriends = new ArrayList();
    public ArrayList dbFriendRequests = new ArrayList();
    public ArrayList friends = new ArrayList();

    public VerticalLayoutGroup content;
    public GameObject FriendsPF;

    public Text Player1Text;
    public Text Player2Text;

    public InputField LobbyNameInputField;
    public Dropdown MaxTurnsDropdown;
    public Dropdown MaxTurnDurationDropdown;

    private string invitedPlayerId;
    private string invitedPlayerName;

    private int MaxTurns;
    private int MaxTurnDuration;
    private string LobbyName;



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

    public void onNewGameButton()
    {
        Player1Text.text = "Player 1: " + _currentUser.DisplayName;
        newGameCanvas.SetActive(true);
        foreach (GameObject friend in friends)
        {
            LoadFriendPFData script = friend.GetComponent<LoadFriendPFData>();
            script.InviteButtonAppearance();
        }
    }

    public void onCreateButton()
    {
        MaxTurns = int.Parse(MaxTurnsDropdown.options[MaxTurnsDropdown.value].text);
        MaxTurnDuration = int.Parse(MaxTurnDurationDropdown.options[MaxTurnDurationDropdown.value].text);
        LobbyName = LobbyNameInputField.text;
        
        //Post LobbyRequest
        Debug.Log("Max Turns: " + MaxTurns);
        Debug.Log("Max Turn Duration: " + MaxTurnDuration);
        Debug.Log("Lobby Name: " + LobbyName);
        Debug.Log("Usuario 1: " + _currentUser.UserId);
        Debug.Log("Usuario 2: " + invitedPlayerId);
    }
    
    

    public void onCloseNewGameButton()
    {
        newGameCanvas.SetActive(false);
        foreach (GameObject friend in friends)
        {
            LoadFriendPFData script = friend.GetComponent<LoadFriendPFData>();
            script.InviteButtonDisappearance();
        }
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
                        dbNotRejectedFriends.Add(asdtemp);
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
                        dbNotRejectedFriends.Add(asdtemp);
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
        size = dbNotRejectedFriends.Count;
        foreach (ArrayList array in dbNotRejectedFriends)
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
                            dbFriendRequests.Add(asdtemp2);
                        }
                    }
                    System.Threading.Interlocked.Increment(ref count);

                });
        }

        
    }
    
    void FillFriendList()
    {
        RectTransform parent = content.GetComponent<RectTransform>();
        foreach (ArrayList array in dbFriendRequests)
        {
            GameObject friend = Instantiate(FriendsPF, parent.transform, true);
            LoadFriendPFData script = friend.GetComponent<LoadFriendPFData>();
            //Debug.Log(array[2]);
            script.AssignData(array[1].ToString(), array[0].ToString(), _currentUser.UserId);
            script.ChangeUsernameText();
            if (array[2].ToString() == "Friends")
            {
                script.HideButtons();
                friends.Add(friend);
            }
            
        }
    }

    public void GetPlayerInfo(string player2name, string player2id)
    {
        invitedPlayerId = player2id;
        invitedPlayerName = player2name;
        Player2Text.text = "Player 2: " + invitedPlayerName;
    }

    
}