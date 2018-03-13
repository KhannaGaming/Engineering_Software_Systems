using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallColliderScript : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
         if (collision.transform.tag == "Player")
        {
            Physics2D.IgnoreCollision(collision.transform.GetComponent<CircleCollider2D>(), GetComponent<BoxCollider2D>());
        }
    }       
}
