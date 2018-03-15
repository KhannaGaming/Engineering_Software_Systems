using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverAI : MonoBehaviour {

    //--------------------------------------------------
    //BOOL
    private bool m_LeverUp;

    //--------------------------------------------------
    //OTHER
    public GameObject targetObject;
    public Sprite elevatorWorkingSprite;
    public GameObject DoorToDisable1;
    public GameObject DoorToDisable2;

    private void Start()
    {
        m_LeverUp = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!m_LeverUp && Input.GetKey("e"))
        {
            if (collision.transform.tag == "Player")
            {
                transform.GetChild(0).GetComponent<Rotate>().rotateLever();
                targetObject.GetComponent<SpriteRenderer>().sprite = elevatorWorkingSprite;
                collision.transform.GetComponent<Controls>().m_ElevatorOpen = true;
                DoorToDisable1.SetActive(false);
                DoorToDisable2.SetActive(false);
                m_LeverUp = true;
            }
        }
    }
}
