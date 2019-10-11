using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hunter : MonoBehaviour
{

    public int maxHP = 100;
    public int maxMP = 40;
    public int curHP = 80;
    public int curMP = 22;

    public int str = 3;
    public int intel = 2;
    public int dex = 5;
    public ArrayList skills;

    // Start is called before the first frame update
    void Start()
    {
        ArrayList arList1 = new ArrayList();
        arList1.Add("Dual Arrow");
        arList1.Add(2);
        arList1.Add(11);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
