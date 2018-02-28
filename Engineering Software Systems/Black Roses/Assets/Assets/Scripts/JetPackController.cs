﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetPackController : MonoBehaviour {
    
    
    private void OnTriggerStay2D(Collider2D collision)
    {if(Input.GetKey("e"))
            {
        if(collision.name == "Player")
            {
                collision.GetComponent<Controls>().pickUpJetPack();
                transform.parent = GameObject.Find("Back").transform;
                GetComponent<SpriteRenderer>().flipX = true;
                transform.position = GameObject.Find("Back").transform.position;
            }
        }
    }
}
