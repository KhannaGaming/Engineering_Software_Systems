using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    public Transform playerTransform;
    

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 playerPos = playerTransform.position;
        transform.position = new Vector3(Mathf.Clamp(playerPos.x, 0, 100), Mathf.Clamp(playerPos.y,-10,100), transform.position.z); 
	}
}
