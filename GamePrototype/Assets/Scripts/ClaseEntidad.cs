using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ClaseEntidad : MonoBehaviour
{
    private Scene CurrentScene;

    public ushort vida;
    private ushort maxVida;
    private ushort coins;
    private ushort gems;

    public Text coinText;
    public Text lifeText;
    public Text gemText;

    public PlaySoundFX CollectableSounds;
    public Reset_Scene ResetSceneManager;

    private void Start()
    {
        coins = 0;
        gems = 0;
        maxVida = vida;
        coinText.text = " + " + coins.ToString();
        gemText.text = " + " + gems.ToString();
        lifeText.text = vida.ToString();

        CurrentScene = SceneManager.GetActiveScene();
    }

    private void Update()
    {
        if (coins >= GetCoinCounter.scene_coins.Length)
        {
            if(CurrentScene.name == "Level2")
            {
                SceneManager.LoadScene("Level1");
            }
            else if (CurrentScene.name == "Level1")
            {
                SceneManager.LoadScene("Level3");
            }
            else if (CurrentScene.name == "Level3")
            {
                SceneManager.LoadScene("GameOver");
            }
        }
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
            //CollectableSounds.AudioName = "UI and Item Sound Effects/OGG/Item2B";
            CollectableSounds.AudioName = "UI and Item Sound Effects/Custom/recuperar salud";
            CollectableSounds.Play = true;
        }
        else if (other.gameObject.CompareTag("Diamond"))
        {
            other.gameObject.SetActive(false);
            gems++;
            gemText.text = " + " + gems.ToString();
            //CollectableSounds.AudioName = "UI and Item Sound Effects/OGG/Item3A";
            CollectableSounds.AudioName = "UI and Item Sound Effects/Custom/gema";
            CollectableSounds.Play = true;
        }
        else if(other.gameObject.CompareTag("Coin"))
        {
            other.gameObject.SetActive(false);
            coins++;
            coinText.text = " + " + coins.ToString();
            //CollectableSounds.AudioName = "UI and Item Sound Effects/OGG/Item1A";
            CollectableSounds.AudioName = "UI and Item Sound Effects/Custom/moneda";
            CollectableSounds.Play = true;
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit other)
    {
        if (other.gameObject.CompareTag("Lockable") && other.gameObject.layer == 8 && Character.canBeHurt && !Character.isInjured)
        {
            Debug.Log("Can't Be hurt");
            Character.canBeHurt = false;
            Character.isInjured = true;
            vida--;
            if (vida == 0)
            {
                SceneManager.LoadScene("GameOver");
            }
            lifeText.text = vida.ToString();
            CollectableSounds.AudioName = "UI and Item Sound Effects/Custom/daño";
            CollectableSounds.Play = true;
        }
    }
};
