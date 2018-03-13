using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class Blackrosestest {

	[UnityTest]
    public IEnumerator MonoBehaviourTest_Works()
    {
        yield return new MonoBehaviourTest<MyMonoBehaviourTest>();
    }

    public class MyMonoBehaviourTest:MonoBehaviour, IMonoBehaviourTest
    {
        private int frameCount;
        public bool IsTestFinished
        {
            get { return frameCount > 10; }
        }
        private void Update()
        {

            frameCount++;
       
            
        }
    }


	//public void JetPackControls_Test_IsTrue() {
 //       // Use the Assert class to test conditions.
 //       //Arrange
 //       var jetpack = new JetPackController();
 //       var result = jetpack.test(1);
 //       Assert.IsTrue(result);
 //   }

	// A UnityTest behaves like a coroutine in PlayMode
	// and allows you to yield null to skip a frame in EditMode
	
}
