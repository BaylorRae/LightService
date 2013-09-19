using System;
using NUnit.Framework;

namespace LightServiceTest {
    [TestFixture]
    public class OrganizerTest {

        [Test]
        public void WithCreatesADefaultContext() {
            Assert.IsTrue(LightService.Organizer.With().context is LightService.Context, "Organizer default context must be an instance of LightService.Context");
        }

        [Test]
        public void ItUsesTheContextPassedIntoWith() {
            LightService.Context context = new LightService.Context();
            LightService.Organizer org = LightService.Organizer.With(context);
            Assert.AreSame(context, org.context);
        }

        [Test]
        public void ItCallsAnAction() {
            ExampleAction action = new ExampleAction();

            LightService.IAction[] actions = new LightService.IAction[] { action };
            ExampleOrganizer.DoSomething(actions);

            Assert.AreEqual("called!!!", action.Status);
        }

        [Test]
        public void ItCallsMultipleActions() {
            ExampleAction action = new ExampleAction();
            ExampleAction action2 = new ExampleAction();

            LightService.IAction[] actions = new LightService.IAction[] { action, action2 };
            ExampleOrganizer.DoSomething(actions);

            Assert.AreEqual("called!!!", action.Status);
            Assert.AreEqual("called!!!", action2.Status, "LightService.Organizer Reduce only called a single action");
        }

    }

    public class ExampleAction : LightService.IAction {
        public string Status { get; set; }

        public ExampleAction() {
            this.Status = "hidy ho neighbor";
        }

        public void Executed() {
            this.Status = "called!!!";
        }
    }

    public interface IExampleAction : LightService.IAction {
        new void Executed();
    }

    internal class ExampleOrganizer : LightService.Organizer {

        public static void DoSomething(LightService.IAction[] actions) {
            With().Reduce(actions);
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
