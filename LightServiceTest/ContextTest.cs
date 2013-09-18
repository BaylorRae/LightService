using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace LightServiceTest {
    [TestClass]
    public class ContextTest {

        [TestMethod]
        public void ContextIsADictionary() {
            LightService.Context context = new LightService.Context();

            Assert.IsTrue(context is Dictionary<string, dynamic>);
        }

    }
}
