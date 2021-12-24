using BLL.Interfaces;
using BLL.Services;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList
{
    public class ConsoleInterface
    {
        private readonly ITaskLogic _taskLogic;
        public ConsoleInterface()
        {
            _taskLogic = new TaskLogic();
        }
        
        public void ConsoleOutput()
        {
            while (true)
            {
                Console.WriteLine(@"Choose actions: " +
                                  "\n1. Create task" +
                                  "\n2. Delete task" +
                                  "\n3. View all tasks" +
                                  "\n4. Sort tasks by priority" +
                                  "\n5. Find task by name" +
                                  "\n6. Mark completed task" +
                                  "\n7. Exit");
                string action = Console.ReadLine();
                Enum.TryParse<Actions>(action, out Actions actionResult);
                ChooseAction(actionResult);
                if (actionResult == Actions.Exit)
                {
                    Console.WriteLine("Bye, I hope you'll coming back :)");
                    break;
                }
            }
        }

        public void ChooseAction(Actions select)
        {
            switch (select)
            {
                case Actions.CreateTask:
                    CreateTask();
                    break;
                case Actions.DeleteTask:
                    DeleteTask();
                    break;
                case Actions.ViewTasks:
                    ViewTasks();
                    break;
                case Actions.SortByPriority:
                    SortByPriority();
                    break;
                case Actions.FindByName:
                    FindByName();
                    break;
                case Actions.MarkTasks:
                    MarkTasks();
                    break;
                case Actions.Exit:
                    break;  
            }
        }

        public void CreateTask()
        {
            var item = new TaskItem();
            Console.WriteLine("Enter the name of your task");
            item.Name = Console.ReadLine();
            Console.WriteLine("Enter the priority of your task");
            int.TryParse(Console.ReadLine(), out int result);
            item.Priority = result;
            Console.WriteLine("Enter the text");
            item.Text = Console.ReadLine();
            _taskLogic.AddTask(item);

        }

        public void DeleteTask()
        {
            Console.WriteLine("Enter the id of task");
            int.TryParse(Console.ReadLine(), out int id);
            _taskLogic.DeleteTask(id); 
        }

        public async void ViewTasks()
        {
            var tasks = await _taskLogic.ReturnTasks();
            foreach (var item in tasks)
            {
                Console.WriteLine(item);
            }
            
        }

        public async void SortByPriority()
        {
            var sortedTasks = await _taskLogic.SortTasksByPriority();
            foreach (var item in sortedTasks)
            {
                Console.WriteLine(item);
            }
        }

        public async void FindByName()
        {
            Console.WriteLine("Enter the name");
            var taskName = Console.ReadLine();
            var searchedTask = await _taskLogic.FindTaskByName(taskName);
            Console.WriteLine(searchedTask);
        }

        public void MarkTasks()
        {
            Console.WriteLine("Enter the id for completed task");
            int.TryParse(Console.ReadLine(), out int id);
            _taskLogic.MarkTask(id);
        }
    }
}
