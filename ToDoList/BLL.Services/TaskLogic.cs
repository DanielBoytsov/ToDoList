using BLL.Interfaces;
using DAL.Interfaces;
using DAL.Services;
using Entities;
using JsonSettings;
using Shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;



namespace BLL.Services
{

    public class TaskLogic : ITaskLogic
    {
        private readonly ITaskDAL _taskDAL;

        private readonly ICustomCache _cache;

        public TaskLogic()
        {
            _cache = new CustomCache();

            var settingsManager = new JsonSettingsManager();

            var settings = settingsManager.CurrentSettings;

            if (settings.DALServiceNumber == 1)
            {
                _taskDAL = new MemoryDAL();
            }
            else if (settings.DALServiceNumber == 2)
            {
                _taskDAL = new TextFilesDAL(settings);
            }

            CheckOrCreateFilePath(settings.Path);
        }

        public async void CheckOrCreateFilePath(string path) 
        {
            var listItems = new List<TaskItem>();

            if (!File.Exists(path))
            {
                using (FileStream fs = new FileStream(path, FileMode.Create))
                {
                    await JsonSerializer.SerializeAsync<List<TaskItem>>(fs, listItems);
                }
            }
        }

        public async Task<int> AddTask(TaskItem task)
        {
            var taskId = await _taskDAL.AddTask(task);

            _cache.CreateEntry(taskId,task);

            return taskId;
        }

        public async Task<int> DeleteTask(int id)
        {
            _cache.Remove(id);

            return  await _taskDAL.DeleteTask(id);
        }

        public async Task<TaskItem> FindTaskByName(string taskName)
        {
            return await _taskDAL.FindTaskByName(taskName);
        }

        public async Task<int> MarkTask(int id)
        {
            return await _taskDAL.MarkTask(id);
        }

        public async Task<List<TaskItem>> ReturnTasks()
        {
            return await _taskDAL.ReturnTasks();
        }

        public async Task<List<TaskItem>> SortTasksByPriority()
        {
            return await _taskDAL.SortTasksByPriority();
        }
    }
}
