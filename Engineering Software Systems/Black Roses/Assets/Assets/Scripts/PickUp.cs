using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour {

    public enum PickUps
    {
        Fuel, HealthPacks
    };

    public PickUps current;


    //------------------------------------------------------------
    //FLOATS
    public float minimum = -1.0F;
    public float maximum = 1.0F;
    static float t = 0.0f;

    private void Update()
    {
          if(current.ToString() == "HealthPacks")
        {
            t += 0.1f * Time.deltaTime;
            transform.localScale = new Vector3(Mathf.Lerp(minimum, maximum, t), transform.localScale.y, transform.localScale.z);

            if (t > 1.0f)
            {
                float temp = maximum;
                maximum = minimum;
                minimum = temp;
                t = 0.0f;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
            if (collision.transform.tag == "Player")
            {
            if (current.ToString() == "Fuel")
            {
                collision.transform.GetComponent<Controls>().m_HasFuel = true;
            }
            else if(current.ToString() == "HealthPacks")
            {
                GameObject.Find("Player").GetComponent<Controls>().m_health = 100;
            }
                Destroy(gameObject);
            }
    }
}
