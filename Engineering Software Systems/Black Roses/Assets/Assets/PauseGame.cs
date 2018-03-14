using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseGame : MonoBehaviour {

    public Transform canvas;

    // Update is called once per frame
    void Update()
    {
        float a = Time.timeScale;
        Debug.Log(a);
        if (Input.GetKeyDown("escape"))
        {
            Pause();
        }

    }
    public void Pause()
    {
        if (canvas.gameObject.activeInHierarchy == false)
        {
            canvas.gameObject.SetActive(true);
            Time.timeScale = 0.0f;
        }
        else
        {
            canvas.gameObject.SetActive(false);
            Time.timeScale = 1.0f;
        }

    }

    public void pressedResume()
    {
        Pause();
    }

    public void pressedMainMenu()
    {
        SceneManager.LoadScene("Main_Menu");
    }
}
