using DAL.Interfaces;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Services
{
    public class MemoryDAL : ITaskDAL
    {
        public List<TaskItem> Tasks { get; private set; }

        public MemoryDAL()
        {
            Tasks = new List<TaskItem>();
        }
        public Task<int> AddTask(TaskItem task)
        {
            var id = Tasks.Any() ? Tasks.Max(x => x.Id) : 0;
            task.Id = ++id;
            Tasks.Add(task);
            return Task.FromResult(task.Id);
        }

        public Task<int> DeleteTask(int id)
        {
            var trashItem = Tasks.FirstOrDefault(x => x.Id == id);

            if (trashItem is not null)
            {
                Tasks.Remove(trashItem);
            }
            return Task.FromResult(id);
        }

        public Task<TaskItem> FindTaskByName(string taskName)
        {
            var itemByName = Tasks.FirstOrDefault(x => x.Name == taskName);
            return Task.FromResult(itemByName);
        }

        public Task<int> MarkTask(int id)
        {
            var markItem = Tasks.FirstOrDefault(x => x.Id == id);

            if (markItem is not null)
            {
                markItem.IsDone = true;
            }
            return Task.FromResult(markItem.Id);
        }

        public Task<List<TaskItem>> ReturnTasks()
        {
            return Task.FromResult(Tasks);
        }

        public Task<List<TaskItem>> SortTasksByPriority()
        {
            var sortedItems = Tasks.OrderBy(x => x.Priority).ToList();
            return Task.FromResult(sortedItems);
        }

    }
}
