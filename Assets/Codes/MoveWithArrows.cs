using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWithArrows : MonoBehaviour
{
    public GameObject player;
    public float speed;

    private Rigidbody2D myRigidbody;

    private Vector3 change;

    private Animator animator;

    public float moveX;

    public float moveY;
    // Start is called before the first frame update
    void Start()
    {
        animator = player.GetComponent<Animator>();
        myRigidbody = player.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void move()
    {
        change = Vector2.zero;
        change.x = 1;
        change.y = moveY;
        UpdateAnimationAndMove();
        Debug.Log("what");
    }
    public void UpdateAnimationAndMove()
    {
        if (change != Vector3.zero)
        {
            MoveCharacter();
            animator.SetFloat("moveX", change.x);
            animator.SetFloat("moveY", change.y);
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
}

