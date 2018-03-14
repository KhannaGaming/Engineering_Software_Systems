using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunFiring : MonoBehaviour {
    //----------------------------------------------------------------------------
    //BOOLS 
    public bool facingLeft;
    private bool reloading;

    //----------------------------------------------------------------------------
    //FLOATS 
    public float Cooldown;
    private float CurCooldown;
    public float reloadCooldown;
    private float reloadCurCooldown;

    //----------------------------------------------------------------------------
    //INTS
    private int curBullets = 20;
    private int maxBullets = 20;


    //----------------------------------------------------------------------------
    //OTHER 
    public Transform bulletPrefab;
    public Transform firePointTransform;
    public Vector3 elbowangles;    
    private Transform elbowTransform;
    private Transform armTransform;
    private Vector3 mousePos;
    private Vector3 relativePos;
    private Vector3 relativePosToFirepoint;

	// Use this for initialization
	void Start () {
        facingLeft = false;
        reloading = false;
        elbowTransform = GameObject.Find("Elbow").transform;
        armTransform = GameObject.Find("Arm").transform;
        relativePosToFirepoint = firePointTransform.position - elbowTransform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameObject.Find("Pause"))
        {
            elbowangles = elbowTransform.eulerAngles;
            CurCooldown += Time.deltaTime;

            if (CurCooldown > Cooldown)
            {

                if (Input.GetMouseButton(0) && curBullets > 0)
                {
                    Instantiate(bulletPrefab, firePointTransform.position, firePointTransform.rotation);
                    GameObject.Find("AudioManager").GetComponent<AudioManangement>().spawnAudio("bulletSound");
                    CurCooldown = 0;
                    curBullets -= 1;
                    reloadCurCooldown = 0;
                    reloading = false;
                    if (GameObject.Find("AudioManager").GetComponent<AudioManangement>().isPlaying("Reload"))
                    {
                        GameObject.Find("AudioManager").GetComponent<AudioManangement>().stopAudio("Reload");
                    }
                }
                else if ((curBullets == 0 || (Input.GetKeyDown("r") && curBullets != maxBullets)) && !reloading)
                {
                    GameObject.Find("AudioManager").GetComponent<AudioManangement>().spawnAudio("reload");
                    reloading = true;
                }

                if (reloading)
                {
                    reloadCurCooldown += Time.deltaTime;

                    if (reloadCurCooldown > reloadCooldown)
                    {
                        curBullets = maxBullets;
                        reloadCurCooldown = 0;
                        reloading = false;
                    }
                }
            }

            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos += new Vector3(0.3f, -0.35f, 0);
            mousePos.z = 0.0f;
            Debug.DrawLine(elbowTransform.position, mousePos, Color.green);
            relativePos = mousePos - elbowTransform.position;
            Debug.DrawLine(elbowTransform.position, firePointTransform.position, Color.red);

            var angle = Vector3.Angle(relativePos, relativePosToFirepoint);

            if (relativePos.y > 0)
            {
                armTransform.eulerAngles = new Vector3(0, 0, angle);
            }
            else
            {
                armTransform.eulerAngles = new Vector3(0, 0, -angle);
            }

            if (relativePos.x < 0)
            {
                GetComponentInParent<Controls>().flipPlayerLeft();
            }
            else if (relativePos.x > 0)
            {
                GetComponentInParent<Controls>().flipPlayerRight();
            }

            GameObject.Find("Ammo").GetComponent<Text>().text = curBullets + "/" + maxBullets;
        }
    }//End of Update(..)
}
