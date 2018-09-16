using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target_arrow : MonoBehaviour {
    // Use this for initialization
    public static GameObject arrow_plane;

    void Start () {
        arrow_plane = GameObject.Find("Arrow");
    }
	
	// Update is called once per frame
	void Update () {
        if (CamaraMouse.state_Locked)
        {
            arrow_plane.gameObject.SetActive(true);
            arrow_plane.gameObject.transform.position = new Vector3(CamaraMouse.lockOnTarget.transform.position.x, CamaraMouse.lockOnTarget.transform.position.y + 2, CamaraMouse.lockOnTarget.transform.position.z);
            arrow_plane.gameObject.transform.LookAt(this.transform.position);
            arrow_plane.gameObject.transform.Rotate(90.0f, 0.0f, 0.0f);
        }
        else
        {
            arrow_plane.gameObject.SetActive(false);
        }
    }
}
