using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Controls : MonoBehaviour {

    Animator m_Animator;
    private bool m_running;
    private bool m_jump;
    private bool m_flipped;
    public int speed;
    public int jumpSpeed;
    private Rigidbody2D rb2D;
    public Vector2 velocity;

    // Use this for initialization
    void Start () {
        m_Animator = gameObject.GetComponent<Animator>();
        m_running = false;
        m_jump = true;
        m_flipped = false;
        rb2D = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        velocity = rb2D.velocity;
        if (Input.GetKey("escape"))
        {
            SceneManager.LoadScene("Main_Menu");
        }

        

        if(Mathf.Abs(Input.GetAxis("Horizontal")) > 0)
        {
                rb2D.velocity = new Vector2(Input.GetAxis("Horizontal")*speed, rb2D.velocity.y);
                m_running = true;
            if(rb2D.velocity.x < 0)
            {
                    StopAllCoroutines();
                    StartCoroutine(FlipLeft());
                GameObject.Find("AudioManager").GetComponent<AudioManangement>().spawnAudio("bells8");
            }
            if (rb2D.velocity.x > 0)
            {
                StopAllCoroutines();
                StartCoroutine(FlipRight());
            }
        }
        else
        {
            m_running = false;
            rb2D.velocity = new Vector2(0, rb2D.velocity.y);
        }
      
             

        if (m_running == false)
        {
            m_Animator.SetBool("Running", false);
        }

        if (m_running == true)
        {
            m_Animator.SetBool("Running", true);
        }

        if (Input.GetKeyDown("space") && m_jump == false)
        {
            m_jump = true;
            rb2D.velocity = new Vector2(rb2D.velocity.x,jumpSpeed);
            GameObject.Find("AudioManager").GetComponent<AudioManangement>().spawnAudio("waterSound");
        }

        if (m_jump == false)
        {
            m_Animator.SetBool("Jump", false);
        }

        if (m_jump == true)
        {
            m_Animator.SetBool("Jump", true);
        }
    }

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Ground")
        {
            m_jump = false;
        }
        if (collision.transform.name == "KillZone")
        {
            ResetPosition();
        }
    }

    private void ResetPosition()
    {
        transform.position = new Vector3(-4.34f,-1.49f,0.0f);
    }
}
