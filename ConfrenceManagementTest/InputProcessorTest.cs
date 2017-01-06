using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConfrenceManagement.Input;
using System.Collections.Generic;
using ConfrenceManagement.Model;

namespace ConfrenceManagementTest
{
    [TestClass]
    public class InputProcessorTest
    {
        private List<string> input;
        private List<Event> output;
        private InputProcessor inputProcessor;

        [TestInitialize()]
        public void Initialize()
        {
            input = new List<string>();
            output = new List<Event>();
            inputProcessor = new InputProcessor();
        }

        private void Reset()
        {
            input.Clear();
            output.Clear();
        }

        [TestMethod]
        public void TestEmptyInput()
        {
            output = inputProcessor.ParseLines(input);
            Assert.AreEqual(0, output.Count);
        }

        [TestMethod]
        public void TestValidInput()
        {
            input.Add("Overdoing it in Python 45min");
            output = inputProcessor.ParseLines(input);
            Assert.AreEqual(1, output.Count);

            Event result = output[0];
            Assert.AreEqual("Overdoing it in Python", result.title);
            Assert.AreEqual(45, result.duration);

            Reset();
            input.Add("Rails for Python Developers lightning");
            output = inputProcessor.ParseLines(input);
            Assert.AreEqual(1, output.Count);

            result = output[0];
            Assert.AreEqual("Rails for Python Developers", result.title);
            Assert.AreEqual(5, result.duration);

            Reset();
            input.Add("Lightning Rails for Python lightning fast Developers lightning");
            output = inputProcessor.ParseLines(input);
            Assert.AreEqual(1, output.Count);

            result = output[0];
            Assert.AreEqual("Lightning Rails for Python lightning fast Developers", result.title);
            Assert.AreEqual(5, result.duration);
        }

        [TestMethod]
        public void TestInvalidInputFormat()
        {
            input.Add("   ");
            try
            {
                output = inputProcessor.ParseLines(input);
                Assert.Fail();
            }
            catch (Exception) { }
            Assert.AreEqual(0, output.Count);

            Reset();
            input.Add("25");
            try
            {
                output = inputProcessor.ParseLines(input);
                Assert.Fail();
            }
            catch (Exception) { }
            Assert.AreEqual(0, output.Count);

            Reset();
            input.Add("25min");
            try
            {
                output = inputProcessor.ParseLines(input);
                Assert.Fail();
            }
            catch (Exception) { }
            Assert.AreEqual(0, output.Count);

            Reset();
            input.Add("Accounting-Driven Development");
            try
            {
                output = inputProcessor.ParseLines(input);
                Assert.Fail();
            }
            catch (Exception) { }
            Assert.AreEqual(0, output.Count);

            Reset();
            input.Add("Accounting-Driven Development in 25");
            try
            {
                output = inputProcessor.ParseLines(input);
                Assert.Fail();
            }
            catch (Exception) { }
            Assert.AreEqual(0, output.Count);

            Reset();
            input.Add("Accounting-Driven Development in 25 min");
            try
            {
                output = inputProcessor.ParseLines(input);
                Assert.Fail();
            }
            catch (Exception) { }
            Assert.AreEqual(0, output.Count);

            Reset();
            input.Add("Accounting-Driven Development in lightning fast");
            try
            {
                output = inputProcessor.ParseLines(input);
                Assert.Fail();
            }
            catch (Exception) { }
            Assert.AreEqual(0, output.Count);

            Reset();
            input.Add("Accounting-Driven Development inlightning");
            try
            {
                output = inputProcessor.ParseLines(input);
                Assert.Fail();
            }
            catch (Exception) { }
            Assert.AreEqual(0, output.Count);
        }

        [TestMethod]
        public void TestInvalidDuration()
        {
            input.Add("Accounting-Driven Development in 3min");
            try
            {
                output = inputProcessor.ParseLines(input);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.Message.Contains("Event duration must be in 5 minutes interval:"));
            }
            Assert.AreEqual(0, output.Count);
        }
    }
}
