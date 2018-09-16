using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClaseEntidad : MonoBehaviour
{
    public ushort vida;
    private ushort maxVida;
    private ushort coins;
    private ushort gems;

    public Text coinText;
    public Text lifeText;
    public Text gemText;

    private void Start()
    {
        coins = 0;
        gems = 0;
        maxVida = vida;
        coinText.text = " + " + coins.ToString();
        gemText.text = " + " + gems.ToString();
        lifeText.text = vida.ToString();
    }

    private void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Health"))
        {
            other.gameObject.SetActive(false);
            if(vida < maxVida)
            {
                vida++;
            }
            lifeText.text = vida.ToString();
            PlaySoundFX.AudioName = "UI and Item Sound Effects/OGG/Item2B";
            PlaySoundFX.Play = true;
        }
        else if (other.gameObject.CompareTag("Diamond"))
        {
            other.gameObject.SetActive(false);
            gems++;
            gemText.text = " + " + gems.ToString();
            PlaySoundFX.AudioName = "UI and Item Sound Effects/OGG/Item3A";
            PlaySoundFX.Play = true;
        }
        else if(other.gameObject.CompareTag("Coin"))
        {
            other.gameObject.SetActive(false);
            coins++;
            coinText.text = " + " + coins.ToString();
            PlaySoundFX.AudioName = "UI and Item Sound Effects/OGG/Item1A";
            PlaySoundFX.Play = true;
        }
    }
};
