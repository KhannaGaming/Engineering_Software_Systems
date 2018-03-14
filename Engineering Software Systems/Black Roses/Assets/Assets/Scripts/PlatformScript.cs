using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformScript : MonoBehaviour
{
    //------------------------------------------------
    //FLOATS
    public float Cooldown;
    private float CurCooldown;
    //------------------------------------------------
    //BOOLS
    private bool m_OnPlatform;

    //------------------------------------------------
    //OTHER
    private Color Red;
    private Color Normal;
    
    private Vector3 StartPosition;
    private Vector3 EndPosition;

    // Use this for initialization
    void Start()
    {
        StartPosition = transform.position;
        EndPosition = new Vector3(StartPosition.x, StartPosition.y -100.0f, 0.0f);
        m_OnPlatform = false;
        Red = new Color(1.0f, 0.0f, 0.0f, 1.0f);
        Normal = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (m_OnPlatform)
        {
            transform.GetComponent<SpriteRenderer>().color -= new Color(0, Time.deltaTime / 2, Time.deltaTime / 2, 0);
        }

        if (transform.GetComponent<SpriteRenderer>().color.g < 0)
        {
            transform.GetComponent<SpriteRenderer>().color =  Red;
        }

        if (transform.GetComponent<SpriteRenderer>().color == Red && transform.position.y > EndPosition.y)
        {
            transform.position -= new Vector3(0,10.0f*Time.deltaTime,0);
        }
        else if(transform.position.y <= EndPosition.y)
        {
            CurCooldown += Time.deltaTime;
            if (CurCooldown > Cooldown)
            {
                transform.GetComponent<SpriteRenderer>().color = Normal;
                transform.position = StartPosition;
                CurCooldown = 0.0f;
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            m_OnPlatform = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            m_OnPlatform = false;
        }
    }
}
