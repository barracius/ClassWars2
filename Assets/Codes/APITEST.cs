// using UnityEngine;
//  using UnityEngine.UI;
//  using System.Collections;
//  using System.Collections.Generic;
//  public class btnGetData : MonoBehaviour {

//   void Start()
//   {
//       TaskOnClick;
//   }
//   IEnumerator WaitForWWW(WWW www)
//   {
//       yield return www;
     
      
//       string txt = "";
//       if (string.IsNullOrEmpty(www.error))
//           txt = www.text;  //text of success
//       else
//           txt = www.error;  //error
//      Debug.Log(txt);
//   }

//   void TaskOnClick()
//   {
//       try
//       {
          
//           WWW api = new WWW("https://afternoon-spire-83789.herokuapp.com/books");
//           ///GET by IIS hosting...
//           ///WWW api = new WWW("http://192.168.1.120/si_aoi/api/total?dynamix={\"plan\":\"TESTA02\"");
//           StartCoroutine(WaitForWWW(api));
//       }
//       catch (UnityException ex) { Debug.Log(ex.Message); }
//   } 
//  }