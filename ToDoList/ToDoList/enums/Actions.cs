using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList
{
    public enum Actions
    {
        None = 0,
        CreateTask = 1,
        DeleteTask = 2,
        ViewTasks = 3,
        SortByPriority = 4,
        FindByName = 5,
        MarkTasks = 6,
        Exit = 7
    }
}
