using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class Instantiate : MonoBehaviour
{
    public GameObject Warrior_GO;
    public GameObject Mage_GO;
    public GameObject Hunter_GO;
    private Warrior attributes;
    //    public Dictionary<string, ArrayList> infojugadores = new Dictionary<string, ArrayList>();
    public Hashtable infojugadores = new Hashtable();
    public List<GameObject> jugadores = new List<GameObject>();

    public List<Vector3> vectores = new List<Vector3>();


    public int max_rounds;
    public float max_time;


    public int cant_jugadores;

    // int getHP(GameObject Player){
    //     attributes = (Attributes)Player.GetComponent( "Attributes" );
    //     print(attributes.HP);
    //     return attributes.HP;
    // }
    // void setHP(GameObject Player, int value){
    //     attributes = (Attributes)Player.GetComponent( "Attributes" );
    //     attributes.HP = value;

    // }
    // int getMP(GameObject Player){
    //     attributes = (Attributes)Player.GetComponent( "Attributes" );
    //     print(attributes.MP);
    //     return attributes.MP;
    // }
    // void setMP(GameObject Player, int value){
    //     attributes = (Attributes)Player.GetComponent( "Attributes" );
    //     attributes.MP = value;

    // }


    void Handletransformation(GameObject Player)
    {
        Player.transform.SetParent(GameObject.FindGameObjectWithTag("aaaa").transform, false);
    }

    GameObject HandleInstantiate(string clase, Vector3 vector)
    {

        if (clase == "Mage")
        {
            GameObject character = Instantiate(Mage_GO, vector, transform.rotation) as GameObject;
            return character;
        }
        else if (clase == "Warrior")
        {
            GameObject character = Instantiate(Warrior_GO, vector, transform.rotation) as GameObject;
            return character;
        }
        else if (clase == "Hunter")
        {
            GameObject character = Instantiate(Hunter_GO, vector, transform.rotation) as GameObject;
            return character;
        }
        else
        {
            return null;
        }
    }

    // void turnEnd()
    // {

    //     infojugadores["Turno"]++;
    //     if (infojugadores["Turno"] % cant_jugadores == 0)
    //     {

    //     }

    // }

    void Update()
    {

    }

    void Start()
    {
        //Generate P1
        ArrayList arList1 = new ArrayList();
        arList1.Add("Hunter");
        arList1.Add(1);
        infojugadores.Add("J1", arList1);

        //Generate P2
        ArrayList arList2 = new ArrayList();
        arList2.Add("Mage");
        arList2.Add(2);
        infojugadores.Add("J2", arList2);

        //Generate Game data
        max_rounds = 4;
        max_time = 10.0f;
        ArrayList arList3 = new ArrayList();
        arList3.Add(max_rounds);
        arList3.Add(max_time);
        arList3.Add(1);
        infojugadores.Add("Partida", arList3);

        //infojugadores.Add("State", null);
        Vector3 P1_Position = new Vector3(0, -190, 0);
        Vector3 P2_Position = new Vector3(0, 370, 0);
        Vector3 P3_Position = new Vector3(350, 370, 0);
        Vector3 P4_Position = new Vector3(-350, 370, 0);
        vectores.Add(P1_Position);
        vectores.Add(P2_Position);
        vectores.Add(P3_Position);
        vectores.Add(P4_Position);




        int cant_jugadores = 0;
        foreach (DictionaryEntry de in infojugadores)
        {
            ArrayList arraylist = (ArrayList)infojugadores[de.Key];
            string player_class = arraylist[0].ToString();

            if (player_class == "Mage" || player_class.ToString() == "Hunter"
                    || player_class.ToString() == "Warrior")
            {
                GameObject Player = HandleInstantiate(player_class.ToString(), vectores[cant_jugadores]);
                cant_jugadores++;
                Handletransformation(Player);
                jugadores.Add(Player);

            }
        }



        // attributes = (Warrior)jugadores[0].GetComponent("Warrior");
        // print(attributes.maxHP);


        //attributes = (Attributes)Player1.GetComponent( "Attributes" );
        //attributes.HP = 33;

        //attributes = Player1.Warrior.HP;

        //getHP(Player1);
        //setHP(Player1, 77);
        //getHP(Player1);



    }



}