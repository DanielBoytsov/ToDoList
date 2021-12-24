using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface ITaskDAL
    {
        public Task<int> AddTask(TaskItem task);

        public Task<int> DeleteTask(int id);

        public Task<List<TaskItem>> ReturnTasks();

        public Task<List<TaskItem>> SortTasksByPriority();

        public Task<TaskItem> FindTaskByName(string taskName);

        public Task<int> MarkTask(int id);
    }
}
