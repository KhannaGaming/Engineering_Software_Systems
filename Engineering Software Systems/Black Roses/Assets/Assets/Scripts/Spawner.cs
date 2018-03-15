using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {


    private float t;

    public GameObject SpawningObject;
    public float timeToSpawn;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (transform.childCount == 0)
        {
            t += Time.deltaTime;
        }

        if(t >= timeToSpawn && transform.childCount ==0)
        {
            Instantiate(SpawningObject, transform);
                t = 0;
        }

	}
}
