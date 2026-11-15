using System;

namespace OOP_Lab3_4
{
    // 3) Окремий клас із методами-обробниками події (вимога для оцінки "відмінно")
    public class QueueEventHandler
    {
        // 4) Сигнатура повністю відповідає стандартним подіям .NET Framework: (object sender, TEventArgs e)
        public void HandleQueueOverflow(object sender, QueueOverflowEventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            string senderName = (sender is BoundedQueue<string> q) ? q.QueueName : sender?.GetType().Name;

            Console.WriteLine($"\n[ПОДІЯ ПЕРЕПОВНЕННЯ] Ініціатор: '{senderName}' ({e.OccurredAt:HH:mm:ss})");
            Console.WriteLine($"   Повідомлення: {e.Message}");
            Console.WriteLine($"   Відхилений елемент: '{e.AttemptedItem}' | Ліміт черги: {e.Capacity}\n");
            Console.ResetColor();
        }

        // Додатковий обробник для демонстрації мультиадресного підписання на подію (+= та -=)
        public void LogOverflowToJournal(object sender, QueueOverflowEventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"[ЖУРНАЛ ПОДІЙ] Зафіксовано переповнення при спробі вставки '{e.AttemptedItem}'.\n");
            Console.ResetColor();
        }
    }
}
