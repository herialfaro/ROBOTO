using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System.Runtime.InteropServices;

public class Reset_Scene : MonoBehaviour {

    private Scene CurrentScene;

	// Use this for initialization
	void Start () {
        CurrentScene = SceneManager.GetActiveScene();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.Backspace))
        {
            Load();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        Load();
    }

    private void Load()
    {
        SceneManager.LoadScene(CurrentScene.name);
    }
}
