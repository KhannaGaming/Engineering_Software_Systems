using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAI : MonoBehaviour
{


    Rigidbody2D rb2d;

    public Vector3 mousePos;
    public Vector3 relativePos;
    private float bulletSpeed = 10f;

    // Use this for initialization
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);


        relativePos = mousePos - GameObject.Find("Elbow").transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 normPos = relativePos.normalized;
        if (normPos.y > 0.0)
        {
            rb2d.velocity = normPos * bulletSpeed;
        }
        else
        {
            rb2d.velocity = new Vector2(normPos.x, 0) * bulletSpeed;
        }
        Destroy(gameObject, 2f);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Ground"|| collision.transform.tag == "Wall")
        {
            Destroy(gameObject);
            //play impact sound effect
        }
    }
}
