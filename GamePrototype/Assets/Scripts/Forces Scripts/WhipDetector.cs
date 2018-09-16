using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//GRAPLING HOOK CODE BY Sykoo on Youtube //

public class WhipDetector : MonoBehaviour {

    public GameObject player;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Lockable")
        {
            //player.GetComponent<Character>().moveSpeed = 0;
            CamaraMouse.SelectStateEnabled = true;
            player.GetComponent<WhipBase>().isHooked = true;
            player.GetComponent<WhipBase>().hookedObj = other.gameObject;
        }
    }
}
