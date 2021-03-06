﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {
    //----------------------------------------------------------------------------
    //OTHER
    public Transform playerTransform;
    

    // Update is called once per frame
    void Update()
    {
        if (playerTransform)
        {
            Vector3 playerPos = playerTransform.position;
            transform.position = new Vector3(Mathf.Clamp(playerPos.x, 0, 150), Mathf.Clamp(playerPos.y + 1, -15, 100), transform.position.z);
        }
    }
}
