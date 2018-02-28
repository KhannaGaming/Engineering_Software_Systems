using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    //----------------------------------------------------------------------------
    //BOOLS 
    public bool m_spotted;

    //----------------------------------------------------------------------------
    //FLOATS 
    public float m_range;
    public float m_minX;
    public float m_maxX;
    public float m_speed;
    public float Cooldown;
    private float CurCooldown = 3;

    //----------------------------------------------------------------------------
    //INTS 
    public int m_health;

    //----------------------------------------------------------------------------
    //OTHER 
    public Transform bulletPrefab;
    public Transform firePointTransform;
    private Rigidbody2D rb2d;
    private GameObject MyRayStart;
    private Animator m_Animator;
    private GameObject EnemyFirePoint;

    // Use this for initialization
    void Start () {
        m_health = 100;
        rb2d = GetComponent<Rigidbody2D>();
        m_Animator = GetComponent<Animator>();

        MyRayStart = transform.GetChild(0).gameObject;

        EnemyFirePoint = transform.GetChild(1).gameObject;
    }
	
	// Update is called once per frame
	void Update () {
        Raycasting();

        if(m_health<0)
        {
           // animator.SetBool("m_dead", true);
            Destroy(gameObject,0.833f);
        }
        if (!m_spotted)
        {
            if (transform.position.x < m_minX || transform.position.x > m_maxX)
            {
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, 0);
            }

            if (transform.localScale.x < 0)
            {
                rb2d.velocity = new Vector2(-m_speed, 0);
                m_Animator.SetBool("m_running", true);
                EnemyFirePoint.transform.eulerAngles = new Vector3(0, 0, 180);


            }
            else
            {
                rb2d.velocity = new Vector2(m_speed, 0);
                m_Animator.SetBool("m_running", true);
                EnemyFirePoint.transform.eulerAngles = new Vector3(0, 0, 0);
            }
        }
        else
        {
            m_Animator.SetBool("m_running", false);
            m_Animator.SetBool("m_shooting", true);

            CurCooldown += Time.deltaTime;
            if (CurCooldown > Cooldown)
            {
                Instantiate(bulletPrefab, firePointTransform.position, firePointTransform.rotation);
                CurCooldown = 0;
            }
        }

        

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, m_minX, m_maxX), transform.position.y, 0);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.transform.tag == "Bullet")
        {
            m_health -= 2;
        }
        
    }

    void OnCollisionEnter2D(Collision2D collision)
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
