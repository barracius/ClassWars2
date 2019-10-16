using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
// using UnityEditor.UI;
using UnityEngine;

public class Friendship
{
    public int user1_id;
    public int user2_id;
    public string friend_status;

    public Friendship(int u1, int u2, string s)
    {
        user1_id = u1;
        user2_id = u2;
        friend_status = s;
    }
}