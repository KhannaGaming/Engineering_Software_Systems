using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunFiring : MonoBehaviour {

    public Transform bulletPrefab;
    public Transform firePointTransform;

    public Vector3 mousePos;
    public Vector3 relativePos;

    public float Cooldown;
    private float CurCooldown;
    

	// Use this for initialization
	void Start () {
       
	}
	
	// Update is called once per frame
	void Update () {

        CurCooldown += Time.deltaTime;
        if (CurCooldown > Cooldown)
        {
            if (Input.GetButton("Fire1"))
            {
                Instantiate(bulletPrefab, firePointTransform.position, firePointTransform.rotation);
                GameObject.Find("AudioManager").GetComponent<AudioManangement>().spawnAudio("bulletSound");
                CurCooldown = 0;
            }
        }
	}

    private void FixedUpdate()
    {

        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

         relativePos =  mousePos - GameObject.Find("Elbow").transform.position ;
         GameObject.Find("Elbow").transform.rotation = Quaternion.LookRotation(Vector3.forward, relativePos);
        GameObject.Find("Elbow").transform.eulerAngles = new Vector3(0,0, GameObject.Find("Elbow").transform.eulerAngles.z+90);
    }
}
