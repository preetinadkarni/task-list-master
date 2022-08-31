using System;
using System.Collections.Generic;

namespace Tasks
{
    public class ProjectService
    {
        public Project AddProject(string name)
        {
            return new Project(name);
        }
        
        public void AddTask(List<Project> projectsList,string project, string description, string taskId)
        {
            Project p = projectsList.Find(p => p.ProjectId == project);
            if ( p == null)
            {
                throw new Exception("Could not find a project with the name \"" + project + "\".");
            }
            p.Tasks.Add( new Task{ Id = taskId, Description = description, Done = false });
        }
        public void SetDone(List<Project> projects, string idString, bool done)
        {
            // int id = int.Parse(idString);
            Task task = GetTask(projects, idString);
            task.Done = done;
        }
        
        public void SetDeadline(List<Project> projects,string commandLine)
        {
            var subcommandRest = commandLine.Split(" ".ToCharArray(), 2);
            // int id = int.Parse(subcommandRest[0]);

            Task task = GetTask(projects, subcommandRest[0]);
            task.Deadline = DateTime.Parse(subcommandRest[1]);
        }

        public Task GetTask(List<Project> projects, string id)
        {
            foreach (var project in projects) {
                foreach (var task in project.Tasks) {
                    if(task.Id == id)
                    {
                        return task;
                    }
                }
            }
            throw new Exception("Could not find a task with id \"" + id + "\".");
        }
    }
}