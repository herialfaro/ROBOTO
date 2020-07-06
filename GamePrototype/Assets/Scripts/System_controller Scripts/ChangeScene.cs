using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void LoadLevel2()
    {
        SceneManager.LoadScene("Level1");
    }

    public void LoadLevel3()
    {
        SceneManager.LoadScene("Level3");
    }

    public void LoadGameOver()
    {
        SceneManager.LoadScene("GameOver");
    }
}
