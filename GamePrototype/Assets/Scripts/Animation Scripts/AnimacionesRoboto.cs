using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AnimacionesRoboto : MonoBehaviour {

    Animator animator;
    Character character_controller;
    public static bool punch_enabled;
    public short floor_type;

    private float timespawn;

    // Use this for initialization
    void Start()
    {
        character_controller = GameObject.Find("Roboto_DAEFBX").GetComponent<Character>();
        animator = GetComponent<Animator>();
        punch_enabled = true;
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetButton("Punch"))
        {
            animator.SetBool("isAtacando1", true);
            timespawn = 0.3f;
            punch_enabled = true;
        }
        else if(Input.GetButton("Jump"))
        {
            animator.SetBool("isJumping1", true);
            animator.SetBool("isAtacando1", false);
            animator.SetBool("isMoving1", false);
            animator.SetBool("isMovingGrass1", false);
        }
        else
        {
            animator.SetBool("isJumping1", false);
            animator.SetBool("isAtacando1", false);
            animator.SetBool("isMoving1", false);
            animator.SetBool("isMovingGrass1", false);
        }

        if(Input.GetButton("Horizontal"))
        {
            if(floor_type == 0)
            {
                animator.SetBool("isMovingGrass1", true);
            }
            else if (floor_type == 1)
            {
                animator.SetBool("isMoving1", true);
            }
        }
        else if (Input.GetButton("Vertical"))
        {
            if (floor_type == 0)
            {
                animator.SetBool("isMovingGrass1", true);
            }
            else if (floor_type == 1)
            {
                animator.SetBool("isMoving1", true);
            }
        }
        else
        {
            animator.SetBool("isMoving1", false);
            animator.SetBool("isMovingGrass1", false);
        }

        if (timespawn <= 0)
        {
            timespawn = 0.3f;
            punch_enabled = false;
        }
        else
        {
            timespawn -= Time.deltaTime;
        }
    }
}
