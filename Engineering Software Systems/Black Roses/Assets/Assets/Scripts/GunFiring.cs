using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunFiring : MonoBehaviour {

    public Transform bulletPrefab;
    public Transform firePointTransform;
    private Transform elbowTransform;
    private Transform armTransform;

    private Vector3 mousePos;
    private Vector3 relativePos;
    private Vector3 relativePosToFirepoint;

    public float Cooldown;
    private float CurCooldown;
    public bool facingLeft;

    public int number;
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














        //if (!facingLeft)
        //{
        //    mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //    relativePos = mousePos -elbowTransform.position;


        //        elbowTransform.rotation = Quaternion.LookRotation(Vector3.forward, relativePos);
        //        elbowTransform.eulerAngles = new Vector3(0, 0, elbowTransform.eulerAngles.z + 90);
        //}
        //if (facingLeft)
        //{

        //    mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //    relativePos = mousePos - elbowTransform.position;


        //        elbowTransform.rotation = Quaternion.LookRotation(Vector3.forward, relativePos);
        //        elbowTransform.eulerAngles = new Vector3(0, 0, elbowTransform.eulerAngles.z + 90);
        //}


        //if (!facingLeft)
        //{
        //    if (elbowTransform.eulerAngles.z > 90 && elbowTransform.eulerAngles.z < 180)
        //    {
        //        GetComponentInParent<Controls>().flipPlayerLeft();
        //        facingLeft = true;
        //        transform.localPosition = new Vector3(-transform.localPosition.x, transform.localPosition.y, transform.localPosition.z);
        //        transform.localScale = new Vector3(-transform.localScale.x, -transform.localScale.y, transform.localScale.z);

        //    }
        //    else if (elbowTransform.eulerAngles.z > 180 && elbowTransform.eulerAngles.z < 270)
        //    {
        //        GetComponentInParent<Controls>().flipPlayerLeft();
        //        facingLeft = true;
        //        transform.localPosition = new Vector3(-transform.localPosition.x, transform.localPosition.y, transform.localPosition.z);
        //        transform.localScale = new Vector3(-transform.localScale.x, -transform.localScale.y, transform.localScale.z);

        //    }
        //   // elbowTransform.eulerAngles = new Vector3(0, 0, Mathf.Clamp(elbowTransform.eulerAngles.z, 0, 180));
        //    if(elbowTransform.eulerAngles.z >180)
        //    {
        //        elbowTransform.eulerAngles = new Vector3(0, 0, 0);
        //    }

        //}
        // else if (facingLeft)
        //{
        //    if (elbowTransform.eulerAngles.z < 90 || elbowTransform.eulerAngles.z > 270)
        //    {
        //        GetComponentInParent<Controls>().flipPlayerRight();
        //        facingLeft = false;
        //        transform.localPosition = new Vector3(-transform.localPosition.x, transform.localPosition.y, transform.localPosition.z);
        //        transform.localScale = new Vector3(-transform.localScale.x, -transform.localScale.y, transform.localScale.z);

        //    }
        //    elbowTransform.eulerAngles = new Vector3(0, 0, Mathf.Clamp(elbowTransform.eulerAngles.z, 0, 180));
        //}
    }
}
