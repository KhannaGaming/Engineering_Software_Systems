using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class staminaBars : MonoBehaviour {
    //----------------------------------------------------------------------------
    //OTHER 
    public GameObject playerGO;
    private Vector3 playerPosition;
	
	// Update is called once per frame
	void Update () {
        if (playerGO)
        {
            playerPosition = playerGO.transform.position;
            transform.position = new Vector3(playerPosition.x + 4.0f, playerPosition.y + 2.0f, playerPosition.z);
        }
	}
}
