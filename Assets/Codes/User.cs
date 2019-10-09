using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

[Serializable]
public class User
{
    public string username, password, email;

    public User(string Username, string Password, string Email)
    {
        username = Username;
        password = Password;
        email = Email;
    }
}