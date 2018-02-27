using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Controls : MonoBehaviour {
    //----------------------------------------------------------------------------
    //BOOLS 
    bool m_running;
    bool m_jump;
    bool m_flipped;
    bool m_BarDepleted;
    bool m_hasJetPack;
    bool m_hasUsedJetPack;

    //----------------------------------------------------------------------------
    //INTS     
    public int m_walkingSpeed;
    public int m_jumpSpeed;
    public int m_health;

    //----------------------------------------------------------------------------
    //FLOATS   
    public float m_warpRefillSpeed;
    public float Cooldown;
    float CurCooldown;
    float enemyCollisionCurCooldown;

    //----------------------------------------------------------------------------
    //OTHER 
    Rigidbody2D rb2D;
    Animator m_Animator;
    Vector3 InitialPosition;

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

        if (m_running == true)
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

    void OnTriggerExit2D(Collider2D collision)
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
    }

    void ResetPosition()
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
