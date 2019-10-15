using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventScene : MonoBehaviour
{
    public FloatValue currentHealth;
    public Signal playerHealthSignal;
    
    public GameObject enemy;
    public GameObject exlamation;
    public GameObject search;
    public GameObject move;
    public GameObject skill;
    public GameObject rest;
    public GameObject dialogBox;

    public Text dialogText;

    public string dialog;

    public bool dialogActive;

    public bool inBox;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (dialogActive)
            {
                
                turnOnMenu();
                dialogActive = false;
                dialogBox.SetActive(false);
                if (enemy)
                {
                    exlamation.SetActive(false);
                    enemy.SetActive(true);
                    if (currentHealth.initialValue > 0)
                    {
                        currentHealth.initialValue -= 1;
                        playerHealthSignal.Raise();
                    }
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            
            Debug.Log(inBox);
            inBox = true;
            Debug.Log(inBox);
            Debug.Log("player in scene");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
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
            }
            turnOffMenu();
            Debug.Log("coming in");
            dialogActive = true;
            dialogBox.SetActive(true);
            dialogText.text = dialog;
            dialog = "nothing";
        }
    }
    
    public void turnOffMenu()
    {
        rest.SetActive(false);
        move.SetActive(false);
        skill.SetActive(false);
        search.SetActive(false);
    }

    public void turnOnMenu()
    {
        rest.SetActive(true);
        move.SetActive(true);
        skill.SetActive(true);
        search.SetActive(true);
    }

    public void Skill()
    {
        Debug.Log("skill");
    }
}
