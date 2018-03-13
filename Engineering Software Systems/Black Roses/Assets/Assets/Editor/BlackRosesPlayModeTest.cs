using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class BlackRosesPlayModeTest {

   public class ControlsTestss
    {
        [UnityTest]
        //public IEnumerator _Collision_Enemy_and_Bullet()
        //{

        //    var enemyPrefab = Resources.Load("Tests/enemy");
        //    var enemySpawner = new GameObject().AddComponent<EnemyController>();
        //    enemySpawner.Construct(100);
            
        //    //Assert
        //    Assert.AreEqual(enemySpawner, enemyPrefab);
        //}
        [TearDown]
        public void AfterEveryTest()
        {

        }

    }
}
