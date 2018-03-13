using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    //----------------------------------------------------------------------------
    //BOOLS 
    private bool movingToSettings = false;
    private bool movingToMenu = false;


    //----------------------------------------------------------------------------
    //INTS 
    public int cameraSpeed;
    public int scene;

    //----------------------------------------------------------------------------
    //FLOATS 
    private float duration = 3.0f;

    //----------------------------------------------------------------------------
    //OTHER 
    public Text loadingText;
    private Color color1;
    private Color color2;
    private bool loadScene = false;


    // Use this for initialization
    void Start()
    {
        GameObject.Find("Music").GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("Volume", 1.0f);
        float val1 = 26.0f / 255.0f;
        float val2 = 89.0f / 255.0f;
        color1 = new Color(val1, 0, 0, 1);
        color2 = new Color(val2, 0, 0, 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("escape"))
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

        // If the new scene has started loading...
        if (loadScene == true)
        {
            // ...then pulse the transparency of the loading text to let the player know that the computer is still working.
            loadingText.color = new Color(loadingText.color.r, loadingText.color.g, loadingText.color.b, Mathf.PingPong(Time.time, 1));
        }
    }

    public void pressedPlay()
    {
        GameObject.Find("AudioManager").GetComponent<AudioManangement>().spawnAudio("click");
        if (loadScene == false)
        {
            GameObject.Find("MainMenu").SetActive(false);
            GameObject.Find("LoadingText").SetActive(true);
            // ...set the loadScene boolean to true to prevent loading a new scene more than once...
            loadScene = true;

            // ...change the instruction text to read "Loading..."
            loadingText.text = "Loading...";

            // ...and start a coroutine that will load the desired scene.
            StartCoroutine(LoadNewScene());
        }
    }

    public void pressedSettings()
    {
        GameObject.Find("AudioManager").GetComponent<AudioManangement>().spawnAudio("click");
        movingToSettings = true;
    }

    public void pressedQuit()
    {
        GameObject.Find("AudioManager").GetComponent<AudioManangement>().spawnAudio("click");
        Application.Quit();
    }

    public void pressedBack()
    {
        GameObject.Find("AudioManager").GetComponent<AudioManangement>().spawnAudio("click");
        movingToMenu = true;
    }

    // The coroutine runs on its own at the same time as Update() and takes an integer indicating which scene to load.
    IEnumerator LoadNewScene()
    {

        // This line waits for 3 seconds before executing the next line in the coroutine.
        // This line is only necessary for this demo. The scenes are so simple that they load too fast to read the "Loading..." text.
        yield return new WaitForSeconds(3);

        // Start an asynchronous operation to load the scene that was passed to the LoadNewScene coroutine.
        AsyncOperation async = SceneManager.LoadSceneAsync(scene);

        // While the asynchronous operation to load the new scene is not yet complete, continue waiting until it's done.
        while (!async.isDone)
        {
            yield return null;
        }

    }
}

