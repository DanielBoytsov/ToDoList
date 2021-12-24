using Entities;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public interface ICustomCache 
    {
        public static Dictionary<int, TaskItem> Cache { get; set; }

        public void CreateEntry(int key, TaskItem taskItem);

        public void Dispose();

        public void Remove(int key);

        public bool TryGetValue(int key, out TaskItem taskItem);
    }
}
