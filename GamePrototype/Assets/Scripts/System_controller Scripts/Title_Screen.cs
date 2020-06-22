using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title_Screen : MonoBehaviour
{
    public void LoadFirstScene()
    {
        SceneManager.LoadScene("Level2");
        Debug.Log("Hello");
    }
}
