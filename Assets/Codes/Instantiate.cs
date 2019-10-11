using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class Instantiate : MonoBehaviour
{
    public GameObject Warrior_GO;
    public GameObject Mage_GO;
    public GameObject Hunter_GO;
    public Warrior attributes;
    public Hashtable infojugadores = new Hashtable();
    public List<GameObject> jugadores = new List<GameObject>();

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
    void Awake(){

    }

    GameObject HandleInstantiate(string jugador){
        string clase = (string)infojugadores[jugador];
        if(clase == "Mage"){
            GameObject character = Instantiate(Mage_GO,new Vector3(0,-190,0), transform.rotation) as GameObject;
            return character;
        }else if(clase=="Warrior"){
            GameObject character = Instantiate(Warrior_GO,new Vector3(0,-190,0), transform.rotation) as GameObject;
            return character;
        }else if(clase=="Hunter"){
            GameObject character = Instantiate(Hunter_GO,new Vector3(0,-190,0), transform.rotation) as GameObject;
            return character;
        }else{
            return null;
        }
    }

    void Start()
    {
        infojugadores.Add("J1","Mage");
        infojugadores.Add("J2","Mage");
        infojugadores.Add("J3","Mage");
        infojugadores.Add("J4","Mage");


        // for (int i = 0; i< infojugadores.Count;i++){
        //     print(infojugadores[i]);
        //     // GameObject Player =HandleInstantiate(infojugadores[i]);
        //     // jugadores.Add(Player);
        // }

        foreach(DictionaryEntry de in infojugadores){
            print(de.Key);
        }

        GameObject Player1 = Instantiate(Warrior_GO,new Vector3(0,-190,0), transform.rotation) as GameObject;
        GameObject Player2 = Instantiate(Warrior_GO, new Vector3(0,370, 0), transform.rotation) as GameObject;
        GameObject Player3 = Instantiate(Hunter_GO, new Vector3(350,370, 0), transform.rotation) as GameObject;
        GameObject Player4 = Instantiate(Mage_GO, new Vector3(-350,370, 0), transform.rotation) as GameObject;
        
        jugadores.Add(Player1);
        jugadores.Add(Player2);
        jugadores.Add(Player3);
        jugadores.Add(Player4);

        attributes = (Warrior)jugadores[0].GetComponent( "Warrior" );
        print(attributes.maxHP);

        Player1.transform.SetParent(GameObject.FindGameObjectWithTag("aaaa").transform, false);
        Player2.transform.SetParent(GameObject.FindGameObjectWithTag("aaaa").transform, false);
        Player3.transform.SetParent(GameObject.FindGameObjectWithTag("aaaa").transform, false);
        Player4.transform.SetParent(GameObject.FindGameObjectWithTag("aaaa").transform, false);

        //attributes = (Attributes)Player1.GetComponent( "Attributes" );
        //attributes.HP = 33;

        //attributes = Player1.Warrior.HP;

        //getHP(Player1);
        //setHP(Player1, 77);
        //getHP(Player1);
        


    }



}