using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LightServiceTest {
    [TestClass]
    public class OrganizerTest {

        [TestMethod]
        public void WithCreatesADefaultContext() {
            Assert.IsTrue(LightService.Organizer.With().context is LightService.Context, "Organizer default context wasn't an instance of LightService.Context");
        }

    }

    //internal class ExampleOrganizer : LightService.Organizer {

    //    public static bool SomeMethod() {
    //        return true;
    //    }

    //    public static bool Add1and1() {
    //        LightService.Action[] actions = new LightService.Action[] {
    //            new ExampleAction()
    //        };

    //        return With(new { number = 1 }).Reduce(actions);
    //    }

    //}

    //internal class ExampleAction : LightService.Action {

    //    public LightService.Context executed(LightService.Context context) {
    //        context.result = context.number + context.number;
    //        return context;
    //    }
    //}
}
