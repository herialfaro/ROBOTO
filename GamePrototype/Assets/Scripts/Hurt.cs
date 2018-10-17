using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurt : MonoBehaviour {
    public bool verCanbeHurt = Character.canBeHurt;
    public bool verinjured = Character.isInjured;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        verinjured = Character.isInjured;
        verCanbeHurt = Character.canBeHurt;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && Character.canBeHurt)
        {
            Character.canBeHurt = false;
            Character.isInjured = true;
            Debug.Log("Can't Be hurt");
        }
    }
}
