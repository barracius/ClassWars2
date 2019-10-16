using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monsters : MonoBehaviour
{
    public int maxHP = 50;
    public int maxMP = 20;
    public int curHP;
    public int curMP;
    public int str = 3;
    public int intel = 2;
    public int dex = 2;

    // Start is called before the first frame update
    void Awake()
    {

        curHP = maxHP;
        curMP = maxMP;
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
    void DmgRecibed(int dmg)
    {
        curHP -= dmg;
    }
    void SpendMana(int mana)
    {
        curMP -= mana;
    }
}
