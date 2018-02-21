using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAI : MonoBehaviour
{


    Rigidbody2D rb2d;

    private Vector3 Direction;
    private Vector3 relativePos;
    private float bulletSpeed = 10f;

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

        rb2d.velocity = new Vector2(relativePos.normalized.x * bulletSpeed,relativePos.normalized.y *bulletSpeed);

        Destroy(gameObject, 2f);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Ground"|| collision.transform.tag == "Wall" || collision.transform.tag == "Destructible" || collision.transform.tag == "Enemy")
        {
            Destroy(gameObject);
            //play impact sound effect
        }
    }
}
