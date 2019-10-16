using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : MonoBehaviour
{
    public bool alive;
    
    public string classname = "Warrior";
    public int maxHP = 100;
    public int maxMP = 40;
    public int curHP = 80;
    public int curMP = 22;
    public int str = 5;
    public int intel = 2;
    public int dex = 3;
    public ArrayList skills;

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
        arList1.Add("Power Strike");
        arList1.Add(2);
        arList1.Add(10);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
