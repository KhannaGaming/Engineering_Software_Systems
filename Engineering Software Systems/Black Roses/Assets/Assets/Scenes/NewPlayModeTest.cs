using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class NewPlayModeTest {

	//[Test]
	//public void NewPlayModeTestSimplePasses() {
	//	// Use the Assert class to test conditions.
	//}

	//// A UnityTest behaves like a coroutine in PlayMode
	//// and allows you to yield null to skip a frame in EditMode
	//[Test]
    //public IEnumerator TestAnimationAnimUtilityPrefab()
    //{
    //    RenderSettings.skybox = null;
    //    GameObject root = new GameObject();
    //    root.AddComponent(typeof(Camera));
    //    var camera = root.GetComponent<Camera>();
    //    camera.backgroundColor = Color.white;
    //    root = GameObject.Instantiate(root);
    //    var prefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/player.prefab");
    //    prefab = GameObject.Instantiate(prefab, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));


    //    yield return new WaitForSeconds(3.0f);
    //    var script = prefab.gameObject.GetComponentInChildren<Controls>();
    //    Assert.IsTrue(script != null, "Controls must be set on player.prefab.");

    //    GameObject.Destroy(prefab);
    //    GameObject.Destroy(root);
    //}
}
