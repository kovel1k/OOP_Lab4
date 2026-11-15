using System;

namespace OOP_Lab3_4
{
    // Оголошення власного делегата для сортування одновимірного масиву символів (Варіант 4)
    public delegate char[] CharArraySortDelegate(char[] array);

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("=== ЗАВДАННЯ 1: ДЕЛЕГАТИ, АНОНІМНИЙ МЕТОД ТА ЛЯМБДА-ВИРАЗ ===");
            DemonstrateDelegates();

            Console.WriteLine("\n=== ЗАВДАННЯ 2 ТА 3: ПОДІЇ ТА ЧЕРГА ОБМЕЖЕНОЇ ЄМНОСТІ ===");
            DemonstrateEvents();

            Console.WriteLine("\nНатисніть Enter для завершення...");
            Console.ReadLine();
        }

        static void DemonstrateDelegates()
        {
            char[] initialChars = { 'd', 'a', 'z', 'm', 'b', 'k', 'x' };
            Console.WriteLine("Початковий масив символів:       " + string.Join(", ", initialChars));

            // 1. Анонімний метод для сортування одновимірного масиву символів за спаданням
            CharArraySortDelegate anonymousSortDesc = delegate (char[] arr)
            {
                if (arr == null) return Array.Empty<char>();
                char[] copy = (char[])arr.Clone();
                Array.Sort(copy);
                Array.Reverse(copy);
                return copy;
            };

            char[] sortedByAnonymous = anonymousSortDesc(initialChars);
            Console.WriteLine("Анонімний метод (за спаданням):  " + string.Join(", ", sortedByAnonymous));

            // 2. Лямбда-вираз для сортування одновимірного масиву символів за спаданням
            CharArraySortDelegate lambdaSortDesc = (arr) =>
            {
                if (arr == null) return Array.Empty<char>();
                char[] copy = (char[])arr.Clone();
                Array.Sort(copy, (a, b) => b.CompareTo(a));
                return copy;
            };

            char[] sortedByLambda = lambdaSortDesc(initialChars);
            Console.WriteLine("Лямбда-вираз (за спаданням):     " + string.Join(", ", sortedByLambda));
        }

        static void DemonstrateEvents()
        {
            // Ініціалізація черги ємністю 3 елементи
            BoundedQueue<string> taskQueue = new BoundedQueue<string>(3, "Черга завдань");
            QueueEventHandler handler = new QueueEventHandler();

            // Підписка двох обробників на подію переповнення черги (мультиадресність)
            taskQueue.QueueOverflow += handler.HandleQueueOverflow;
            taskQueue.QueueOverflow += handler.LogOverflowToJournal;

            // Заповнення черги
            taskQueue.Enqueue("Завдання 1");
            taskQueue.Enqueue("Завдання 2");
            taskQueue.Enqueue("Завдання 3");

            Console.WriteLine($"Перший елемент у черзі (Peek): '{taskQueue.Peek()}'");

            // Спроба додати 4-й елемент викличе подію QueueOverflow (спрацюють обидва обробники)
            taskQueue.Enqueue("Завдання 4 (зайве)");

            // Скасування підписки другого обробника (-=)
            taskQueue.QueueOverflow -= handler.LogOverflowToJournal;

            // Видалення одного елемента звільняє місце
            taskQueue.Dequeue();

            // Тепер додавання проходить успішно
            taskQueue.Enqueue("Завдання 4 (успішно)");

            // Чергова спроба переповнити чергу (спрацює лише перший обробник)
            taskQueue.Enqueue("Завдання 5 (зайве)");
        }
    }
}
