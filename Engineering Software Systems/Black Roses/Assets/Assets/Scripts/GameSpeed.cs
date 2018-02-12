using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSpeed : MonoBehaviour {

    public float m_gameSpeed = 1.0f; 

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void slowTime()
    {
        m_gameSpeed = 0.5f;
    }
    public void resetGameSpeed()
    {
        m_gameSpeed = 1.0f;
    }
}
