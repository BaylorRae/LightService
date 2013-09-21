using System;
using NUnit.Framework;

namespace LightServiceTest {
    [TestFixture]
    class CustomContextTest {

        [Test]
        public void ActionsCanUseAnythingAsAContext() {
            CustomContext context = new CustomContext();
            ExampleAction action = new ExampleAction();

            CustomContext newContext = action.Executed(context);
            Assert.AreEqual("new message", newContext.Message);
        }

        internal class ExampleAction : LightService.IAction<CustomContext> {
            public CustomContext Executed(CustomContext context) {
                context.Message = "new message";
                return context;
            }
        }

        internal class CustomContext : LightService.Context {
            public string Message { get; set; }

            public CustomContext() {
                this.Message = "some message";
            }
        }
    }
}
