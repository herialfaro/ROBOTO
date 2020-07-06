using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetCoinCounter : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameObject[] scene_coins;
    void Start()
    {
        scene_coins = GameObject.FindGameObjectsWithTag("Coin");
    }
}
