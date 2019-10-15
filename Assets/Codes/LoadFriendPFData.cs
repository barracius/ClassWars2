using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadFriendPFData : MonoBehaviour
{
   public string id;
   public string username;
   public Text UsernameText;

   public void ChangeUsernameText(string username2)
   {
      UsernameText.text = username2;
      username = username2;
   }

   public void AssignID(string id2)
   {
      id = id2;
      Debug.Log("id asignado: " + id);
   }
}
