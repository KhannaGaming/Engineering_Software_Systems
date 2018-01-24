using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Controls : MonoBehaviour {

    Animator m_Animator;
    private bool m_running;
    private bool m_jump;
    private bool m_flipped;
    private float dt;
    public int speed;
    public int jumpSpeed;
    private Rigidbody2D rb2D;

    // Use this for initialization
    void Start () {
        m_Animator = gameObject.GetComponent<Animator>();
        m_running = false;
        m_jump = true;
        m_flipped = false;
        dt = Time.deltaTime;
        rb2D = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey("escape"))
        {
            SceneManager.LoadScene("Main_Menu");
        }

            if (Input.GetKey("d"))
        {
            m_running = true;
            transform.position += new Vector3(speed * dt, 0, 0);
            StopAllCoroutines();
            StartCoroutine(FlipRight());
        }
        if (Input.GetKeyUp("d"))
        {
            m_running = false;
        }
        if (Input.GetKey("a"))
        {
            m_running = true;
            transform.position -= new Vector3(speed * dt, 0, 0);
            StopAllCoroutines();
            StartCoroutine(FlipLeft());
        }
        if (Input.GetKeyUp("a"))
        {
            m_running = false;
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
            rb2D.velocity = new Vector2 (0,jumpSpeed);
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
            rb2D.velocity = new Vector2(0, 0);
            m_jump = false;
        }
    }
}
