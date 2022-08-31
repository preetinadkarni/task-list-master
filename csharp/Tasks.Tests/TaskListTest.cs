using System;
using NUnit.Framework;

namespace Tasks
{
    [TestFixture]
    public sealed class TaskListTest
    {
        private TaskList taskList;
        
        [SetUp]
        public void Setup()
        {
            taskList  =new TaskList();
        }
        
        [Test, Timeout(500)]
        public void GivenCommandLineHasProjectWhenAddIsCalledThenAddsProject()
        {
            string commandLine = "project secrets";
            taskList.Add(commandLine);
            var projects = taskList.projects;
            Assert.AreEqual(projects.Count, 1);
            Assert.AreEqual(projects[0].ProjectId, "secrets");
        }
        
        [Test, Timeout(500)]
        public void GivenCommandLineHasTaskWhenAddIsCalledThenAddsTask()
        {
            taskList.Add("project secrets");
            string commandLine = "task secrets SOLID";
            taskList.Add(commandLine);
            var tasks = taskList.projects[0].Tasks[0];
            
            Assert.AreEqual("SOLID", tasks.Description);
            Assert.AreEqual( "1", tasks.Id);
        }
        
        [Test, Timeout(500)]
        [TestCase("task secrets SOLID @ST1", "SOLID", "ST1")]
        [TestCase("task secrets SOLID Training @123", "SOLID Training", "123")]
        [TestCase("task secrets SOLID @ABC", "SOLID", "ABC")]
        public void GivenCommandLineHasTaskWithTaskIDWhenAddIsCalledThenAddsTask(string taskCommandLine, string expectedDescription, string expectedTaskId)
        {
            taskList.Add("project secrets");
            string commandLine = taskCommandLine;
            taskList.Add(commandLine);
            var tasks = taskList.projects[0].Tasks[0];
            
            Assert.AreEqual(expectedDescription, tasks.Description);
            Assert.AreEqual( expectedTaskId, tasks.Id);
        }
        
        [Test, Timeout(500)]
        [TestCase("task secrets SOLID @ST$$1")]
        [TestCase("task secrets SOLID @ST  1")]
        [TestCase("task secrets SOLID @ST^&*1")]
        public void GivenCommandLineHasTaskWithSpecialCharsTaskIDWhenAddIsCalledThenThrowsException(string taskCommandLine)
        {
            taskList.Add("project secrets");
            string commandLine = taskCommandLine;
            var ex =Assert.Throws<Exception>( ()=> taskList.Add(commandLine));
            Assert.AreEqual("Task id cannot contain spaces and special characters",ex.Message);
        }
    }
}