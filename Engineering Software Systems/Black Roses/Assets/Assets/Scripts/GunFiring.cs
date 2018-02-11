using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunFiring : MonoBehaviour {

    public Transform bulletPrefab;
    public Transform firePointTransform;
    private Transform elbowTransform;

    public Vector3 mousePos;
    public Vector3 relativePos;

    public float Cooldown;
    private float CurCooldown;
    

	// Use this for initialization
	void Start () {

        elbowTransform = GameObject.Find("Elbow").transform;

    }
	
	// Update is called once per frame
	void Update () {

        CurCooldown += Time.deltaTime;
        if (CurCooldown > Cooldown)
        {
            if (Input.GetMouseButton(0))
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
        Quaternion tempQuat = Quaternion.LookRotation(Vector3.forward, relativePos);
     
        elbowTransform.rotation = Quaternion.LookRotation(Vector3.forward, relativePos);
        elbowTransform.eulerAngles = new Vector3(0,0, elbowTransform.eulerAngles.z+90);

    }
}
