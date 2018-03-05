using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour {
    //----------------------------------------------------------------------------
    //BOOLS 
    private bool movingToSettings = false;
    private bool movingToMenu = false;
  

    //----------------------------------------------------------------------------
    //INTS 
    public int cameraSpeed;

    //----------------------------------------------------------------------------
    //FLOATS 
    private float duration = 3.0f;

    //----------------------------------------------------------------------------
    //OTHER 
    Color color1;
    Color color2;

    // Use this for initialization
    void Start () {
        GameObject.Find("Music").GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("Volume", 1.0f);
        float val1 = 26.0f / 255.0f;
        float val2 = 89.0f / 255.0f;
        color1 = new Color(val1, 0, 0, 1);
        color2 = new Color(val2, 0, 0, 1);
    }
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey("escape"))
        {
            Application.Quit();
        }
        if (movingToSettings == true && GameObject.Find("Moveable").GetComponent<RectTransform>().localPosition.x > -800)
        {
            GameObject.Find("Moveable").GetComponent<Transform>().localPosition -= new Vector3(cameraSpeed, 0, 0);
            
        }
        else
        {
            movingToSettings = false;
        }

        if (movingToMenu == true && GameObject.Find("Moveable").GetComponent<RectTransform>().localPosition.x < 0)
        {
            GameObject.Find("Moveable").GetComponent<Transform>().localPosition += new Vector3(cameraSpeed, 0, 0);

        }
        else
        {
            movingToMenu = false;
        }

        PlayerPrefs.SetFloat("Volume", GameObject.Find("VolumeSlider").GetComponent<Slider>().value);
        GameObject.Find("Music").GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("Volume", 1.0f);


        float t = Mathf.PingPong(Time.time, duration) / duration;
            GetComponent<Camera>().backgroundColor = Color.Lerp(color1, color2, t);
        
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

    public void pressedBack()
    {
        movingToMenu = true;
    }
}
