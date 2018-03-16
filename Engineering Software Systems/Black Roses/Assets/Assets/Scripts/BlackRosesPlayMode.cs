using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class BlackRosesPlayMode {



    // A UnityTest behaves like a coroutine in PlayMode
    // and allows you to yield null to skip a frame in EditMode
    [UnityTest]
    public IEnumerator Rotate_readyForGrenade_IsTrue() {
        // Use the Assert class to test conditions.
        // yield to skip a frame


        //Arrange
        GameObject Arm = new GameObject();
        Arm.transform.rotation = Quaternion.Euler(0, 0, 144.0f);
        Arm.AddComponent<Rotate>();

        //Act
        var result = Arm.GetComponent<Rotate>().readyForGrenade(144.0f);
        yield return null;

        //Assert
        Assert.IsTrue(result);
    }

    [UnityTest]
    public IEnumerator PauseGame_Pause_AreEqual()
    {
        // Use the Assert class to test conditions.
        // yield to skip a frame


        //Arrange
        GameObject Canvas = new GameObject();
        Canvas.AddComponent<Canvas>();
        GameObject PauseMenu = new GameObject();
        GameObject Menu = new GameObject();
        Menu.transform.SetParent(Canvas.transform); 
        Menu.SetActive(false);
        PauseMenu.AddComponent<PauseGame>();
        PauseMenu.GetComponent<PauseGame>().canvas = Menu.transform;
        PauseMenu.GetComponent<PauseGame>().Pause();

        //Act
        var result = Time.timeScale;
        yield return null;

        //Assert
        Assert.AreEqual(0.0f, result);
    }

    [UnityTest]
    public IEnumerator explosionScript_Update_IsFalse()
    {
        //Arrange
        GameObject explosion = new GameObject();
        explosion.AddComponent<explosionScript>();
        explosion.GetComponent<explosionScript>().GO = explosion;
        //Act
        var result = explosion;
        yield return new WaitForSeconds(3);

        //Arrange
        Assert.IsFalse(result);
    }
}
