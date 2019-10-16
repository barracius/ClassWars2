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
   private string status;
   public GameObject AcceptButton;
   public GameObject RejectButton;
   public GameObject FriendsPF;
   public GameObject InviteButton;
   public ArrayList ArrayAmigos;

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

   public void onInviteButtonClick()
   {
      Debug.Log("friend username: " + friendUsername + ", friend ID: " + friendID);
      invitedFriendId = friendID;
      invitedFriendUsername = friendUsername;
      OnInviteButtonClass = true;
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
}
