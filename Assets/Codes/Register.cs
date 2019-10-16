using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Database;
using Firebase;
using Firebase.Unity.Editor;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class Register : MonoBehaviour
{

    public InputField RegisterUsernameText;
    public InputField RegisterPasswordText;
    public InputField RegisterEmailText;

    public static string PlayerUsername;
    public static string PlayerPassword;
    public static string PlayerEmail = null;

    public User user;

    public void OnSubmitRegisterButton()
    {
        PlayerUsername = RegisterUsernameText.text;
        PlayerPassword = RegisterPasswordText.text;
        PlayerEmail = RegisterEmailText.text;
        StartCoroutine(PostToDatabase());
    }
    IEnumerator PostToDatabase()
    {
        WWWForm form = new WWWForm();
        form.AddField("pass",PlayerPassword);
        form.AddField("username",PlayerUsername);
        form.AddField("email",PlayerEmail);
        
        using (UnityWebRequest www = UnityWebRequest.Post("https://afternoon-spire-83789.herokuapp.com/users",form))
        {
            
            yield return www.SendWebRequest();
            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
               Debug.Log("User successfully created!");
               if (www.downloadHandler.isDone)
               {
                   StartCoroutine(getUser());
               }
               
            }
        }
    }
    
    IEnumerator getUser()
    {
        using (UnityWebRequest www = UnityWebRequest.Get("https://afternoon-spire-83789.herokuapp.com/login/" + PlayerEmail))
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
                    Debug.Log("Wrong email or password");
                    yield break;
                }
                if (www.downloadHandler.isDone)
                {
                    string temp = www.downloadHandler.text.Substring(1, www.downloadHandler.text.Length-2);; 
                    user = JsonUtility.FromJson<User>(temp);
                    if (user.pass != PlayerPassword)
                    {
                        Debug.Log("Wrong email or password");
                    }
                    else
                    {
                        PlayerPrefs.SetInt("UserId",user.id);
                        PlayerPrefs.SetString("UserUsername", user.username);
                        SceneManager.LoadScene("Scenes/MenuScene");
                    }
                }
            }
        }
    }
}