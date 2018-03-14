using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeAI : MonoBehaviour {

    private Rigidbody2D rb2D;
    public Transform explosionPrefab;
    private bool m_Thrown;
    private float CurCooldown = 0.0f;
    private float Cooldown = 1.5f;

    private void Start()
    {
        m_Thrown = false;
        rb2D = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update () {
		if(!transform.parent && !m_Thrown)
        {
            Vector3 relativePos = GameObject.Find("Hitler").transform.position - GameObject.Find("Player").transform.position;
            if (relativePos.x > 0)
            {
                rb2D.AddForce(new Vector3(1, -1, 0) * -150);
            }

            if (relativePos.x < 0)
            {
                rb2D.AddForce(new Vector3(-1, -1, 0) * -150);
            }
            m_Thrown = true;
            
        }
        if(m_Thrown)
        {
            CurCooldown += Time.deltaTime;
            if (CurCooldown > Cooldown)
            {
                Instantiate(explosionPrefab,transform.position,Quaternion.identity,null);
                Destroy(gameObject);
            }
        }
        
	}
}
