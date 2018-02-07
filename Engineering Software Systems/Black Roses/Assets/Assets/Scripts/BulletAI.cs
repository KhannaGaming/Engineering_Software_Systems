using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAI : MonoBehaviour {


    Rigidbody2D rb2d;
    
    public Vector3 mousePos;
    public Vector3 relativePos;
    private float bulletSpeed = 10f;

    private float dt;
    // Use this for initialization
	void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        // dt = Time.deltaTime;
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);


         relativePos = mousePos - GameObject.Find("FirePoint").transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 normPos = relativePos.normalized;
        rb2d.velocity = normPos * bulletSpeed;

        Destroy(gameObject, 2);

	}
}
