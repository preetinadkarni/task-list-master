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
                string taskId;                
                var projectTask = subcommandRest[1].Split(" ".ToCharArray(), 2);
                var taskDescription = projectTask[1];
                bool isTaskIdSpecified = taskDescription.Contains("@");

                if (isTaskIdSpecified)
                {
                    taskDescription = projectTask[1].Split("@", 2)[0].Trim();
                    taskId = projectTask[1].Split("@", 2)[1];
                    if (HasAnySpecialCharactersOrSpace(taskId))
                        throw new Exception("Task id cannot contain spaces and special characters");
                }
                else
                    taskId = NextId().ToString();
                _projectService.AddTask(projects,projectTask[0], taskDescription, taskId );
            }
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