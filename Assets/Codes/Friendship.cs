using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
// using UnityEditor.UI;
using UnityEngine;

public class Friendship
{
    public string user1_id, user2_id, status;

    public Friendship(string u1, string u2)
    {
        user1_id = u1;
        user2_id = u2;
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