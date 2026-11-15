using System;

namespace OOP_Lab3_4
{
    // 1) Клас-аргумент події переповнення черги (вимога для оцінки "відмінно")
    public class QueueOverflowEventArgs : EventArgs
    {
        public string Message { get; }
        public object AttemptedItem { get; }
        public int Capacity { get; }
        public DateTime OccurredAt { get; }

        public QueueOverflowEventArgs(string message, object attemptedItem, int capacity)
        {
            Message = message;
            AttemptedItem = attemptedItem;
            Capacity = capacity;
            OccurredAt = DateTime.Now;
        }
    }
}
