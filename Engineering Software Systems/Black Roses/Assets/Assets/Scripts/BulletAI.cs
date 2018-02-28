using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAI : MonoBehaviour
{
    //----------------------------------------------------------------------------
    //BOOLS 
    private float m_bulletSpeed = 10f;

    //----------------------------------------------------------------------------
    //OTHER
    private Rigidbody2D rb2d;
    private Vector3 Direction;
    private Vector3 relativePos;
   

    // Use this for initialization
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        Direction = GameObject.Find("Direction").transform.position;

        relativePos = Direction - GameObject.Find("FirePoint").transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        rb2d.velocity = new Vector2(relativePos.normalized.x * m_bulletSpeed,relativePos.normalized.y *m_bulletSpeed);

        Destroy(gameObject, 2f);

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Ground"|| collision.transform.tag == "Wall" || collision.transform.tag == "Destructible" || collision.transform.tag == "Enemy")
        {
            Destroy(gameObject);
            //play impact sound effect
        }
    }
}
