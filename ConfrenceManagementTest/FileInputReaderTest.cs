using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConfrenceManagement.Input;
using System.Collections.Generic;
using ConfrenceManagementLogic.Model;
using Moq;

namespace ConfrenceManagementTest
{
    [TestClass]
    public class FileInputReaderTest
    {
        private List<string> input;
        private List<Event> output;
        private FileInputReader fileInputReader;

        [TestInitialize()]
        public void Initialize()
        {
            input = new List<string>();
            output = new List<Event>();
        }

        private void Reset()
        {
            input.Clear();
            output.Clear();
        }

        private IFileHandler MockFileHandlerReturn(List<string> input)
        {
            var mock = new Mock<IFileHandler>();
            mock.Setup(x => x.FileExist("")).Returns(true);
            mock.Setup(x => x.ReadFile("")).Returns(input);
            return mock.Object;
        }

        [TestMethod]
        public void TestInvalidPath()
        {
            var mock = new Mock<IFileHandler>();
            mock.Setup(x => x.FileExist("")).Returns(false);
            mock.Setup(x => x.ReadFile("")).Returns(input);
            fileInputReader = new FileInputReader(mock.Object, "");

            try
            {
                output = fileInputReader.ReadInput();
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Invalid file path", ex.Message);
            }
        }

        [TestMethod]
        public void TestEmptyInput()
        {
            fileInputReader = new FileInputReader(MockFileHandlerReturn(input), "");

            try
            {
                output = fileInputReader.ReadInput();
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.AreEqual("File content is empty", ex.Message);
            }
        }

        [TestMethod]
        public void TestValidInput()
        {
            input.Add("Overdoing it in Python 45min");
            input.Add("Rails for Python Developers lightning");
            input.Add("Lightning Rails for Python lightning fast Developers lightning");

            fileInputReader = new FileInputReader(MockFileHandlerReturn(input), "");

            output = fileInputReader.ReadInput();
            Assert.AreEqual(3, output.Count);

            Event result1 = output[0];
            Assert.AreEqual("Overdoing it in Python", result1.title);
            Assert.AreEqual(45, result1.duration);
            Assert.AreEqual(Event.EventType.Talk, result1.eventType);

            Event result2 = output[1];
            Assert.AreEqual("Rails for Python Developers", result2.title);
            Assert.AreEqual(5, result2.duration);
            Assert.AreEqual(Event.EventType.Talk, result2.eventType);

            Event result3 = output[2];
            Assert.AreEqual("Lightning Rails for Python lightning fast Developers", result3.title);
            Assert.AreEqual(5, result3.duration);
            Assert.AreEqual(Event.EventType.Talk, result3.eventType);
        }

        [TestMethod]
        public void TestInvalidInputFormat()
        {
            input.Add("   ");
            fileInputReader = new FileInputReader(MockFileHandlerReturn(input), "");
            try
            {
                output = fileInputReader.ReadInput();
            }
            catch (Exception)
            {
                Assert.Fail();
            }
            Assert.AreEqual(0, output.Count);

            Reset();
            input.Add("25");
            fileInputReader = new FileInputReader(MockFileHandlerReturn(input), "");
            try
            {
                output = fileInputReader.ReadInput();
                Assert.Fail();
            }
            catch (Exception) { }
            Assert.AreEqual(0, output.Count);

            Reset();
            input.Add("25min");
            fileInputReader = new FileInputReader(MockFileHandlerReturn(input), "");
            try
            {
                output = fileInputReader.ReadInput();
                Assert.Fail();
            }
            catch (Exception) { }
            Assert.AreEqual(0, output.Count);

            Reset();
            input.Add("Accounting-Driven Development");
            fileInputReader = new FileInputReader(MockFileHandlerReturn(input), "");
            try
            {
                output = fileInputReader.ReadInput();
                Assert.Fail();
            }
            catch (Exception) { }
            Assert.AreEqual(0, output.Count);

            Reset();
            input.Add("Accounting-Driven Development in 25");
            fileInputReader = new FileInputReader(MockFileHandlerReturn(input), "");
            try
            {
                output = fileInputReader.ReadInput();
                Assert.Fail();
            }
            catch (Exception) { }
            Assert.AreEqual(0, output.Count);

            Reset();
            input.Add("Accounting-Driven Development in 25 min");
            fileInputReader = new FileInputReader(MockFileHandlerReturn(input), "");
            try
            {
                output = fileInputReader.ReadInput();
                Assert.Fail();
            }
            catch (Exception) { }
            Assert.AreEqual(0, output.Count);

            Reset();
            input.Add("Accounting-Driven Development in lightning fast");
            fileInputReader = new FileInputReader(MockFileHandlerReturn(input), "");
            try
            {
                output = fileInputReader.ReadInput();
                Assert.Fail();
            }
            catch (Exception) { }
            Assert.AreEqual(0, output.Count);

            Reset();
            input.Add("Accounting-Driven Development inlightning");
            fileInputReader = new FileInputReader(MockFileHandlerReturn(input), "");
            try
            {
                output = fileInputReader.ReadInput();
                Assert.Fail();
            }
            catch (Exception) { }
            Assert.AreEqual(0, output.Count);
        }

        [TestMethod]
        public void TestInvalidDuration()
        {
            input.Add("Accounting-Driven Development in 3min");
            fileInputReader = new FileInputReader(MockFileHandlerReturn(input), "");
            try
            {
                output = fileInputReader.ReadInput();
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
