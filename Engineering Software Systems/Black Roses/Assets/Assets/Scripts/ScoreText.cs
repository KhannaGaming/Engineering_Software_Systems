using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour {

    private void Update()
    {
       gameObject.GetComponent<Text>().text = GameObject.Find("Timer").GetComponent<PlayerTimer>().getHighscoreAsString();
    }
}
