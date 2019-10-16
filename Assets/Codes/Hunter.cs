using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hunter : MonoBehaviour
{

    public bool alive;
    
    public string classname = "Hunter";
    public int maxHP = 100;
    public int maxMP = 40;
    public int curHP = 80;
    public int curMP = 22;
    public int str = 3;
    public int intel = 2;
    public int dex = 5;
    public ArrayList skills;

    public HealthBar HBPlayer;

    // Start is called before the first frame update
    void Start()
    {
        if (curHP <= 0)
        {
            alive = false;
        }
        else
        {
            alive = true;
        }
        ArrayList arList1 = new ArrayList();
        arList1.Add("Dual Arrow");
        arList1.Add(2);
        arList1.Add(11);
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

    // public int dealDmg()
    // {
    //     return 
    // }


}
