using System;
using System.Collections.Generic;
using System.Linq;

namespace Tasks
{
    public class TaskList
    {
        private ProjectService _projectService;
        public List<Project> projects;
        private long lastId = 0;

        public TaskList()
        {
            projects = new List<Project>();
            _projectService = new ProjectService();
        }
        
        public void Add(string commandLine)
        {
            
            var subcommandRest = commandLine.Split(" ".ToCharArray(), 2);
            var subcommand = subcommandRest[0];
            if (subcommand == "project")
            {
                projects.Add(_projectService.AddProject(subcommandRest[1]));
            } else if (subcommand == "task")
            {
                AddTask(subcommandRest[1]);
            }
        }

        private void AddTask(string subcommand)
        {
            var projectTask = subcommand.Split(" ".ToCharArray(), 2);
            string[] task = GetTaskDescriptionAndId(projectTask[1]);

            _projectService.AddTask(projects, projectTask[0], task[0], task[1]);
        }

        private string[] GetTaskDescriptionAndId(string taskcommand)
        {
            bool isTaskIdSpecified = taskcommand.Contains("@");
            string[] task= new string[2];
            task[0] = taskcommand.Split("@", 2)[0].Trim();
            if (isTaskIdSpecified)
            {
                task[1] = taskcommand.Split("@", 2)[1];
                if (HasAnySpecialCharactersOrSpace(task[1]))
                    throw new Exception("Task id cannot contain spaces and special characters");
            }
            else
                task[1] = NextId().ToString();
            return task;

        }

        private long NextId()
        {
            return ++lastId;
        }

        private bool HasAnySpecialCharactersOrSpace(string taskId)
        {
            return taskId.Any(ch => !Char.IsLetterOrDigit(ch));
        }
    }
}