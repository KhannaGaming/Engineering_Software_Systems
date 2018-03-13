using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitlerControls : MonoBehaviour {

    //----------------------------------------------------
    //FLOATS
    public float chargeCooldown;
    private float curChargeCooldown;
    private float m_health;
    private float m_maxHealth;
    private float animationLength;

    //----------------------------------------------------------------------------
    //INTS     
    private int m_walkingSpeed;

    //----------------------------------------------------------------------------
    //BOOLS
    private bool m_flippedLeft;
    private bool m_IsCharging;

    //----------------------------------------------------------------------------
    //Other
    private Animator m_Animator;
    private RuntimeAnimatorController ac;
    private Rigidbody2D rb2D;
    private Vector3 relativePosition;
    private GameObject playerTransform;

    // Use this for initialization
    void Start () {
        m_health = 1000.0f;
        m_walkingSpeed = 2;
        m_maxHealth = m_health;
        m_flippedLeft = false;
        m_IsCharging = false;
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
            if (!m_IsCharging)
            {
                rb2D.velocity = new Vector2(m_walkingSpeed, 0);
            }
            m_Animator.SetBool("m_walking", true);

            curChargeCooldown += Time.deltaTime;
            if (relativePosition.x < 5)
            {
                if (curChargeCooldown > chargeCooldown)
                {

                    m_IsCharging = true;
                    rb2D.velocity = new Vector2(m_walkingSpeed * 10, 0);
                    curChargeCooldown = 0;
                }

            }

            if(relativePosition.x <1)
            {
                m_IsCharging = false;
            }
        }//End of Phase 1

        if(m_health <= 0.0f)
        {
            for (int i = 0; i < ac.animationClips.Length; i++)
            {
                if (ac.animationClips[i].name == "Death")
                {
                    animationLength = ac.animationClips[i].length;
                }

            }

            Destroy(gameObject, animationLength);
        }

	}

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
    }
}
