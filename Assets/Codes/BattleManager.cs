using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public Hashtable infojugadores = new Hashtable();
    public List<GameObject> jugadores = new List<GameObject>();
    public List<Vector3> players_positions = new List<Vector3>();

    // public GameObject HP_Panel;
    // Start is called before the first frame update
    void Start()
    {
   //     getDatafromDB();
    }

    // Update is called once per frame
    void Update()
    {

    }

//    //     void getDatafromDB()
//     {
//         //Generate P1
//         ArrayList arList1 = new ArrayList();
//         arList1.Add("Hunter");
//         arList1.Add(1);
//         infojugadores.Add("J1", arList1);

//         //Generate P2
//         ArrayList arList2 = new ArrayList();
//         arList2.Add("Mage");
//         arList2.Add(2);
//         infojugadores.Add("J2", arList2);

//         //Generate P3
//         ArrayList arList3 = new ArrayList();
//         arList3.Add("Warrior");
//         arList3.Add(4);
//         infojugadores.Add("J3", arList3);

//         //Generate P4
//         ArrayList arList4 = new ArrayList();
//         arList4.Add("Warrior");
//         arList4.Add(3);
//         infojugadores.Add("J4", arList4);

//         //Generate Game data
//         max_rounds = 4;
//         max_time = 10.0f;
//         ArrayList arListGD = new ArrayList();
//         arListGD.Add(max_rounds);
//         arListGD.Add(max_time);
//         arListGD.Add(1);
//         infojugadores.Add("Partida", arListGD);
//     }

}
