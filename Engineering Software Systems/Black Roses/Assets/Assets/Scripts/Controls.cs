using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Controls : MonoBehaviour {
    //----------------------------------------------------------------------------
    //BOOLS 

    private bool m_jump;
    private bool m_flipped;
    private bool m_BarDepleted;
    private bool m_hasJetPack;
    private bool m_hasUsedJetPack;
    private bool m_immune;
    private bool m_knockedBack;

    //----------------------------------------------------------------------------
    //INTS     
    private int m_walkingSpeed;
    private int m_jumpSpeed;
    private int m_health;

    //----------------------------------------------------------------------------
    //FLOATS   
    public float Cooldown;
    public float immuneCooldown;
    private float CurCooldown;
    private float immuneCurCooldown;
    private float transparencyValue = 0.02f;

    //----------------------------------------------------------------------------
    //OTHER 
    private Rigidbody2D rb2D;
    private Animator m_Animator;
    private Vector3 previousPosition;
    private Transform elbowTransform;

    // Use this for initialization
    void Start () {
        m_Animator = gameObject.GetComponent<Animator>();
        m_jump = false;
        m_flipped = false;
        m_hasJetPack = false;
        m_hasUsedJetPack = false;
        m_BarDepleted = false;
        m_immune = false;
        m_knockedBack = false;
        m_health = 100;
        m_walkingSpeed = 3;
        m_jumpSpeed = 10;
        rb2D = GetComponent<Rigidbody2D>();
        elbowTransform = GameObject.Find("Elbow").transform;
    }
	
	// Update is called once per frame
	void Update () {
        
        immuneTesting();

        playerInput();
       

    //Jumping
        
     
        

        if (Input.GetKeyDown("space") && m_jump == false)
        {
            m_jump = true;
            if (!m_knockedBack)
            {
                rb2D.velocity = new Vector2(rb2D.velocity.x, m_jumpSpeed);
            }

        }
        else if (Input.GetKeyDown("space") && m_jump == true)
        {
            if(m_hasJetPack && m_hasUsedJetPack == false)
            {
                if (!m_knockedBack)
                {
                    rb2D.velocity = new Vector2(rb2D.velocity.x, m_jumpSpeed * 2);
                }
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

        //Boundaries for the BarTime
        GameObject.Find("BarTime").transform.localScale = new Vector3(Mathf.Clamp(GameObject.Find("BarTime").transform.localScale.x, 0, 1),
                                                                        Mathf.Clamp(GameObject.Find("BarTime").transform.localScale.y, 0, 1),
                                                                        0);
        //If Dead
        if(m_health <= 0)
        {
            SceneManager.LoadScene("Main_Menu");
        }
        else
        {
            
            GameObject.Find("Heart").GetComponent<Image>().sprite = Resources.Load<Sprite>("Hearts/Health"+Mathf.CeilToInt(m_health/10));
        }
    }//End of Update...

    IEnumerator FlipRight()
    {
        if (m_flipped == true)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            elbowTransform.localScale = new Vector3(-elbowTransform.localScale.x, -elbowTransform.localScale.y, elbowTransform.localScale.z);
            elbowTransform.localPosition = new Vector3(-elbowTransform.localPosition.x, -elbowTransform.localPosition.y, elbowTransform.localPosition.z);
            m_flipped = false;
            yield return 0;
        }
    }

    IEnumerator FlipLeft()
    {
        if (m_flipped == false)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            elbowTransform.localScale = new Vector3(-elbowTransform.localScale.x, -elbowTransform.localScale.y, elbowTransform.localScale.z);
            elbowTransform.localPosition = new Vector3(-elbowTransform.localPosition.x, -elbowTransform.localPosition.y, elbowTransform.localPosition.z);
            m_flipped = true;
            yield return 0;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Enemy" && !m_immune)
        {
            m_health -= 20;
            gameObject.layer = 9;
            m_immune = true;
        }
        else if (collision.transform.tag == "EnemyBullet" && !m_immune)
        {
            m_health -= 10;
            gameObject.layer = 9;
            m_immune = true;
        }
        else if (collision.transform.name == "Musket" && !m_immune)
        {
            m_health -= 5;
            gameObject.layer = 9;
            m_immune = true;
        }

        if (collision.transform.tag == "Ground"|| collision.transform.tag == "Destructible")
        {
            m_jump = false;
            m_hasUsedJetPack = false;
        }
        else if (collision.transform.name == "KillZone")
        {
            ResetPosition();
        }        
        else if(collision.transform.tag == "Spike")
        {
            SceneManager.LoadScene("Level01");
        }

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        
        

        if(collision.transform.name == "EndGame")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else if (collision.transform.name == "MainMenu")
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

    public bool flipPlayerLeft()
    {
        StopAllCoroutines();
        StartCoroutine(FlipLeft());
        return true;
    }

    public void flipPlayerRight()
    {
        StopAllCoroutines();
        StartCoroutine(FlipRight());
    }

    private void immuneTesting()
    {
        //Immune Layer
        if (gameObject.layer == 9)
        {


            if (GetComponent<SpriteRenderer>().color.a < 0.0f)
            {
                transparencyValue *= -1.0f;
            }
            else if (GetComponent<SpriteRenderer>().color.a > 1.0f)
            {
                transparencyValue *= -1.0f;
            }
            GetComponent<SpriteRenderer>().color -= new Color(0, 0, 0, transparencyValue);
            GameObject.Find("Gun").GetComponent<SpriteRenderer>().color -= new Color(0, 0, 0, transparencyValue);
            m_immune = true;
        }
        else
        {
            m_immune = false;
        }


        if (m_immune == true)
        {
            gameObject.layer = 9;
            immuneCurCooldown += Time.deltaTime;
            if (immuneCurCooldown > immuneCooldown)
            {
                m_immune = false;
                gameObject.layer = 8;
                GetComponent<SpriteRenderer>().color = new Color(GetComponent<SpriteRenderer>().color.r, GetComponent<SpriteRenderer>().color.g, GetComponent<SpriteRenderer>().color.b, 1.0f);
                GameObject.Find("Gun").GetComponent<SpriteRenderer>().color = new Color(GameObject.Find("Gun").GetComponent<SpriteRenderer>().color.r, GameObject.Find("Gun").GetComponent<SpriteRenderer>().color.g, GameObject.Find("Gun").GetComponent<SpriteRenderer>().color.b, 1.0f);
                immuneCurCooldown = 0;
            }
        }

    }

    private void playerInput()
    {
        //Input
        if (GameObject.Find("Hitler"))
        {
            float distance = Mathf.Sqrt(Mathf.Pow((previousPosition.x - transform.position.x),2.0f)+Mathf.Pow((previousPosition.x - transform.position.x),2.0f));
            if (distance >= 5)
            {
                m_knockedBack = false;
            }
        } 

        if (Input.GetKey("escape"))
        {
            SceneManager.LoadScene("Main_Menu");
        }

        if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0)
        {
            if (!m_knockedBack)
            {
                rb2D.velocity = new Vector2(Input.GetAxis("Horizontal") * m_walkingSpeed, rb2D.velocity.y);
            }
            m_Animator.SetBool("Running", true);
        }
        else
        {
            if (!m_knockedBack)
            {
                rb2D.velocity = new Vector2(0, rb2D.velocity.y);
            }
            m_Animator.SetBool("Running", false);
        }

        if (Input.GetButton("Left Shift"))
        {
            if (Time.timeScale == 1.0f & !m_BarDepleted)
            {
                CurCooldown = 0;
                m_BarDepleted = true;
            }

            CurCooldown += Time.deltaTime;
            if (CurCooldown < Cooldown && GameObject.Find("BarTime").transform.localScale.x > 0)
            {
                Time.timeScale = 0.5f;
                GameObject.Find("BarTime").transform.localScale -= new Vector3(Time.deltaTime / Cooldown, Time.deltaTime / Cooldown, 0);

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

            GameObject.Find("BarTime").transform.localScale += new Vector3(Time.deltaTime / (Cooldown*2.0f), Time.deltaTime / (Cooldown*2.0f), 0);

        }
    }//End of playerInput(..)

    public void GetKnockedBack(Vector2 knockBackDir)
    {
        previousPosition = transform.position;
        m_knockedBack = true;
        rb2D.velocity = new Vector2(knockBackDir.x*10, knockBackDir.y*10 );
    }
}
