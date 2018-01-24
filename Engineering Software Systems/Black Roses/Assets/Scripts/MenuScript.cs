using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour {

    private bool movingToSettings = false;
    public int cameraSpeed;
    private float dt;
	// Use this for initialization
	void Start () {
        GameObject.Find("Music").GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("Volume", 1.0f);
        dt = Time.deltaTime;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey("escape"))
        {
            Application.Quit();
        }

        if(movingToSettings == true && GameObject.Find("Moveable").GetComponent<RectTransform>().localPosition.x > -800)
        {
            GameObject.Find("Moveable").GetComponent<Transform>().localPosition -= new Vector3(cameraSpeed, 0, 0);
            
        }
        else
        {
            movingToSettings = false;
        }

        PlayerPrefs.SetFloat("Volume", GameObject.Find("VolumeSlider").GetComponent<Slider>().value);
        GameObject.Find("Music").GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("Volume", 1.0f);
    }

    public void pressedPlay()
    {
        SceneManager.LoadScene("Level01");
    }

    public void pressedSettings()
    {
        movingToSettings = true;
    }

    public void pressedQuit()
    {
        Application.Quit();
    }
}
