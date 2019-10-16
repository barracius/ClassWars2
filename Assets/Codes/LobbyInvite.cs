using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using JetBrains.Annotations;
// using UnityEditor.UI;
using UnityEngine;

public class LobbyInvite
{
    public string user1_id, user2_id, lobbystatus, lobbyname;
    public int id;

    public LobbyInvite(string u1, string u2, string s, int mt, int td, string ln, int lid)
    {
        user1_id = u1;
        user2_id = u2;
        lobbystatus = s;
        lobbyname = ln;
        id = lid;
    }
}