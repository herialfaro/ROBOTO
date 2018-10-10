using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause_game : MonoBehaviour {
    bool pause;

    // Use this for initialization
    void Start () {
        pause = false; // pausa por default está apagada
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Escape) & pause == false) // si el usuario presiona esc y no hay pausa
        {
            pause = true; // el juego está pausado y el tiempo se detiene dentro del juego
            Time.timeScale = 0; // detener el tiempo
        }
        
        else if(Input.GetKeyDown(KeyCode.Escape) & pause == true)
        {
            pause = false; // regresar pausa a falso
            Time.timeScale = 1; // regresar el tiempo a la normalidad
        }
 
  
	}
  
}
