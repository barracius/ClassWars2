﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage : MonoBehaviour
{
    public bool alive;
    public int maxHP = 100;
    public int maxMP = 40;
    public int curHP = 80;
    public int curMP = 22;

    public int str = 2;
    public int intel = 5;
    public int dex = 3;
    public ArrayList skills;
    //public HealthBar HBPlayer;

    // private void OnGUI()
    // {
    //     HBPlayer.OnGUI();
    // }
    // Start is called before the first frame update 
    void Start()
    {
        // HBPlayer.currentHp = curHP;
        // HBPlayer.fullHp = maxHP;
        if (curHP <= 0)
        {
            alive = false;
        }
        else
        {
            alive = true;
        }
        ArrayList arList1 = new ArrayList();
        arList1.Add("Fire Ball");
        arList1.Add(3);
        arList1.Add(20);
    }

    // Update is called once per frame
    void Update()
    {
        // HBPlayer.currentHp = curHP;
        // HBPlayer.fullHp = maxHP;
    }
}
