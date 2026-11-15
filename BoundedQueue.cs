using System;

namespace OOP_Lab3_4
{
    // 2) Компонент багаторазового використання - Черга обмеженої ємності (Варіант 4)
    // Містить подію QueueOverflow та захищений метод OnQueueOverflow, що її викликає
    public class BoundedQueue<T>
    {
        private readonly T[] _items;
        private int _head;
        private int _tail;
        private int _count;

        public int Capacity { get; }
        public int Count => _count;
        public bool IsFull => _count == Capacity;
        public bool IsEmpty => _count == 0;
        public string QueueName { get; }

        // Подія з використанням стандартного узагальненого делегата EventHandler<TEventArgs>
        public event EventHandler<QueueOverflowEventArgs> QueueOverflow;

        // Ініціалізація черги заданої ємності
        public BoundedQueue(int capacity, string queueName = "Черга")
        {
            if (capacity <= 0)
                throw new ArgumentException("Ємність черги повинна бути більшою за 0.", nameof(capacity));

            Capacity = capacity;
            QueueName = queueName;
            _items = new T[capacity];
            _head = 0;
            _tail = 0;
            _count = 0;
        }

        // Розміщення даних у черзі (Enqueue)
        public void Enqueue(T item)
        {
            if (IsFull)
            {
                // Генерація події при переповненні черги
                OnQueueOverflow(new QueueOverflowEventArgs(
                    $"Спроба додати елемент '{item}' у заповнену чергу (макс. ємність: {Capacity}).",
                    item,
                    Capacity));
                return;
            }

            _items[_tail] = item;
            _tail = (_tail + 1) % Capacity;
            _count++;
            Console.WriteLine($"[+] Додано елемент '{item}'. Заповнено: {_count}/{Capacity}");
        }

        // Видалення даних із черги (Dequeue)
        public T Dequeue()
        {
            if (IsEmpty)
                throw new InvalidOperationException("Черга порожня. Неможливо видалити елемент.");

            T removedItem = _items[_head];
            _items[_head] = default(T);
            _head = (_head + 1) % Capacity;
            _count--;
            Console.WriteLine($"[-] Видалено елемент '{removedItem}'. Залишилось: {_count}/{Capacity}");
            return removedItem;
        }

        // Перегляд першого елемента без видалення
        public T Peek()
        {
            if (IsEmpty)
                throw new InvalidOperationException("Черга порожня.");
            return _items[_head];
        }

        // Очищення черги
        public void Clear()
        {
            Array.Clear(_items, 0, Capacity);
            _head = 0;
            _tail = 0;
            _count = 0;
            Console.WriteLine($"[!] Чергу '{QueueName}' очищено.");
        }

        // Захищений метод для безпечного виклику події
        protected virtual void OnQueueOverflow(QueueOverflowEventArgs e)
        {
            QueueOverflow?.Invoke(this, e);
        }
    }
}
