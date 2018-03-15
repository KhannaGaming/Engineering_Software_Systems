using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    //----------------------------------------------------------------------------
    //BOOLS 
    public bool m_spotted;
    public enum enemyList { 
            Grunt,Charger,Bruiser
        };
    
     public enemyList current;
    

    //----------------------------------------------------------------------------
    //FLOATS 
    public float m_range;
    public float m_speed;
    public float Cooldown;
    private float CurCooldown;
    private float animationLength;

    //----------------------------------------------------------------------------
    //INTS 
    public int m_health;

    //----------------------------------------------------------------------------
    //OTHER 
    public Transform bulletPrefab;
    private Rigidbody2D rb2d;
    private GameObject MyRayStart;
    private Animator m_Animator;
    private GameObject EnemyFirePoint;
    private RuntimeAnimatorController ac;

    public void Construct(int health)
    {
        m_health = health;
    }

    // Use this for initialization
    void Start () {
     
        rb2d = GetComponent<Rigidbody2D>();
        m_Animator = GetComponent<Animator>();
        m_Animator.SetBool("m_running", true);
        MyRayStart = transform.GetChild(0).gameObject;
        EnemyFirePoint = transform.GetChild(1).gameObject;
        ac = m_Animator.runtimeAnimatorController;
        CurCooldown = Cooldown;
    }
	
	// Update is called once per frame
	void Update () {
        Raycasting();

        if(m_health<0)
        {
            m_Animator.SetBool("m_dead", true);
            for (int i = 0; i < ac.animationClips.Length  ; i++)
            {
                if (ac.animationClips[i].name == "Death")
                {
                    animationLength = ac.animationClips[i].length;
                }

            }
            
            Destroy(gameObject, animationLength);
        }
        if (!m_spotted)
        {

           

            if (transform.localScale.x < 0)
            {
                rb2d.velocity = new Vector2(-m_speed, 0);

                if (current.ToString() == "Grunt")
                {
                    m_Animator.SetBool("m_running", true);
                    m_Animator.SetBool("m_shooting", false);
                }
                else if (current.ToString() == "Charger")
                {
                    m_Animator.SetBool("m_charging", false);
                }
                else if (current.ToString() == "Bruiser")
                {
                    //@ToDo
                }

                EnemyFirePoint.transform.eulerAngles = new Vector3(0, 0, 180);


            }
            else
            {
                rb2d.velocity = new Vector2(m_speed, 0);

                if (current.ToString() == "Grunt")
                {
                    m_Animator.SetBool("m_running", true);
                    m_Animator.SetBool("m_shooting", false);
                }
                else if (current.ToString() == "Charger")
                {
                    m_Animator.SetBool("m_charging", false);
                }
                else if (current.ToString() == "Bruiser")
                {
                    //@ToDo
                }

                EnemyFirePoint.transform.eulerAngles = new Vector3(0, 0, 0);
            }
        }
        else
        {


            if (current.ToString() == "Grunt")
            {
                rb2d.velocity = new Vector2(0, 0);
                m_Animator.SetBool("m_running", false);
                m_Animator.SetBool("m_shooting", true);

                CurCooldown += Time.deltaTime;

                if (CurCooldown > Cooldown && m_health > 0)
                {
                    Instantiate(bulletPrefab, EnemyFirePoint.transform.position, EnemyFirePoint.transform.rotation);
                    GameObject.Find("AudioManager").GetComponent<AudioManangement>().spawnAudio("enemyBullet");
                    CurCooldown = 0;
                }
            }
            else if (current.ToString() == "Charger")
            {
                float tempSpeed = m_speed * 2;
                m_Animator.SetBool("m_charging", true);
              
                if (transform.localScale.x < 0)
                {
                    rb2d.velocity = new Vector2(-tempSpeed, 0);
                    EnemyFirePoint.transform.eulerAngles = new Vector3(0, 0, 180);
                }
                else
                {
                    rb2d.velocity = new Vector2(tempSpeed, 0);
                    EnemyFirePoint.transform.eulerAngles = new Vector3(0, 0, 0);
                }
            }
            else if (current.ToString() == "Bruiser")
            {
                //@ToDo
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.transform.tag == "Bullet")
        {
            m_health -= 10;
        }
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Enemy"|| collision.transform.tag == "Player")
        {
            Physics2D.IgnoreCollision(collision.transform.GetComponent<CircleCollider2D>(), GetComponent<CircleCollider2D>());
        }

        if(collision.transform.tag == "EnemyWall"||collision.transform.tag == "Destructible")
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, 0);
        }
    }

    private void Raycasting()
    {
        Vector3 EndPosition = MyRayStart.transform.position + new Vector3(m_range, 0, 0) * transform.localScale.x;
        Debug.DrawLine(MyRayStart.transform.position, EndPosition, Color.green);
        m_spotted = Physics2D.Linecast(MyRayStart.transform.position, EndPosition, 1 << LayerMask.NameToLayer("Player"));
        
    }
}
