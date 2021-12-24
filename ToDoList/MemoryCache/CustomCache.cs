using Entities;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;

namespace Shared
{
    public class CustomCache : ICustomCache
    {
        public static Dictionary<int,TaskItem> Cache { get; set; }

        public CustomCache()
        {
            Cache = new Dictionary<int, TaskItem>();
        }

        public void CreateEntry(int key, TaskItem taskItem)
        {
            Cache.Add(key, taskItem);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Remove(int key)
        {
            Cache.Remove(key);
        }

        public bool TryGetValue(int key, out TaskItem taskItem)
        {
            return Cache.TryGetValue(key, out taskItem);
        }
    }
}
