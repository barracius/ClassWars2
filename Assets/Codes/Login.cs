﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour
{
    public User user;

    private void Update()
    {
        if (isLoggedin) {
            SceneManager.LoadScene("Scenes/MenuScene");
            isLoggedin = false;
        }
    }

    public InputField LoginEmailText;
    public InputField LoginPasswordText;

    public static string PlayerEmail;
    public static string PlayerPassword;
    private User current_user;
    public bool isLoggedin;

    public void OnSubmitLoginButton()
    {
        PlayerEmail = LoginEmailText.text;
        PlayerPassword = LoginPasswordText.text;
        StartCoroutine(LoginToDatabase());
    }

    IEnumerator LoginToDatabase()
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
                        isLoggedin = true;
                    }
                }
            }
        }
    }
}