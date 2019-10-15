using System.Collections;
using System.Collections.Generic;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using UnityEngine;
using UnityEngine.UI;

public class LoadFriendPFData : MonoBehaviour
{
   private string friendID;
   private string friendUsername;
   private string userID;
   public Text UsernameText;
   private string status;
   public GameObject AcceptButton;
   public GameObject RejectButton;
   public GameObject FriendsPF;


   public void ChangeUsernameText()
   {
      UsernameText.text = friendUsername;
   }

   public void AssignData(string friendID2, string friendUsername2, string userID2)
   {
      friendUsername = friendUsername2;
      friendID = friendID2;
      userID = userID2;
      //Debug.Log("friend username: " + friendUsername + ", friend ID: " + friendID + ", current user ID: " + userID);
   }

   public void onConfirmButtonClick()
   {
      FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://class-wars.firebaseio.com/.json");
      DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;
      //Debug.Log("friend username: " + friendUsername + ", friend ID: " + friendID + ", current user ID: " + userID);
      Friendship friendship = new Friendship(friendID, userID,"Friends");
      string json = JsonUtility.ToJson(friendship);
      reference.Child("friendship").Child(friendID + " | " + userID).SetRawJsonValueAsync(json);
      HideButtons();
   }

   public void onRejectButtonClick()
   {
      FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://class-wars.firebaseio.com/.json");
      DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;
      //Debug.Log("friend username: " + friendUsername + ", friend ID: " + friendID + ", current user ID: " + userID);
      Friendship friendship = new Friendship(friendID, userID,"Rejected");
      string json = JsonUtility.ToJson(friendship);
      reference.Child("friendship").Child(friendID + " | " + userID).SetRawJsonValueAsync(json);
      FriendsPF.SetActive(false);
      
   }

   public void HideButtons()
   {
      AcceptButton.SetActive(false);
      RejectButton.SetActive(false);
   }
}
