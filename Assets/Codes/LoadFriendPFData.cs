using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LoadFriendPFData : MonoBehaviour
{
   private int friendID;
   private string friendUsername;
   private int userID;
   public Text UsernameText;
   public Text IDText;
   private string status;
   public GameObject AcceptButton;
   public GameObject RejectButton;
   public GameObject FriendsPF;
   public GameObject InviteButton;
   public GameObject AcceptInviteButton;

   public string invitedFriendUsername;
   public int invitedFriendId;
   public bool onInvite = false;
   
   //lobby invites
   private string lobbyName;
   private int maxTurns;
   private int maxTurnDuration;
   
   public bool OnInviteButtonClass
   {
      get { return onInvite; }
      set
      {
         if (value == onInvite)
            return;
         onInvite = value;
         if (onInvite)
         {
            MenuHandling script = GameObject.Find("CanvasMenu").GetComponent<MenuHandling>();
            script.GetPlayerInfo(invitedFriendUsername, invitedFriendId);
            onInvite = false;
         }
      }
   }

   public void ChangeUsernameText()
   {
      UsernameText.text = friendUsername;
      IDText.text = friendID.ToString();
   }

   public void AssignData(string friendID2, string friendUsername2, int userID2)
   {
      friendUsername = friendUsername2;
      friendID = int.Parse(friendID2);
      userID = userID2;
      //Debug.Log("friend username: " + friendUsername + ", friend ID: " + friendID + ", current user ID: " + userID);
   }
   public void onConfirmButtonClick()
   {
      StartCoroutine(Confirm());
   }
   IEnumerator Confirm()
   {
      WWWForm form = new WWWForm();
      form.AddField("friend_status","FRIENDS");
      form.AddField("user1_id", friendID);
      form.AddField("user2_id", userID);
        
      using (UnityWebRequest www = UnityWebRequest.Post("https://afternoon-spire-83789.herokuapp.com/friendshipUpd",form))
      {
            
         yield return www.SendWebRequest();
         if (www.isNetworkError || www.isHttpError)
         {
            Debug.Log(www.error);
         }
         else
         {
            Debug.Log("Friendship Accepted!");
            if (www.downloadHandler.isDone)
            {
               HideButtons();
               InviteButtonAppearance();
            }
         }
      }
   }

   public void onRejectButtonClick()
   {
      StartCoroutine(Reject());
   }
   IEnumerator Reject()
   {
      WWWForm form = new WWWForm();
      form.AddField("friend_status","REJECTED");
      form.AddField("user1_id", friendID);
      form.AddField("user2_id", userID);
        
      using (UnityWebRequest www = UnityWebRequest.Post("https://afternoon-spire-83789.herokuapp.com/friendshipUpd",form))
      {
            
         yield return www.SendWebRequest();
         if (www.isNetworkError || www.isHttpError)
         {
            Debug.Log(www.error);
         }
         else
         {
            Debug.Log("Friendship Accepted!");
            if (www.downloadHandler.isDone)
            {
               FriendsPF.SetActive(false);
            }
         }
      }
   }

   public void onAcceptInviteButtonClick()
   {
      print(friendID);
      print(userID);
      StartCoroutine(updateLobbyRequest());
   }
   IEnumerator updateLobbyRequest()
   {
      WWWForm form = new WWWForm();
      
      form.AddField("lobbystatus","CONFIRMED");
      form.AddField("user1_id", friendID);
      form.AddField("user2_id", userID);
      
        
      using (UnityWebRequest www = UnityWebRequest.Post("https://afternoon-spire-83789.herokuapp.com/lobbyrequestUpd",form))
      {
         yield return www.SendWebRequest();
         if (www.isNetworkError || www.isHttpError)
         {
            Debug.Log(www.error);
         }
         else
         {
            Debug.Log("Invitation to join lobby accepted!");
            AcceptInviteButtonDisappearance();
         }
      }
   }
   

   public void onInviteButtonClick()
   {
      Debug.Log("friend username: " + friendUsername + ", friend ID: " + friendID);
      invitedFriendId = friendID;
      invitedFriendUsername = friendUsername;
      StartCoroutine(newLobbyRequest());
      OnInviteButtonClass = true;

   }
   IEnumerator newLobbyRequest()
   {
      WWWForm form = new WWWForm();
      form.AddField("user1_id", userID);
      form.AddField("user2_id", friendID);
      form.AddField("lobbystatus","STANDBY");
        
      using (UnityWebRequest www = UnityWebRequest.Post("https://afternoon-spire-83789.herokuapp.com/lobbyrequests",form))
      {
            
         yield return www.SendWebRequest();
         if (www.isNetworkError || www.isHttpError)
         {
            Debug.Log(www.error);
         }
         else
         {
            Debug.Log("Lobby Invitation Sent!");
         }
      }
   }
   
   public void HideButtons()
   {
      AcceptButton.SetActive(false);
      RejectButton.SetActive(false);
   }

   public void InviteButtonAppearance()
   {
      InviteButton.SetActive(true);
   }

   public void InviteButtonDisappearance()
   {
      InviteButton.SetActive(false);
   }

   public void AcceptInviteButtonAppearance()
   {
      AcceptInviteButton.SetActive(true);
   }
   public void AcceptInviteButtonDisappearance()
   {
      AcceptInviteButton.SetActive(false);
   }
}
