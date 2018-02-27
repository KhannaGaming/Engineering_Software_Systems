﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManangement : MonoBehaviour {

    //----------------------------------------------------------------------------
    //OTHER 
    public GameObject soundPrefab;

    //----------------------------------------------------------------------------
    //LISTS 
    public List<string> audioName = new List<string>();
    public List<AudioClip> audioFile = new List<AudioClip>();
    public List<float> volume = new List<float>();

    //----------------------------------------------------------------------------
    //DICTIONARIES
    Dictionary<string, AudioClip> dicAudioSources = new Dictionary<string, AudioClip>();
    Dictionary<string, float> dicAudioSourcesVolumes = new Dictionary<string, float>();

    // Use this for initialization
    void Start () {

        for (int i = 0; i < audioName.Count; i++)
        {
            dicAudioSources.Add(audioName[i], audioFile[i] );
        }
        for (int i = 0; i < audioFile.Count; i++)
        {
            dicAudioSourcesVolumes.Add(audioName[i], volume[i]);
        }
    }
	
	// Update is called once per frame
	void Update () {

	}

    public void spawnAudio(string soundName)
    {
       GameObject soundObject = Instantiate(soundPrefab);
        soundObject.transform.parent = gameObject.transform;
        soundObject.AddComponent<AudioSource>();
        soundObject.GetComponent<AudioSource>().clip = dicAudioSources[soundName];
        soundObject.GetComponent<AudioSource>().volume = dicAudioSourcesVolumes[soundName];
        soundObject.GetComponent<AudioSource>().Play();
    }    
}
