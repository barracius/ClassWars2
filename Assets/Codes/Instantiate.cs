using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class Instantiate : MonoBehaviour
{
    public GameObject Warrior_GO;
    public GameObject Mage_GO;
    public GameObject Hunter_GO;
    public Hashtable infojugadores = new Hashtable();
    public List<GameObject> jugadores = new List<GameObject>();
    public List<Vector3> players_positions = new List<Vector3>();
    public int max_rounds;
    public float max_time;
    public int cant_jugadores;

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



    void getDatafromDB()
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

        //Generate P3
        ArrayList arList3 = new ArrayList();
        arList3.Add("Warrior");
        arList3.Add(4);
        infojugadores.Add("J3", arList3);

        //Generate P4
        ArrayList arList4 = new ArrayList();
        arList4.Add("Warrior");
        arList4.Add(3);
        infojugadores.Add("J4", arList4);

        //Generate Game data
        max_rounds = 4;
        max_time = 10.0f;
        ArrayList arListGD = new ArrayList();
        arListGD.Add(max_rounds);
        arListGD.Add(max_time);
        arListGD.Add(1);
        infojugadores.Add("Partida", arListGD);
    }

    void handleVectors()
    {
        Vector3 P1_Position = new Vector3(0, -190, 0);
        Vector3 P2_Position = new Vector3(0, 370, 0);
        Vector3 P3_Position = new Vector3(350, 370, 0);
        Vector3 P4_Position = new Vector3(-350, 370, 0);
        players_positions.Add(P1_Position);
        players_positions.Add(P2_Position);
        players_positions.Add(P3_Position);
        players_positions.Add(P4_Position);
    }
    void Update()
    {

    }

    void Start()
    {

        getDatafromDB();
        handleVectors();

        int cant_jugadores = 0;
        foreach (DictionaryEntry de in infojugadores)
        {
            ArrayList arraylist = (ArrayList)infojugadores[de.Key];
            string player_class = arraylist[0].ToString();

            if (player_class == "Mage" || player_class.ToString() == "Hunter"
                    || player_class.ToString() == "Warrior")
            {
                GameObject Player = HandleInstantiate(player_class.ToString(), players_positions[cant_jugadores]);
                cant_jugadores++;
                Handletransformation(Player);
                jugadores.Add(Player);
            }
        }
    }
}