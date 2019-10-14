using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

[Serializable]
public class User
{
    public string username, id;

    public User(string Username, string Id)
    {
        username = Username;
        id = Id;
    }
}