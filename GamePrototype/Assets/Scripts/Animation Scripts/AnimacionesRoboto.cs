using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AnimacionesRoboto : MonoBehaviour {

    Animator animator;
    Character character_controller;
    public static bool punch_enabled;

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
        if (Input.GetButtonDown("Punch"))
        {
            animator.SetBool("isAtacando1", true);
            timespawn = 0.5f;
            punch_enabled = true;
        }
        else
        {
            animator.SetBool("isAtacando1", false);
        }

        if(character_controller.state == Character.Moving)
        {

        }
        else if(character_controller.state == Character.Jumping)
        {

        }
        else if(character_controller.state == Character.Falling)
        {

        }

        if (timespawn <= 0)
        {
            timespawn = 0.5f;
            punch_enabled = false;
        }
        else
        {
            timespawn -= Time.deltaTime;
        }
    }
}
