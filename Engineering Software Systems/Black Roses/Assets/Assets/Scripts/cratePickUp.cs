using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cratePickUp : MonoBehaviour {

    int m_health;
	// Use this for initialization
	void Start () {
        m_health = 50;
	}
	
	// Update is called once per frame
	void Update () {
        if (m_health <= 0)
        {
            GameObject.Find("AudioManager").GetComponent<AudioManangement>().spawnAudio("boxBreak");
            Destroy(gameObject);
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Bullet")
        {
            m_health -= 10;
            GameObject.Find("AudioManager").GetComponent<AudioManangement>().spawnAudio("boxCollision");

        }
    }
}
