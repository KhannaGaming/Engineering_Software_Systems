using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        PlayerPrefs.SetInt("Volume", 10);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }

    public void pressedPlay()
    {
        SceneManager.LoadScene("Level01");
    }

    public void pressedSettings()
    {

    }

    public void pressedQuit()
    {
        Application.Quit();
    }

}
