using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace BlackRoses.Tests
{
    [TestClass()]
    public class JetPackControllerTests
    {
        [TestMethod()]
        public void testTest()
        {
            //Arrange
            var JetPack = new JetPackController();

            //Act
            var result = JetPack.test(2);

            //Assert
            Assert.IsTrue(result);
        }
    }
}