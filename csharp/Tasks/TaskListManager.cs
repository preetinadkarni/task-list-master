using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualBasic.CompilerServices;

namespace Tasks
{
	public sealed class TaskListManager
	{
		private const string QUIT = "quit";
		
		private readonly IConsole _console;
		private Print _print;
		private TaskList taskList;
		private ProjectService _projectService; 
		
		public static void Main(string[] args)
		{
			
			new TaskListManager(new RealConsole()).Run();
		}

		public TaskListManager(IConsole console)
		{
			this._console = console;
			taskList = new TaskList();
			_projectService = new ProjectService();
			_print = new Print(console);
		}

		public void Run()
		{
			while (true) {
				_console.Write("> ");
				var command = _console.ReadLine();
				if (command == QUIT) {
					break;
				}
				Execute(command);
			}
		}

		private void Execute(string commandLine)
		{
			var commandRest = commandLine.Split(" ".ToCharArray(), 2);
			var command = commandRest[0];
			var projects = taskList.projects;
			try
			{
				switch (command)
				{
					case "show":
						_print.Show(taskList);
						break;
					case "today":
						_print.ShowAllTasksDueToday(taskList);
						break;
					case "add":
						taskList.Add(commandRest[1]);
						break;
					case "check":
						_projectService.SetDone(projects, commandRest[1], true);
						break;
					case "uncheck":
						_projectService.SetDone(projects, commandRest[1], false);
						break;
					case "deadline":
						_projectService.SetDeadline(projects, commandRest[1]);
						break;
					case "help":
						_print.Help();
						break;
					default:
						_print.Error(command);
						break;
				}
			}
			catch(Exception ex)
			{
				_console.WriteLine(ex.Message);
			}
		}
	}
}
