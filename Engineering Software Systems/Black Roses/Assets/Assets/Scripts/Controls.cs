using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Controls : MonoBehaviour {
    //----------------------------------------------------------------------------
    //BOOLS 
    private bool m_running;
    private bool m_jump;
    private bool m_flipped;
    private bool m_BarDepleted;
    private bool m_hasJetPack;
    private bool m_hasUsedJetPack;

    //----------------------------------------------------------------------------
    //INTS     
    private int m_walkingSpeed;
    private int m_jumpSpeed;
    private int m_health;

    //----------------------------------------------------------------------------
    //FLOATS   
    private float m_warpRefillSpeed;
    public float Cooldown;
    private float CurCooldown;
    private float enemyCollisionCurCooldown;

    //----------------------------------------------------------------------------
    //OTHER 
    private Rigidbody2D rb2D;
    private Animator m_Animator;
    private Vector3 InitialPosition;

    // Use this for initialization
    void Start () {
        m_Animator = gameObject.GetComponent<Animator>();
        m_running = false;
        m_jump = true;
        m_flipped = false;
        m_hasJetPack = false;
        m_hasUsedJetPack = false;
        m_BarDepleted = false;
        m_health = 100;
        m_walkingSpeed = 3;
        m_jumpSpeed = 10;
        m_warpRefillSpeed = 0.001f;
        rb2D = GetComponent<Rigidbody2D>();
        InitialPosition = transform.localPosition;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey("escape"))
        {
            SceneManager.LoadScene("Main_Menu");
        }        

        if(Mathf.Abs(Input.GetAxis("Horizontal")) > 0)
        {
                rb2D.velocity = new Vector2(Input.GetAxis("Horizontal")*m_walkingSpeed, rb2D.velocity.y);
                m_running = true;
        }
        else
        {
            m_running = false;
            rb2D.velocity = new Vector2(0, rb2D.velocity.y);
        }
      
        if(Input.GetButton("Left Shift"))
        {
            if (Time.timeScale == 1.0f &! m_BarDepleted)
            {
                CurCooldown = 0;
                m_BarDepleted = true;
            }

                CurCooldown += Time.deltaTime;
                if (CurCooldown < Cooldown)
                {
                    Time.timeScale = 0.5f;
                    GameObject.Find("BarTime").transform.localScale -= new Vector3(0.003f, 0.003f, 0);
                    
                }
                else
                {
                    Time.timeScale = 1.0f;
                }
            
        }
        else
        {
            
            if (Time.timeScale != 1.0f || m_BarDepleted)
            {
                Time.timeScale = 1.0f;

            }
                m_BarDepleted = false;
           
                GameObject.Find("BarTime").transform.localScale += new Vector3(m_warpRefillSpeed, m_warpRefillSpeed, 0);
            
        }

        if (m_running == false)
        {
            m_Animator.SetBool("Running", false);
        }
        else
        {
            m_Animator.SetBool("Running", true);
        }

    //Jumping
        
     
        

        if (Input.GetKeyDown("space") && m_jump == false)
        {
            m_jump = true;
            rb2D.velocity = new Vector2(rb2D.velocity.x,m_jumpSpeed);

        }
        else if (Input.GetKeyDown("space") && m_jump == true)
        {
            if(m_hasJetPack && m_hasUsedJetPack == false)
            {
                rb2D.velocity = new Vector2(rb2D.velocity.x, m_jumpSpeed*2 );
                m_hasUsedJetPack = true;
            }
        }


    //Jump animation
        if (m_jump == false)
        {
            m_Animator.SetBool("Jump", false);
        }
        else
        {
            m_Animator.SetBool("Jump", true);
        }


        GameObject.Find("BarTime").transform.localScale = new Vector3(Mathf.Clamp(GameObject.Find("BarTime").transform.localScale.x, 0, 1),
                                                                        Mathf.Clamp(GameObject.Find("BarTime").transform.localScale.y, 0, 1),
                                                                        0);

        if(m_health <= 0)
        {
            SceneManager.LoadScene("Main_Menu");
        }
        else
        {
            
            GameObject.Find("Heart").GetComponent<Image>().sprite = Resources.Load<Sprite>("Health"+Mathf.CeilToInt(m_health/10));
        }
    }//End of Update...

    IEnumerator FlipRight()
    {
        if (m_flipped == true)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            m_flipped = false;
            yield return 0;
        }
    }

    IEnumerator FlipLeft()
    {
        if (m_flipped == false)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            m_flipped = true;
            yield return 0;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Ground")
        {
            m_jump = false;
            m_hasUsedJetPack = false;
        }
        if (collision.transform.name == "KillZone")
        {
            ResetPosition();
        }
        
        if(collision.transform.tag == "Spike")
        {
            transform.localPosition =   InitialPosition;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            enemyCollisionCurCooldown += Time.deltaTime;
            if (enemyCollisionCurCooldown < Cooldown)
            {
                //  transform.position += new Vector3(-200.0f * Time.deltaTime, 0,0);
                rb2D.AddForce(new Vector2(20,0));
                m_health -= 20;
                enemyCollisionCurCooldown = 0;
            }
        }
        if (collision.transform.tag == "EnemyBullet")
        {
            m_health -= 10;
        }
        if(collision.transform.name == "EndGame")
        {
            SceneManager.LoadScene("Main_Menu");
        }
    }

    private void ResetPosition()
    {
        transform.position = new Vector3(-4.34f,-1.49f,0.0f);
    }

    public void pickUpJetPack()
    {
        m_hasJetPack = true;
    }

    public void flipPlayerLeft()
    {
        StopAllCoroutines();
        StartCoroutine(FlipLeft());
    }

    public void flipPlayerRight()
    {
        StopAllCoroutines();
        StartCoroutine(FlipRight());
    }
}
