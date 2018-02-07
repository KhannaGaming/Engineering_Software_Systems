using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManangement : MonoBehaviour {

    public GameObject soundPrefab;
    public List<string> audioName = new List<string>();
    public List<AudioClip> audioFile = new List<AudioClip>();

    private Dictionary<string, AudioClip> dicAudioSources = new Dictionary<string, AudioClip>();


    // Use this for initialization
    void Start () {

        for (int i = 0; i < audioName.Count+1; i++)
        {
            dicAudioSources.Add(audioName[i], audioFile[i] );
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
        soundObject.GetComponent<AudioSource>().Play();
    }

    
}
