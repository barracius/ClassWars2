using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor.UI;
using UnityEngine;

public class Friendship
{
    public string username1, username2, status;

    public Friendship(string u1, string u2)
    {
        username1 = u1;
        username2 = u2;
        status = "Stand by";
    }

    public void Accepted()
    {
        status = "Friends";
    }

    public void Rejected()
    {
        status = "Blocked";
    }
}