using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class staminaBars : MonoBehaviour {
    //----------------------------------------------------------------------------
    //OTHER 
    public GameObject playerGO;
    Vector3 playerPosition;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (playerGO)
        {
            playerPosition = playerGO.transform.position;
            transform.position = new Vector3(playerPosition.x + 4.0f, playerPosition.y + 2.0f, playerPosition.z);
        }
	}
}
