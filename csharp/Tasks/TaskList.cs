using System.Collections.Generic;

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
            } else if (subcommand == "task") {
                var projectTask = subcommandRest[1].Split(" ".ToCharArray(), 2);
                _projectService.AddTask(projects,projectTask[0], projectTask[1], NextId());
            }
        }
        
        private long NextId()
        {
            return ++lastId;
        }
    }
}