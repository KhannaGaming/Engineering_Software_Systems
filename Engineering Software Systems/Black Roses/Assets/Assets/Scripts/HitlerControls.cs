using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitlerControls : MonoBehaviour {

    //----------------------------------------------------
    //FLOATS
    public float chargeCooldown;
    private float curChargeCooldown;
    private float grenadeCooldown;
    private float curGrenadeCooldown;
    private float m_maxHealth;
    private float animationLength;
    private float m_walkingSpeed;
    private float turningAmount = 140.0f;
    private float grenadeTurningAmount = 220.0f;
    // Maximum turn rate in degrees per second.
    public float turningRate = 50f;
    //----------------------------------------------------
    //INTS
    private int m_health;

    //----------------------------------------------------------------------------
    //BOOLS
    private bool m_flippedLeft;
    private bool m_IsCharging;
    private bool m_HasReset;
    private bool m_dead;
    private bool m_HasThrownGrenade;
    private bool m_HasGrenade;
    private bool m_hasResetGrenade;

    //----------------------------------------------------------------------------
    //Other
    private Animator m_Animator;
    private RuntimeAnimatorController ac;
    private Rigidbody2D rb2D;
    private Vector3 relativePosition;
    private GameObject playerTransform;
    public Sprite[] Sprites;
    public Transform Arm;
    public Transform grenadePrefab;
    

    // Use this for initialization
    void Start () {
        m_health = 1000;
        m_walkingSpeed = 1.5f;
        grenadeCooldown = 2.0f;
        curGrenadeCooldown = 0.0f;
        m_maxHealth = m_health;
        m_flippedLeft = false;
        m_IsCharging = false;
        m_HasReset = false;
        m_dead = false;
        m_HasThrownGrenade = false;
        m_hasResetGrenade = true;
        m_Animator = gameObject.GetComponent<Animator>();
        ac = m_Animator.runtimeAnimatorController;
        rb2D = GetComponent<Rigidbody2D>();
        playerTransform = GameObject.Find("Player");
        
    }
	
	// Update is called once per frame
	void Update () {

        relativePosition = transform.position - playerTransform.transform.position;


        if (m_health > m_maxHealth / 2)
        {
            m_Animator.SetBool("m_walking", true);
            if (!m_IsCharging)
            {
                rb2D.velocity = new Vector2(m_walkingSpeed, 0);

                if ((relativePosition.x > -20 && relativePosition.x < -5) && !m_flippedLeft)
                {
                    transform.GetChild(0).GetComponent<Rotate>().rotate(turningAmount, turningRate);
                }
                else if ((relativePosition.x < 20 && relativePosition.x > 5)&& m_flippedLeft)
                {
                    transform.GetChild(0).GetComponent<Rotate>().rotate(-turningAmount, turningRate);
                }
            }

            curChargeCooldown += Time.deltaTime;
            if (relativePosition.x < 5 && relativePosition.x > -5)
            {
                transform.GetChild(0).GetComponent<Rotate>().rotateBack(turningRate);
                if (curChargeCooldown > chargeCooldown)
                {                    
                    m_IsCharging = true;                   
                    rb2D.velocity = new Vector2(m_walkingSpeed * 10, 0);                    
                    curChargeCooldown = 0;
                }

            }

            if ((relativePosition.x < -5 || relativePosition.x > 5)||(relativePosition.x <2.3f && relativePosition.x > -2.3f))
            {
                m_IsCharging = false;
            }
        }//End of Phase 1


        if (m_health <= m_maxHealth / 2 && m_health >0)
        {
            do
            {
                Arm.GetComponent<SpriteRenderer>().sprite = Sprites[1];
                transform.GetChild(0).GetComponent<Rotate>().resetRotation();
                m_HasReset = true;
                m_Animator.SetBool("m_walking", true);
            } while (!m_HasReset);

            //Grenade
            if (m_hasResetGrenade)
            {
                if ((relativePosition.x > -20 && relativePosition.x < 0.0f) && !m_flippedLeft)
                {
                    transform.GetChild(0).GetComponent<Rotate>().rotate(-grenadeTurningAmount, turningRate);
                }
                else if ((relativePosition.x < 20 && relativePosition.x > 0.0f) && m_flippedLeft)
                {
                    transform.GetChild(0).GetComponent<Rotate>().rotate(grenadeTurningAmount, turningRate);
                }
            }
            if (relativePosition.x < 10 && relativePosition.x > -10)
            {
                m_Animator.SetBool("m_walking", false);
                rb2D.velocity = Vector3.zero;
                m_HasThrownGrenade = true;

                if (transform.GetChild(0).GetComponent<Rotate>().readyForGrenade(grenadeTurningAmount) && !m_HasGrenade)
                {
                    m_HasGrenade = true;
                    Instantiate(grenadePrefab, transform.GetChild(0).GetChild(0).GetChild(0).position, transform.GetChild(0).GetChild(0).GetChild(0).rotation,null);
                    m_hasResetGrenade = false;
                }
                    

                
            }
            else
            {
                m_HasThrownGrenade = false;
            }              
            
            if (!m_hasResetGrenade)
            {
               transform.GetChild(0).GetComponent<Rotate>().rotateBack(turningRate);
                curGrenadeCooldown += Time.deltaTime;
                if (curGrenadeCooldown > grenadeCooldown)
                {
                    m_hasResetGrenade = true;
                    curGrenadeCooldown = 0.0f;
                    m_HasGrenade = false;
                }
            }
            if (!m_HasThrownGrenade)
            {
                m_Animator.SetBool("m_walking", true);
                rb2D.velocity = new Vector2(m_walkingSpeed, 0);
            }


        //Gun
            


        }//End of Phase 2(..)


        if(m_health <= 0.0f)
        {
            for (int i = 0; i < ac.animationClips.Length; i++)
            {
                if (ac.animationClips[i].name == "Death")
                {
                    animationLength = ac.animationClips[i].length;
                }

            }
            if (!m_dead)
            {
                foreach (Transform child in transform)
                {
                    GameObject.Destroy(child.gameObject);
                }
                transform.position = new Vector3(transform.position.x, transform.position.y - 1, 0);
                rb2D.velocity = new Vector3(0, 0, 0);
                m_Animator.SetBool("m_dead", true);
                m_dead = true;
            }
            Destroy(gameObject, animationLength+2);
        }//End of Death(..)

	}//End of Update(..)


    

       
   

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            
                relativePosition = transform.position - playerTransform.transform.position;
                if (relativePosition.x > 0 && !m_flippedLeft)
                {
                    transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, 0);
                    m_flippedLeft = true;
                    m_walkingSpeed *= -1;
                }
                else if (relativePosition.x < 0 && m_flippedLeft)
                {
                    transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, 0);
                    m_flippedLeft = false;
                    m_walkingSpeed *= -1;
                }
            
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "EnemyWall")
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, 0);
            m_flippedLeft = !m_flippedLeft;
            m_walkingSpeed *= -1;
        }
        if(collision.transform.tag == "Player")
        {
            Controls mover = collision.transform.GetComponent<Controls>();

            Vector2 knockBackDir = (mover.transform.position - transform.position).normalized;
            mover.GetKnockedBack(knockBackDir);
        }

        if(collision.transform.tag == "Grenade")
        {
            Physics2D.IgnoreCollision(collision.transform.GetComponent<CircleCollider2D>(), GetComponent<CircleCollider2D>());
        }
    }

    public int DamageCalculator(int Damage)
    {
        m_health -= Damage;
        return m_health;
    }
    
}
