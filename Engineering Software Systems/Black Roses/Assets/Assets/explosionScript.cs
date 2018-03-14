using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosionScript : MonoBehaviour {

    public GameObject GO;

    private void Update()
    {
        Destroy(GO, 1.333f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            Controls mover = collision.transform.GetComponent<Controls>();

            Vector2 knockBackDir = (mover.transform.position - transform.position).normalized;
            mover.GetKnockedBack(knockBackDir);
            collision.transform.GetComponent<Controls>().damageCalculator(15);
        }
    }
}
