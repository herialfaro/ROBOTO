using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubirPared : MonoBehaviour {
    
    //Falta agregar que cunado se agarre cancele todo input hasta que termine el movimiento.

    public bool SeAgarro = false;
    public bool Subio = false;
    public GameObject Player = null;

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
        
        if (SeAgarro) //si se agarro el personaje sube
        {
            Player.GetComponent<CharacterController>().Move(Vector3.up * 0.2f);
        }
        if (Subio) // si ya subio el jugador es dezplazado hacia enfrente para que quede sobre la plataforma
        {
            Player.GetComponent<CharacterController>().Move(Vector3.forward * 0.1f);
            Invoke("Parar", 0.3f);
        }
    }


    //Cuando entra al trigger
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "EdgeGrab" && gameObject.transform.parent.transform.position.y <= other.transform.position.y) //si tiene tag edge grab y la posiciond el jugador es menor a la de el edge
        {
            SeAgarro = true;
        }
        if (other.tag == "EdgeGrab" && gameObject.transform.parent.transform.position.y > other.transform.position.y) //si la posision del jugador es mayor al edge entonces no se agarra. sin esto el jugador no se peude tirar del edge porque el trigger lo vovleria a subir
        {
            SeAgarro = false;
        }
    }

    //cuando termine de subir
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "EdgeGrab" && gameObject.transform.parent.transform.position.y >= other.transform.position.y) //Solo si su posision es mayor a la del edge
        {
            Invoke("PararAgarre", 0.1f); //vovler falso el booleano de agarre
            Subio = true;
        }
        if (other.tag == "EdgeGrab" && gameObject.transform.parent.transform.position.y < other.transform.position.y) // si el jugador esta abajo del edge todavia, entonces "Subio" es falso
        {
            SeAgarro = false;
            Subio = false;
        }
    }
    

    void Parar()
    {
        Subio = false;
    }
    void PararAgarre()
    {
        SeAgarro = false;
    }
}
