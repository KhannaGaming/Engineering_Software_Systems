using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {
    public void rotate(float amount, float length)
    {
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0,0,amount), Time.deltaTime * length);
    }

    public void rotateBack(float length)
    {
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.identity, Time.deltaTime * length * 15);
    }

    public void resetPosition(int childNumber)
    {
        transform.GetChild(childNumber).localPosition = new Vector3(-0.23f, -0.3f, 0);
    }

    public void resetRotation()
    {
        transform.rotation = Quaternion.identity;
    }

    public bool readyForGrenade(float amount)
    {
        if(transform.rotation == Quaternion.Euler(0, 0, amount) || transform.rotation == Quaternion.Euler(0, 0, -amount))
        {
            return true;
        }
        return false;
    }

    public void rotateLever()
    {
        if (transform.rotation == Quaternion.Euler(0, 0, -135.0f)) 
        {
            transform.rotation = Quaternion.Euler(0, 0, -45.0f);
        }
    }
}
