using DAL.Interfaces;
using Entities;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace DAL.Services
{
    public class TextFilesDAL : ITaskDAL
    {
        private string _path;

        public TextFilesDAL()
        {

        }
        public TextFilesDAL(JsonSettings.JsonSettings settings)
        {
            _path = settings.Path;
        }

        public async Task<int> AddTask(TaskItem task)
        {
            var taskItems = new List<TaskItem>();

            using (FileStream fs = new FileStream(_path, FileMode.Open))
            {
                taskItems = await JsonSerializer.DeserializeAsync<List<TaskItem>>(fs).ConfigureAwait(false);
            }

            var id = taskItems.Any() ? taskItems.Max(x => x.Id) : 0;
            task.Id = ++id; 
            taskItems.Add(task);

            using (FileStream fs = new FileStream(_path, FileMode.Create))
            {
                await JsonSerializer.SerializeAsync<List<TaskItem>>(fs, taskItems).ConfigureAwait(false);

                return task.Id;
            }
        }

        public async Task<int> DeleteTask(int id)
        {
            var taskItems = new List<TaskItem>();

            using (FileStream fs = new FileStream(_path, FileMode.Open))
            {
                taskItems = await JsonSerializer.DeserializeAsync<List<TaskItem>>(fs).ConfigureAwait(false);
            }

            var trashItem = taskItems.FirstOrDefault(x => x.Id == id);

            if(trashItem is not null)
            {
                taskItems.Remove(trashItem);
            }

            using (FileStream fs = new FileStream(_path, FileMode.Create))
            {
                await JsonSerializer.SerializeAsync<List<TaskItem>>(fs, taskItems).ConfigureAwait(false);

                return id;
            }
        }

        public async Task<TaskItem> FindTaskByName(string name)
        {
            var taskItems = new List<TaskItem>();

            using (FileStream fs = new FileStream(_path, FileMode.Open))
            {
                taskItems = await JsonSerializer.DeserializeAsync<List<TaskItem>>(fs).ConfigureAwait(false);
            }

            var itemByName = taskItems.FirstOrDefault(x => x.Name == name);

            return itemByName;
        }

        public async Task<int> MarkTask(int id)
        {
            var taskItems = new List<TaskItem>();

            using (FileStream fs = new FileStream(_path, FileMode.Open))
            {
                taskItems = await JsonSerializer.DeserializeAsync<List<TaskItem>>(fs).ConfigureAwait(false);
            }

            var markItem = taskItems.FirstOrDefault(x => x.Id == id);

            if (markItem is not null)
            {
                markItem.IsDone = true;
            }

            using (FileStream fs = new FileStream(_path, FileMode.Create))
            {
                await JsonSerializer.SerializeAsync<List<TaskItem>>(fs, taskItems).ConfigureAwait(false);

                return id;
            }
        }

        public async Task<List<TaskItem>> ReturnTasks()
        {
            using (FileStream fs = new FileStream(_path, FileMode.Open))
            {
                var taskItems = await JsonSerializer.DeserializeAsync<List<TaskItem>>(fs).ConfigureAwait(false);

                return taskItems;
            }
        }

        public async Task<List<TaskItem>> SortTasksByPriority()
        {
            var taskItems = new List<TaskItem>();

            using (FileStream fs = new FileStream(_path, FileMode.Open))
            {
                taskItems = await JsonSerializer.DeserializeAsync<List<TaskItem>>(fs).ConfigureAwait(false);
                
            }

            var sortedItems = taskItems.OrderBy(x => x.Priority).ToList();

            return sortedItems;
        }
    }
}
