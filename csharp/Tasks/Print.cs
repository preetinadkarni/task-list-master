using System;
using System.Collections.Generic;

namespace Tasks
{
    public class Print
    {
        private readonly IConsole _console;
        
        public Print(IConsole console)
        {
            _console = console;
        }
        public void Show(TaskList taskList)
        {
            foreach (var project in taskList.projects) {
                _console.WriteLine(project.ProjectId);
                foreach (var task in project.Tasks) {
                    _console.WriteLine("    [{0}] {1}: {2}{3}", (task.Done ? 'x' : ' '), task.Id, task.Description,
                        (task.Deadline==null ? "" : " " + task.Deadline.Value.ToString("MM-dd-yyyy")) );
                }
                _console.WriteLine();
            }
        }
        
        public void ShowAllTasksDueToday(TaskList taskList)
        {
            foreach (var project in taskList.projects) {
                foreach (var task in project.Tasks) {
                    if(task.Deadline == DateTime.Today)
                    {
                        _console.WriteLine("    [{0}] {1}: {2}{3} {4}", (task.Done ? 'x' : ' '), task.Id, task.Description,
                            (task.Deadline==null ? "" : " " + task.Deadline.Value.ToString("MM-dd-yyyy")), project.ProjectId );
                    }
                }
            }
        }
        
        public void Help()
        {
            _console.WriteLine("Commands:");
            _console.WriteLine("  show");
            _console.WriteLine("  add project <project name>");
            _console.WriteLine("  add task <project name> <task description>");
            _console.WriteLine("  check <task ID>");
            _console.WriteLine("  uncheck <task ID>");
            _console.WriteLine("  deadline <task ID> <date mm/dd/yyyy format>");
            _console.WriteLine();
        }
        
        public void Error(string command)
        {
            _console.WriteLine("I don't know what the command \"{0}\" is.", command);
        }

    }
}