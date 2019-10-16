using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    // Start is called before the first frame update

    public string CharClass;
    public GameObject Player;
    public int dmg = 0;

    public string classname = "";
    public int maxHP = 0;
    public int maxMP = 0;
    public int curHP = 0;
    public int curMP = 0;
    public int str = 0;
    public int intel = 0;
    public int dex = 0;



    void Start()
    {

        //OBTENER DE BD
        CharClass = "Warrior";

        if (CharClass == "Mage")
        {

            Player.AddComponent<Mage>();
            Mage clase = Player.GetComponent<Mage>();
            classname = clase.classname;
            maxHP = clase.maxHP;
            maxMP = clase.maxMP;
            curHP = clase.curHP;
            curMP = clase.curMP;
            str = clase.str;
            intel = clase.intel;
            dex = clase.dex;

        }

        if (CharClass == "Warrior")
        {

            Player.AddComponent<Warrior>();
            var clase = Player.GetComponent<Warrior>();
            classname = clase.classname;
            maxHP = clase.maxHP;
            maxMP = clase.maxMP;
            curHP = clase.curHP;
            curMP = clase.curMP;
            str = clase.str;
            intel = clase.intel;
            dex = clase.dex;
        }
        if (CharClass == "Hunter")
        {
            Player.AddComponent<Hunter>();
            var clase = Player.GetComponent<Hunter>();
            classname = clase.classname;
            maxHP = clase.maxHP;
            maxMP = clase.maxMP;
            curHP = clase.curHP;
            curMP = clase.curMP;
            str = clase.str;
            intel = clase.intel;
            dex = clase.dex;
        }


    }


    // Update is called once per frame
    void Update()
    {


    }




}