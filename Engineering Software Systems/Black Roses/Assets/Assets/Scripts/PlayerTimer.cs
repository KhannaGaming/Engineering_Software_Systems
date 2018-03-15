using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTimer : MonoBehaviour
{

    private float currentSeconds;
    private int currentMinutes;
    // Use this for initialization
    void Start()
    {
        currentSeconds = PlayerPrefs.GetFloat("CurrentSeconds", 0);
        currentMinutes = PlayerPrefs.GetInt("CurrentMinutes", 0);
    }

    // Update is called once per frame
    void Update()
    {
        currentSeconds += Time.deltaTime;
        if (currentSeconds >= 60.0f)
        {
            currentSeconds = 0;
            currentMinutes += 1;
        }

        GameObject.Find("Timer").GetComponent<Text>().text = currentMinutes + "m:" + ((int)currentSeconds) + "s";
    }

    public void saveTime()
    {
        PlayerPrefs.SetFloat("CurrentSeconds", currentSeconds);
        PlayerPrefs.SetInt("CurrentMinutes", currentMinutes);
    }

    public void resetTime()
    {
        PlayerPrefs.SetFloat("CurrentSeconds", 0.0f);
        PlayerPrefs.SetInt("CurrentMinutes", 0);
    }


    public void setHighScore()
    {
        PlayerPrefs.SetFloat("HighscoreSeconds", currentSeconds);
        PlayerPrefs.SetInt("HighscoreMinutes", currentMinutes);
    }

    public string getHighscoreAsString()
    {
        string highscore = PlayerPrefs.GetInt("HighscoreMinutes", 0).ToString() + "m:" + ((int)PlayerPrefs.GetFloat("HighscoreSeconds", 0)).ToString() + "s";
        return highscore;
    }

    public float getHighScoreAsFloat()
    {
        float highscore =  (PlayerPrefs.GetInt("HighscoreMinutes", 0)*60)+ PlayerPrefs.GetFloat("HighscoreSeconds", 0);
        return highscore;
    }
}
