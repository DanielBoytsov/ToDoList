using System;

namespace Entities
{
    public class TaskItem : IEquatable<TaskItem>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Priority { get; set; }

        public string Text { get; set; }

        public bool IsDone { get; set; }

        public TaskItem()
        {

        }

        public override string ToString()
        {
            return $"Id: {Id} \nName: {Name} \nPriority: {Priority} \nText: {Text} \nIs Done: {IsDone}";
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as TaskItem);
        }

        public bool Equals(TaskItem other)
        {
            return other != null &&
                   Id == other.Id &&
                   Name == other.Name &&
                   Priority == other.Priority &&
                   Text == other.Text &&
                   IsDone == other.IsDone;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Name, Priority, Text, IsDone);
        }
    }
}
