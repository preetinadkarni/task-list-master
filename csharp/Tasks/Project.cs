using System;
using System.Collections.Generic;

namespace Tasks
{
    public class Project
    {
        public string ProjectId { get; set; }
        public List<Task> Tasks { get; set; }

        public Project(string projectId)
        {
            ProjectId = projectId;
            Tasks = new List<Task>();
        }
    }
}