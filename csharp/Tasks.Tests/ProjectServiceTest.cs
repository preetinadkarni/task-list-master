using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Tasks
{
    [TestFixture]
    public sealed class ProjectServiceTest
    {
        private ProjectService _projectService;
        
        [SetUp]
        public void Setup()
        {
            _projectService = new ProjectService();
        }
        
        [Test, Timeout(500)]
        public void GivenProjectNameWhenAddProjectCalledThenReturnsNewProject()
        {
            Project p = _projectService.AddProject("secrets");
            Assert.AreEqual("secrets",p.ProjectId);
            Assert.AreEqual(0,p.Tasks.Count);
        }
        
        [Test, Timeout(500)]
        public void GivenTaskWhenAddTaskIsCalledThenShouldAddTaskToProjectList()
        {
            List<Project> projects = new List<Project>{};
            projects.Add(_projectService.AddProject("secrets"));
            projects.Add(_projectService.AddProject("project training"));
            _projectService.AddTask(projects, "project training","training SOLID",1);
            Task expected = new Task { Id = 1, Description = "training SOLID", Done = false };
            Task actual = _projectService.GetTask(projects, 1);
            Assert.AreEqual(expected.Id,actual.Id);
            Assert.AreEqual(expected.Description,actual.Description);
            Assert.AreEqual(expected.Done,actual.Done);
            Assert.AreEqual(expected.Deadline,actual.Deadline);
        }
        
        [Test, Timeout(500)]
        public void GivenTaskIdAndDoneWhenSetDoneIsCalledThenSetDoneCorrectly()
        {
            List<Project> projects = new List<Project>{};
            projects.Add(_projectService.AddProject("secrets"));
            _projectService.AddTask(projects, "secrets","training SOLID",1);
            _projectService.SetDone(projects,"1",true);
            Assert.AreEqual(true,_projectService.GetTask(projects,1).Done);
        }
        
        [Test, Timeout(500)]
        public void GivenProjectsAndDeadlineWhenSetDeadlineIsCalledThenSetDeadlineCorrectly()
        {
            List<Project> projects = new List<Project>{};
            projects.Add(_projectService.AddProject("secrets"));
            _projectService.AddTask(projects, "secrets","training SOLID",1);
            _projectService.SetDeadline(projects,"1 10-22-2022");
            string deadline = _projectService.GetTask(projects, 1).Deadline?.ToString("MM-dd-yyyy");
            Assert.AreEqual("10-22-2022", deadline);
        }

        [Test, Timeout(500)]
        public void GivenValidTaskWhenGetTaskIsCalledThenShouldReturnTask()
        {
            List<Project> projects = new List<Project> { };
            projects.Add(_projectService.AddProject("secrets"));
            _projectService.AddTask(projects, "secrets", "training SOLID", 1);
            Task expected = new Task { Id = 1, Description = "training SOLID", Done = false };
            Task actual = _projectService.GetTask(projects, 1);
            Assert.AreEqual(expected.Id,actual.Id);
            Assert.AreEqual(expected.Description,actual.Description);
            Assert.AreEqual(expected.Done,actual.Done);
            Assert.AreEqual(expected.Deadline,actual.Deadline);
        }

        [Test, Timeout(500)]
        public void GivenInvalidTaskWhenGetTaskIsCalledThenThrowError()
        {
            List<Project> projects = new List<Project> { };
            projects.Add(_projectService.AddProject("secrets"));
            _projectService.AddTask(projects, "secrets", "training SOLID", 1);
            var ex =Assert.Throws<Exception>( ()=> _projectService.GetTask(projects, 2));
            Assert.AreEqual("Could not find a task with id \"2\".",ex.Message);
        }
    }
}