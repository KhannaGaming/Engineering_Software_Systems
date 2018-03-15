using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetPackController : MonoBehaviour {
    
    
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.name == "Player")
            {
                collision.GetComponent<Controls>().pickUpJetPack();
                transform.parent = GameObject.Find("Back").transform;
                if (collision.transform.localScale.x < 0.0f)
                {
                    GetComponent<SpriteRenderer>().flipX = true;
                }
                transform.position = GameObject.Find("Back").transform.position;
            }
        
    }

 public bool test(int x)
    {
        if (x == 1)
        { return true; }

        if (x == 2)
        { return true; }

        return false;
    }
   
}