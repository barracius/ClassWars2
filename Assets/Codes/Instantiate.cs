using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class Instantiate : MonoBehaviour
{
    public GameObject Warrior_GO;
    public GameObject Mage_GO;
    public GameObject Hunter_GO;
    private Warrior attributes;
    public Hashtable infojugadores = new Hashtable();
    public List<GameObject> jugadores = new List<GameObject>();

    public List<Vector3> vectores = new List<Vector3>();
    public Vector3 P1_Position = new Vector3(0, -190, 0);
    public Vector3 P2_Position = new Vector3(0, 370, 0);
    public Vector3 P3_Position = new Vector3(350, 370, 0);
    public Vector3 P4_Position = new Vector3(-350, 370, 0);

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

    GameObject HandleInstantiate(string jugador, Vector3 vector)
    {
        string clase = (string)infojugadores[jugador];
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

    void Start()
    {
        infojugadores.Add("J1", "Mage");
        infojugadores.Add("J2", "Hunter");
        infojugadores.Add("J3", "Warrior");
        infojugadores.Add("J4", "Mage");
        vectores.Add(P1_Position);
        vectores.Add(P2_Position);
        vectores.Add(P3_Position);
        vectores.Add(P4_Position);

        int i = 0;
        foreach (DictionaryEntry de in infojugadores)
        {
            GameObject Player = HandleInstantiate(de.Key.ToString(), vectores[i]);
            i++;
            Handletransformation(Player);
            jugadores.Add(Player);
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