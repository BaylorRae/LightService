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

        [Test]
        public void ReduceReturnsTheOrganizersContext() {
            LightService.Context context = new LightService.Context();

            LightService.Organizer org = LightService.Organizer.With(context);
            LightService.Context newContext = org.Reduce(new LightService.IAction[] { new ExampleAction() });

            Assert.AreSame(org.context, newContext);
        }

        [Test]
        public void ReduceUpdatesContextWhenAnActionIsCalled() {
            // create a custom context
            LightService.Context context = new LightService.Context();
            context.Add("message", "default");
            
            LightService.Context newContext = MessageUpdatingOrganizer.UpdateContext(context);

            // calling the action should update the organizer's context
            string message = newContext["message"];
            Assert.AreEqual("updated context message", message);
        }

    }

    internal class ExampleAction : LightService.IAction {
        public string Status { get; set; }

        public ExampleAction() {
            this.Status = "hidy ho neighbor";
        }

        public LightService.Context Executed(LightService.Context context) {
            this.Status = "called!!!";
            return context;
        }
    }

    internal class ExampleOrganizer : LightService.Organizer {

        public static void DoSomething(LightService.IAction[] actions) {
            With().Reduce(actions);
        }

    }

    internal class MessageUpdatingAction : LightService.IAction {
        public LightService.Context Executed(LightService.Context context) {
            context["message"] = "updated context message";
            return context;
        }
    }

    internal class MessageUpdatingOrganizer : LightService.Organizer {
        public static LightService.Context UpdateContext(LightService.Context context) {
            MessageUpdatingAction action = new MessageUpdatingAction();
            return With(context).Reduce(new LightService.IAction[] { action });
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
