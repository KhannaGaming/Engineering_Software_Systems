using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour {

    //----------------------------------------------------------------------------
    //FLOATS 
    private float duration = 3.0f;
    private float Cooldown = 3.0f;
    private float CurCooldown;

    //----------------------------------------------------------------------------
    //BOOL
    private bool loadedButtons= false;

    //------------------------------------------
    //OTHER
    public Transform canvas;
    public Text GameOverText;
    private Color Background;
    private Color color1;
    private Color color2;

    // Use this for initialization
    void Start () {
        float val1 = 26.0f / 255.0f;
        float val2 = 89.0f / 255.0f;
        color1 = new Color(val1, 0, 0, 1);
        color2 = new Color(val2, 0, 0, 1);
        CurCooldown = 0;
    }
	
	// Update is called once per frame
	void Update () {

        float t = Mathf.PingPong(Time.time, duration) / duration;
        GameObject.Find("Background").GetComponent<Image>().color = Color.Lerp(color1, color2, t);
        GameOverText.color = new Color(GameOverText.color.r, GameOverText.color.g, GameOverText.color.b, Mathf.PingPong(Time.time, 1));

        CurCooldown += Time.deltaTime;
        if(CurCooldown > Cooldown && !loadedButtons)
        {

            if (canvas.gameObject.activeInHierarchy == false)
            {
                canvas.gameObject.SetActive(true);
            }

            loadedButtons = true;
        }


    }

    public void pressedRetry()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("CurrentScene"));
    }
    public void pressedExit()
    {
        SceneManager.LoadScene("Main_Menu");
    }

    public void pressedStartAgain()
    {
        SceneManager.LoadScene("Level01");
    }
}
