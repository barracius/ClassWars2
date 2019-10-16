using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleMove : MonoBehaviour
{
    public GameObject up;

    public GameObject down;

    public GameObject right;

    public GameObject left;

    public GameObject rest;

    public GameObject move;

    public GameObject skill;

    public GameObject search;

    public GameObject menu;

    public GameObject fight;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void turnOnMove()
    {
        turnOffMenu();
        
        if(up.GetComponent<buttonToggle>().canMove){
            up.SetActive(true);
        }
        if(down.GetComponent<buttonToggle>().canMove){
            down.SetActive(true);
        }
        if(right.GetComponent<buttonToggle>().canMove){
            right.SetActive(true);
        }
        if(left.GetComponent<buttonToggle>().canMove){
            left.SetActive(true);
        }  
    }

    public void turnOffMove()
    {
        turnOnMenu();
        up.SetActive(false);
        down.SetActive(false);
        left.SetActive(false);
        right.SetActive(false);
    }

    public void turnOffMenu()
    {
        rest.SetActive(false);
        move.SetActive(false);
        skill.SetActive(false);
        search.SetActive(false);
        menu.SetActive(true);
    }

    public void turnOnMenu()
    {
        up.SetActive(false);
        down.SetActive(false);
        left.SetActive(false);
        right.SetActive(false);
        rest.SetActive(true);
        move.SetActive(true);
        skill.SetActive(true);
        search.SetActive(true);
        menu.SetActive(false);
    }
}
