using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletAI : MonoBehaviour
{


    Rigidbody2D rb2d;
    private Vector3 playerPos;
    private Vector3 relativePos;

    public float m_bulletSpeed = 10f;

    // Use this for initialization
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();

        playerPos = GameObject.Find("Player").transform.position;


        relativePos = playerPos - transform.position;

    }

    // Update is called once per frame
    void Update()
    {        
            rb2d.velocity = new Vector3(m_bulletSpeed* relativePos.normalized.x,0, 0) ;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Ground"|| collision.transform.tag == "Wall" || collision.transform.tag == "Destructible" || collision.transform.tag == "Player")
        {
            Destroy(gameObject);
            //play impact sound effect
        }
    }
}
