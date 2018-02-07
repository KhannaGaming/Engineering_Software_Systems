using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffect : MonoBehaviour {

		
	// Update is called once per frame
	void Update () {
		if(!GetComponent<AudioSource>().isPlaying)
        {
            Destroy(gameObject);
        }
	}
}
