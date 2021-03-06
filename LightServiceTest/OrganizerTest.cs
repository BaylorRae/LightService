﻿using System;
using NUnit.Framework;

namespace LightServiceTest {
    [TestFixture]
    public class OrganizerTest {

        [Test]
        public void WithCreatesADefaultContext() {
            Assert.IsTrue(LightService.Organizer<LightService.Context>.With().context is LightService.Context, "Organizer default context must be an instance of LightService.Context");
        }

        [Test]
        public void ItUsesTheContextPassedIntoWith() {
            LightService.Context context = new LightService.Context();
            LightService.Organizer<LightService.Context> org = LightService.Organizer<LightService.Context>.With(context);
            Assert.AreSame(context, org.context);
        }

        [Test]
        public void ItCallsAnAction() {
            ExampleAction action = new ExampleAction();

            LightService.IAction<LightService.Context>[] actions = new LightService.IAction<LightService.Context>[] { action };
            ExampleOrganizer.DoSomething(actions);

            Assert.AreEqual("called!!!", action.Status);
        }

        [Test]
        public void ItCallsMultipleActions() {
            ExampleAction action = new ExampleAction();
            ExampleAction action2 = new ExampleAction();

            LightService.IAction<LightService.Context>[] actions = new LightService.IAction<LightService.Context>[] { action, action2 };
            ExampleOrganizer.DoSomething(actions);

            Assert.AreEqual("called!!!", action.Status);
            Assert.AreEqual("called!!!", action2.Status, "LightService.Organizer Reduce only called a single action");
        }

        [Test]
        public void ReduceReturnsTheOrganizersContext() {
            LightService.Context context = new LightService.Context();

            LightService.Organizer<LightService.Context> org = LightService.Organizer<LightService.Context>.With(context);
            LightService.Context newContext = org.Reduce(new LightService.IAction<LightService.Context>[] { new ExampleAction() });

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

    internal class ExampleAction : LightService.IAction<LightService.Context> {
        public string Status { get; set; }

        public ExampleAction() {
            this.Status = "hidy ho neighbor";
        }

        public LightService.Context Executed(LightService.Context context) {
            this.Status = "called!!!";
            return context;
        }
    }

    internal class ExampleOrganizer : LightService.Organizer<LightService.Context> {

        public static void DoSomething(LightService.IAction<LightService.Context>[] actions) {
            With().Reduce(actions);
        }

    }

    internal class MessageUpdatingAction : LightService.IAction<LightService.Context> {
        public LightService.Context Executed(LightService.Context context) {
            context["message"] = "updated context message";
            return context;
        }
    }

    internal class MessageUpdatingOrganizer : LightService.Organizer<LightService.Context> {
        public static LightService.Context UpdateContext(LightService.Context context) {
            MessageUpdatingAction action = new MessageUpdatingAction();
            return With(context).Reduce(new LightService.IAction<LightService.Context>[] { action });
        }
    }
}
