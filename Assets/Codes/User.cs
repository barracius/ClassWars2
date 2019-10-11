using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

[Serializable]
public class User
{
    public string username;

    public User(string Username)
    {
        username = Username;
    }
}