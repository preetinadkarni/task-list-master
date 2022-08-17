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
            Assert.AreEqual( 1, tasks.Id);
        }
        
        [Test, Timeout(500)]
        public void GivenCommandLineHasTaskWithTaskIDWhenAddIsCalledThenAddsTask()
        {
            taskList.Add("project secrets");
            string commandLine = "task secrets SOLID ST1";
            taskList.Add(commandLine);
            var tasks = taskList.projects[0].Tasks[0];
            
            Assert.AreEqual("SOLID", tasks.Description);
            Assert.AreEqual( "ST1", tasks.Id);
        }
    }
}