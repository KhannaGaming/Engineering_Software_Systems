using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {


    private Rigidbody2D rb2d;
    private GameObject MyRayStart;
    private Animator animator;
    private GameObject EnemyFirePoint;

    public float m_range;
    public bool m_spotted;



    public float speed;
    public int health;
    public float minX;
    public float maxX;

    public float Cooldown;
    private float CurCooldown = 3;

    public Transform bulletPrefab;
    public Transform firePointTransform;

    // Use this for initialization
    void Start () {
        health = 100;
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        MyRayStart = transform.GetChild(0).gameObject;

        EnemyFirePoint = transform.GetChild(1).gameObject;
    }
	
	// Update is called once per frame
	void Update () {
        Raycasting();

        if(health<0)
        {
           // animator.SetBool("m_dead", true);
            Destroy(gameObject,0.833f);
        }
        if (!m_spotted)
        {
            if (transform.position.x < minX || transform.position.x > maxX)
            {
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, 0);
            }

            if (transform.localScale.x < 0)
            {
                rb2d.velocity = new Vector2(-speed, 0);
                animator.SetBool("m_running", true);
                EnemyFirePoint.transform.eulerAngles = new Vector3(0, 0, 180);


            }
            else
            {
                rb2d.velocity = new Vector2(speed, 0);
                animator.SetBool("m_running", true);
                EnemyFirePoint.transform.eulerAngles = new Vector3(0, 0, 0);
            }
        }
        else
        {
            animator.SetBool("m_running", false);
            animator.SetBool("m_shooting", true);

            CurCooldown += Time.deltaTime;
            if (CurCooldown > Cooldown)
            {
                Instantiate(bulletPrefab, firePointTransform.position, firePointTransform.rotation);
                CurCooldown = 0;
            }
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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Enemy"|| collision.transform.tag == "Player")
        {
            Physics2D.IgnoreCollision(collision.transform.GetComponent<CircleCollider2D>(), GetComponent<CircleCollider2D>());
        }
    }
    private void Raycasting()
    {
        Vector3 EndPosition = MyRayStart.transform.position + new Vector3(m_range, 0, 0) * transform.localScale.x;
        Debug.DrawLine(MyRayStart.transform.position, EndPosition, Color.green);
        m_spotted = Physics2D.Linecast(MyRayStart.transform.position, EndPosition, 1 << LayerMask.NameToLayer("Player"));
        
    }
}
