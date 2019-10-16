using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Schema;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using Unity.Notifications.Android;

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
    private static int OtherFriendCode;

    public string currentUserUsername;
    public int currentUserId;

    public Text friendCodeText;

    public ArrayList dbNotRejectedFriends = new ArrayList();
    public ArrayList dbFriendRequests = new ArrayList();
    public ArrayList friends = new ArrayList();
    public ArrayList invitations = new ArrayList();

    public VerticalLayoutGroup content;
    public GameObject FriendsPF;
    public GameObject content2;
    
    public Text Player1Text;
    public Text Player2Text;

    public InputField LobbyNameInputField;
    public Dropdown MaxTurnsDropdown;
    public Dropdown MaxTurnDurationDropdown;
    public Dropdown NumberOfPlayersDropdown;

    private int invitedPlayerId;
    private string invitedPlayerName;

    private int MaxTurns;
    private int MaxTurnDuration;
    private string LobbyName;
    private int NumberOfPlayers;

    public Friendship friendship;
    public User user;
    public LobbyInvite lobbyInvite;
    public ArrayList lobbyInvites = new ArrayList();

    void Start()
    {
        currentUserId = PlayerPrefs.GetInt("UserId");
        currentUserUsername = PlayerPrefs.GetString("UserUsername");
        cat.SetActive(false);
        profileButton.GetComponentInChildren<Text>().text = currentUserUsername;
        friendCodeText.text = "Your friend code is: " + currentUserId;
        StartCoroutine(CheckFriendships());
        var c = new AndroidNotificationChannel()
        {
            Id = "channel_id",
            Name = "Default Channel",
            Importance = Importance.High,
            Description = "Generic notifications",
        };
        AndroidNotificationCenter.RegisterNotificationChannel(c);

    }
    public void sendNot(string title, string subtitle)
    {
        var notification = new AndroidNotification();
        notification.Title = title;
        notification.Text = subtitle;
        notification.FireTime = System.DateTime.Now.AddSeconds(5);

        AndroidNotificationCenter.SendNotification(notification, "channel_id");

    }
    private void Update()
    {
        if (parch)
        {
            //CheckFriendships2(currentUserId);
            parch = false;
        }
        if (parch2)
        {
            //CheckFriendsNames();
            parch2 = false;
        }

        if (size == count)
        {
            //FillFriendList();
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
        Player1Text.text = "Player 1: " + currentUserUsername;
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
        NumberOfPlayers = int.Parse(NumberOfPlayersDropdown.options[NumberOfPlayersDropdown.value].text);
        
        //Post LobbyRequest
        Debug.Log("Max Turns: " + MaxTurns);
        Debug.Log("Max Turn Duration: " + MaxTurnDuration);
        Debug.Log("Lobby Name: " + LobbyName);
        Debug.Log("Usuario 1: " + currentUserId);
        Debug.Log("Usuario 2: " + invitedPlayerId);

        if (NumberOfPlayers == 1)
        {
            PlayerPrefs.SetInt("cantJugadores", 1);
            SceneManager.LoadScene("Scenes/MapScene");
        }
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
        StartCoroutine(AddFriendDB());
    }

    public void Logout_btn()
    {
        SceneManager.LoadScene("Scenes/LoginScene");
    }

    IEnumerator AddFriendDB()
    {
        WWWForm form = new WWWForm();
        form.AddField("user1_id", currentUserId);
        form.AddField("user2_id",int.Parse(UsernameInputField.text));
        form.AddField("friend_status","STANDBY");
        
        using (UnityWebRequest www = UnityWebRequest.Post("https://afternoon-spire-83789.herokuapp.com/friendships",form))
        {
            
            yield return www.SendWebRequest();
            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Friend Invitation Sent!");
            }
        }
    }
    IEnumerator CheckFriendships()
    {
        using (UnityWebRequest www = UnityWebRequest.Get("https://afternoon-spire-83789.herokuapp.com/friendships/" + currentUserId))
        {
            yield return www.SendWebRequest();
            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                if (www.downloadHandler.text.Length == 2)
                {
                    Debug.Log("No Friends ;(");
                    yield break;
                }
                if (www.downloadHandler.isDone)
                {
                    string temp = www.downloadHandler.text.Substring(1, www.downloadHandler.text.Length-2);
                    int temp5 = Regex.Matches(temp, "user1_id").Count;
                    if (temp5 > 1)
                    {
                        string[] splitArray =  temp.Split(new string[]{"},{"},StringSplitOptions.None);
                        for (int i = 0; i < splitArray.Length; i++)
                        {
                            string temp2;
                            string temp3;
                            if (i == 0)
                            {
                                temp2 = splitArray[i].Insert(splitArray[i].Length, "}");
                            }

                            else if (i == splitArray.Length - 1)
                            {
                                temp2 = splitArray[i].Insert(0, "{");
                            }
                            else
                            {
                                temp3 = splitArray[i].Insert(splitArray[i].Length, "}");
                                temp2 = temp3.Insert(0, "{");
                            }
                            dbFriendRequests.Add(temp2);
                        }
                        foreach (string asd in dbFriendRequests)
                        {
                            friendship = JsonUtility.FromJson<Friendship>(asd);
                            /*print(friendship.friend_status);
                            print(friendship.user1_id);
                            print(friendship.user2_id);*/
                            dbNotRejectedFriends.Add(friendship);
                        }
                    }
                    else if(temp5 == 1)

                    {
                        friendship = JsonUtility.FromJson<Friendship>(temp);
                        dbNotRejectedFriends.Add(friendship);
                    }
                   
                    dbFriendRequests.Clear();
                    StartCoroutine(CheckFriendsNames());
                }
            }
        }
    }
    
    IEnumerator CheckFriendsNames()
    {
        foreach (Friendship friendship in dbNotRejectedFriends)
        {
            if (friendship.user1_id == currentUserId)
            {
                using (UnityWebRequest www = UnityWebRequest.Get("https://afternoon-spire-83789.herokuapp.com/users/" + friendship.user2_id))
                {
                    yield return www.SendWebRequest();
                    if (www.isNetworkError || www.isHttpError)
                    {
                        Debug.Log(www.error);
                    }
                    else
                    {
                        if (www.downloadHandler.text.Length == 2)
                        {
                            Debug.Log("Vacio");
                            yield break;
                        }
                        if (www.downloadHandler.isDone)
                        {
                            string temp = www.downloadHandler.text.Substring(1, www.downloadHandler.text.Length-2); 
                            user = JsonUtility.FromJson<User>(temp);
                            ArrayList asdtemp2 = new ArrayList();
                            asdtemp2.Add(user.username);
                            asdtemp2.Add(user.id);
                            asdtemp2.Add(friendship.friend_status);
                            if (friendship.friend_status == "STANDBY")
                            {
                                sendNot("Solicitud de Amistad",  user.username + " te ha agregado como amig@!");
                            }
                            dbFriendRequests.Add(asdtemp2);
                        
                        }
                    }
                }
            }
            else if (friendship.user2_id == currentUserId)
            {
                using (UnityWebRequest www = UnityWebRequest.Get("https://afternoon-spire-83789.herokuapp.com/users/" + friendship.user1_id))
                {
                    yield return www.SendWebRequest();
                    if (www.isNetworkError || www.isHttpError)
                    {
                        Debug.Log(www.error);
                    }
                    else
                    {
                        if (www.downloadHandler.text.Length == 2)
                        {
                            Debug.Log("Vacio");
                            yield break;
                        }
                        if (www.downloadHandler.isDone)
                        {
                            string temp = www.downloadHandler.text.Substring(1, www.downloadHandler.text.Length-2); 
                            user = JsonUtility.FromJson<User>(temp);
                            ArrayList asdtemp2 = new ArrayList();
                            asdtemp2.Add(user.username);
                            asdtemp2.Add(user.id);
                            asdtemp2.Add(friendship.friend_status);
                            if (friendship.friend_status == "STANDBY")
                            {
                                sendNot("Solicitud de Amistad",  user.username + " te ha agregado como amig@!");
                            }
                            dbFriendRequests.Add(asdtemp2);
                        
                        }
                    }
                }  
            }
            
        }

        StartCoroutine(CheckLobbyInvitations());
        FillFriendList();
    }
    
    IEnumerator CheckLobbyInvitations()
    {
        for (int i = 0; i < dbFriendRequests.Count; i++)
        {
            
            print(dbFriendRequests.Count);
            ArrayList sublista = (ArrayList) dbFriendRequests[i];
            sendNot("Nueva Partida!",sublista[0] + " te ha invitado a una partida!");
            if (sublista[2].ToString() == "FRIENDS")
            {
                WWWForm form = new WWWForm();
                form.AddField("user1_id", currentUserId);
                form.AddField("user2_id", int.Parse(sublista[1].ToString()));

                using (UnityWebRequest www = UnityWebRequest.Post("https://afternoon-spire-83789.herokuapp.com/lobbyrequestUsers",form))
                {
            
                    yield return www.SendWebRequest();
                    if (www.isNetworkError || www.isHttpError)
                    {
                        Debug.Log(www.error);
                    }
                    else
                    {    
                        if (www.downloadHandler.text.Length == 2)
                        {
                                Debug.Log("No Invitations ;(");
                                yield break;
                        }
                        if (www.downloadHandler.isDone)
                        {
                            
                            string temp = www.downloadHandler.text.Substring(1, www.downloadHandler.text.Length-2);
                            int temp5 = Regex.Matches(temp, "user1_id").Count;
                            if (temp5 > 1)
                            {
                                string[] splitArray =  temp.Split(new string[]{"},{"},StringSplitOptions.None);
                                for (int j = 0; j < splitArray.Length; j++)
                                {
                                    string temp2;
                                    string temp3;
                                    if (j == 0)
                                    {
                                        temp2 = splitArray[j].Insert(splitArray[j].Length, "}");
                                    }

                                    else if (j == splitArray.Length - 1)
                                    {
                                        temp2 = splitArray[j].Insert(0, "{");
                                    }
                                    else
                                    {
                                        temp3 = splitArray[j].Insert(splitArray[j].Length, "}");
                                        temp2 = temp3.Insert(0, "{");
                                    }

                                    print(temp2);
                                    invitations.Add(temp2);
                                }
                                foreach (string asd in invitations)
                                {
                                    lobbyInvite = JsonUtility.FromJson<LobbyInvite>(asd);
                                    /*print(friendship.friend_status);
                                    print(friendship.user1_id);
                                    print(friendship.user2_id);*/
                                    lobbyInvites.Add(lobbyInvite);
                                }
                            }
                            else if(temp5 == 1)
                            {
                                lobbyInvite = JsonUtility.FromJson<LobbyInvite>(temp);
                                lobbyInvites.Add(lobbyInvite);
                            }
                           
                            invitations.Clear();
                        }
                        /*if (www.downloadHandler.isDone)
                        {
                            string temp = www.downloadHandler.text.Substring(1, www.downloadHandler.text.Length-2);
                            print(temp);
                            if (temp == "")
                            {
                                break;
                            }
                            lobbyInvite = JsonUtility.FromJson<LobbyInvite>(temp);
                            lobbyInvites.Add(lobbyInvite);
                            
                        }*/
                        
                    }
                }
            }
            
        }
        foreach (GameObject friend in friends)
        {
            LoadFriendPFData script = friend.GetComponent<LoadFriendPFData>();
            Transform transformId = friend.gameObject.transform.GetChild(1);
            Transform transformNombre = friend.gameObject.transform.GetChild(0);
            Text idText = transformId.GetComponent<Text>();
            Text usernameText = transformNombre.GetComponent<Text>();
            for (int i = 0; i < lobbyInvites.Count; i++)
            {
                LobbyInvite li = (LobbyInvite)lobbyInvites[i];
                if (li.user1_id == idText.text && li.user2_id == currentUserId.ToString() && li.lobbystatus == "STANDBY")
                {
                    script.AcceptInviteButtonAppearance();

                }
            }
            
        }
        
    }

   void FillFriendList()
    {
        RectTransform parent = content.GetComponent<RectTransform>();
        foreach (ArrayList array in dbFriendRequests)
        {
            GameObject friend = Instantiate(FriendsPF, parent.transform, true);
            LoadFriendPFData script = friend.GetComponent<LoadFriendPFData>();
            //print("Array 0: " + array[0]);
            //print("Array 1: " + array[1]);
            //print("Array 2: " + array[2]);
            
            script.AssignData(array[1].ToString(), array[0].ToString(), currentUserId);
            script.ChangeUsernameText();
            if (array[2].ToString() == "FRIENDS")
            {
                script.HideButtons();
                friends.Add(friend);
            }
            
        }
    }

    public void GetPlayerInfo(string player2name, int player2id)
    {
        invitedPlayerId = player2id;
        invitedPlayerName = player2name;
        Player2Text.text = "Player 2: " + invitedPlayerName;
    }

    
}