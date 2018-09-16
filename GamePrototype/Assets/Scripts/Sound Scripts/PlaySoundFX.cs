﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundFX : MonoBehaviour {

    public AudioClip FXClip;
    private AudioSource Audio;

    public static string AudioName;
    public static bool Play;
	// Use this for initialization
	void Start () {
        Audio = this.gameObject.GetComponent<AudioSource>();
        Play = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(Play)
        {
            FXClip = (AudioClip)Resources.Load(AudioName);
            Audio.clip = FXClip;
            Audio.Play();
            Play = false;
        }
	}
}
