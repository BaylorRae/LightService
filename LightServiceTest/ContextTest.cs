using System;
using NUnit.Framework;
using System.Collections.Generic;

namespace LightServiceTest {
    [TestFixture]
    public class ContextTest {

        [Test]
        public void ContextIsADictionary() {
            LightService.Context context = new LightService.Context();

            Assert.IsTrue(context is Dictionary<string, dynamic>);
        }

    }
}
