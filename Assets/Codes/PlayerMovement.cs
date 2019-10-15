﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private bool movx = false;
    private bool movy = false;
    private int xChange;
    private int yChange;

    public GameObject up;
    public GameObject down;
    public GameObject right;
    public GameObject left;
    public GameObject menu;
    
    
    private int c = 312;
    private int y = 177;
    public float speed;
    public Vector2 cameraChange;
    private CameraMovement cam;
    
    
    private Rigidbody2D myRigidbody;

    private Vector3 change;

    private Animator animator;

    public float moveX;

    public float moveY;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.GetComponent<CameraMovement>();
        animator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (movx)
        {
            if (c == 0)
            {
                movx = false;
                TurnOnMoveButtons();
                c = 312;
                
            }

            if (c == 170)
            {
                cam.minPosition += cameraChange;
                cam.maxPosition += cameraChange;
            }
            change = Vector2.zero;
            change.x = xChange;
            change.y = yChange;
            UpdateAnimationAndMove();
            c -= 1;
        }
        if (movy)
        {
            if (y == 0)
            {
                movy = false;
                TurnOnMoveButtons();
                y = 177;
                
            }

            if (y == 75)
            {
                cam.minPosition += cameraChange;
                cam.maxPosition += cameraChange;
            }
            change = Vector2.zero;
            change.x = xChange;
            change.y = yChange;
            UpdateAnimationAndMove();
            y -= 1;
        }
        change = Vector2.zero;
        change.x = Input.GetAxis("Horizontal");
        change.y = Input.GetAxis("Vertical");
        UpdateAnimationAndMove();
    }

    public void moveLeft()
    {
        TurnOffMoveButtons();
        xChange = -1;
        yChange = 0;
        cameraChange.x = -18;
        cameraChange.y = 0;
        movx = true;
    }
    public void moveUo()
    {
        TurnOffMoveButtons();
        cameraChange.x = 0;
        cameraChange.y = 10;
        xChange = 0;
        yChange = 1;
        movy = true;
    }
    public void moveDown()
    {
        TurnOffMoveButtons();
        cameraChange.x = 0;
        cameraChange.y = -10;
        xChange = 0;
        yChange = -1;
        movy = true;
    }
    public void moveRight()
    {
        TurnOffMoveButtons();
        cameraChange.x = 18;
        cameraChange.y = 0;
        xChange = 1;
        yChange = 0;
        movx = true;

    }
    public void UpdateAnimationAndMove()
    {
        if (change != Vector3.zero)
        {
            MoveCharacter();
        }
        if (movx || movy)
        {
            animator.SetFloat("moveX", xChange);
            animator.SetFloat("moveY", yChange);
            animator.SetBool("moving", true);
        }
        else
        {
            animator.SetBool("moving", false);
        }
    }
    public void MoveCharacter()
    {
        myRigidbody.MovePosition(transform.position  + Time.deltaTime * speed * change);
    }

    public void TurnOffMoveButtons()
    {
        menu.SetActive(false);
        up.SetActive(false);
        down.SetActive(false);
        right.SetActive(false);
        left.SetActive(false);
    }
    public void TurnOnMoveButtons()
    {
        menu.SetActive(true);
        up.SetActive(true);
        down.SetActive(true);
        right.SetActive(true);
        left.SetActive(true);
    }
}
