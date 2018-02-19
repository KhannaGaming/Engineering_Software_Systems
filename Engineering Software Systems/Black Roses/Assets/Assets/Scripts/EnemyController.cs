using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {


    private Rigidbody2D rb2d;
    private Animator animator;
    public float speed;
    public int health;
    public float minX;
    public float maxX;

	// Use this for initialization
	void Start () {
        health = 100;
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        

        if(health<0)
        {
           // animator.SetBool("m_dead", true);
            Destroy(gameObject,0.833f);
        }

    if(transform.position.x <minX || transform.position.x > maxX)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, 0);
        }

        if (transform.localScale.x < 0)
        {
            rb2d.velocity = new Vector2(-speed,0);
            animator.SetBool("m_running", true);
        }
        else
        {
            rb2d.velocity = new Vector2(speed, 0);
            animator.SetBool("m_running", true);
        }


        

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, minX, maxX), transform.position.y, 0);
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.transform.tag == "Bullet")
        {
            health -= 2;
        }
    }
}
