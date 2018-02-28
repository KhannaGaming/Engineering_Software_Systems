using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunFiring : MonoBehaviour {
    //----------------------------------------------------------------------------
    //BOOLS 
    public bool facingLeft;

    //----------------------------------------------------------------------------
    //FLOATS 
    public float Cooldown;
    private float CurCooldown;

    //----------------------------------------------------------------------------
    //OTHER 
    public Transform bulletPrefab;
    public Transform firePointTransform;
    private Transform elbowTransform;
    private Transform armTransform;
    private Vector3 mousePos;
    private Vector3 relativePos;
    private Vector3 relativePosToFirepoint;
    public Vector3 elbowangles;    

	// Use this for initialization
	void Start () {
        facingLeft = false;
        elbowTransform = GameObject.Find("Elbow").transform;
        armTransform = GameObject.Find("Arm").transform;
        relativePosToFirepoint = firePointTransform.position - elbowTransform.position;
    }
	
	// Update is called once per frame
	void Update () {

        elbowangles = elbowTransform.eulerAngles;
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

        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos += new Vector3(0.3f, -0.35f,0);
       //Debug.DrawLine(elbowTransform.position, mousePos, Color.green);
        relativePos = mousePos - elbowTransform.position;
        //Debug.DrawLine(elbowTransform.position, firePointTransform.position, Color.red);

        var angle = Vector3.Angle(relativePos,relativePosToFirepoint);

        if (relativePos.y > 0)
        {
            armTransform.eulerAngles = new Vector3(0, 0, angle);
        }
        else
        {
            armTransform.eulerAngles = new Vector3(0, 0, -angle);
        }

        if (relativePos.x<0 )
        {
            GetComponentInParent<Controls>().flipPlayerLeft();
        }
        else if(relativePos.x > 0)
        {
            GetComponentInParent<Controls>().flipPlayerRight();
        }
    }
}
