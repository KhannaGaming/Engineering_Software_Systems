using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetPackController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    
    private void OnTriggerStay2D(Collider2D collision)
    {if(Input.GetKey("e"))
            {
        if(collision.name == "Player")
            {
                collision.GetComponent<Controls>().pickUpJetPack();
                Destroy(gameObject);
            }
        }
    }
}
