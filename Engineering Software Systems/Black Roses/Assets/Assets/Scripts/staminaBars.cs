using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class staminaBars : MonoBehaviour {

    public GameObject playerGO;
    private Vector3 playerPosition;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        playerPosition = playerGO.transform.position;
        transform.position = new Vector3(playerPosition.x+4.0f, playerPosition.y+2.0f, playerPosition.z);
	}
}
