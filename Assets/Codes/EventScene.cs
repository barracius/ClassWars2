using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventScene : MonoBehaviour
{

    public FloatValue currentHealth;
    public Signal playerHealthSignal;

    public GameObject player;
    public GameObject enemy;
    public GameObject exlamation;
    public GameObject search;
    public GameObject move;
    public GameObject skill;
    public GameObject rest;
    public GameObject attack;
    public GameObject skill1;
    public GameObject dialogBox;
    public GameObject up;
    public GameObject down;
    public GameObject right;
    public GameObject left;
    public GameObject menu;

    public Text dialogText;

    public string dialog;

    public bool dialogActive;

    public bool inBox;

    public bool afterFightP;
    public bool afterFightN;
    private bool player2In;

    private bool parch;

    private bool beginFightP;
    private bool beginFightN;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (player2In && !player.GetComponent<PlayerMovement>().moving && parch)
        {
            dialogActive = true;
            dialogBox.SetActive(true);
            dialog = "You have encountered another player, get ready to rumble!";
            dialogText.text = dialog;
            parch = false;
            beginFightP = true;
            TurnOffMenu();
        }
        if (Input.GetMouseButton(0))
        {
            if (dialogActive)
            {

                TurnOnMenu();
                dialogActive = false;
                dialogBox.SetActive(false);
                if (enemy)
                {
                    exlamation.SetActive(false);
                    enemy.SetActive(true);
                    TurnOffMenu();
                    if (currentHealth.initialValue > 0)
                    {
                        currentHealth.initialValue -= 1;
                        playerHealthSignal.Raise();
                    }
                }

                if (beginFightP)
                {
                    TurnOffMenu();
                    TurnOnFight();
                    Debug.Log("Fight wih user");
                }

                if (beginFightN)
                {
                    TurnOffMenu();
                    TurnOnFight();
                    Debug.Log("Fight wih NPC");
                }

                if (afterFightN)
                {
                    //fight.SetActive(false);
                    TurnOffFight();
                    enemy.SetActive(false);
                    TurnOnMenu();
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player")
        {


            inBox = true;
            Debug.Log("player in scene");
            if (player2In)
            {
                parch = true;
            }
        }

        if (other.name == "Player2")
        {
            player2In = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            inBox = false;
            Debug.Log("player out scene");
        }
    }

    public void Search()
    {
        Debug.Log(inBox);

        if (inBox)
        {
            if (enemy)
            {
                exlamation.SetActive(true);
                beginFightN = true;
            }
            TurnOffMenu();
            dialogActive = true;
            dialogBox.SetActive(true);
            dialogText.text = dialog;
            dialog = "nothing";
        }
    }

    public void Fight()
    {
        //fight.SetActive(false);
        Debug.Log("Fighting");

        Character character = player.GetComponent<Character>();

        Monsters monster = enemy.GetComponent<Monsters>();
        if (character.curHP >= 0)
        {

        }
        if (monster.curHP <= 0)
        {
            dialogActive = true;
            dialogBox.SetActive(true);
            dialog = "You have defeated the log!";
            dialogText.text = dialog;

            afterFightN = true;

        }

    }

    public void TurnOnFight()
    {
        attack.SetActive(true);
        skill1.SetActive(true);
    }

    public void TurnOffFight()
    {
        attack.SetActive(false);
        skill1.SetActive(false);
    }
    public void TurnOffMenu()
    {
        rest.SetActive(false);
        move.SetActive(false);
        skill.SetActive(false);
        search.SetActive(false);
    }

    public void TurnOnMenu()
    {
        rest.SetActive(true);
        move.SetActive(true);
        skill.SetActive(true);
        search.SetActive(true);
    }

    public void TurnOffMovement()
    {
        right.SetActive(false);
        left.SetActive(false);
        up.SetActive(false);
        down.SetActive(false);
        menu.SetActive(false);

    }

    public void Skill()
    {
        Debug.Log("skill");
    }


}
