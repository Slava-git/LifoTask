using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    class LifoTask
    {
        static void Main(string[] args)
        {
            int.TryParse(Console.ReadLine(), out var size);
            var stack = new LifoQueue(size);
            Task.Run(() => Push(stack));
            Task.Run(() => TryPop(stack));
            Task.Run(() => PushAndPop(stack));
            Task.Run(() => PushAndPop(stack));
            Console.WriteLine(stack.Length);
            Console.ReadLine();
        }

        private static void Push(LifoQueue stack)
        {
            for (int i = 0; i < stack.Length; i++)
            {
                Console.WriteLine("Pushed: " + i);
                stack.Push(i);
            }
        }

        private static void TryPop(LifoQueue stack)
        {
            while (true)
            {
                var temp = 0;
                if (stack.Pop(out temp))
                {
                    Console.WriteLine("Pop: " + temp);
                }
            }
        }

        private static void PushAndPop(LifoQueue stack)
        {
            for (int i = 0; i < stack.Length; i++)
            {
                Console.WriteLine("Pushed: " + i);
                stack.Push(i);
                var temp = 0;
                if (stack.Pop(out temp))
                {
                    Console.WriteLine("Pop: " + temp);
                }
            }
        }

        interface IQueue
        {
            public bool Pop(out int value);
            public void Push(int item);
        }
        class LifoQueue : IQueue
        {
            private int[] Array;
            private int Len;

            public LifoQueue(int size)
            {
                Array = new int[size];
            }

            public int Length
            {
                get
                {
                    return Array.Length;
                }
            }

            public bool Pop(out int value)
            {
                lock (Array)
                {
                    var index = Array.Length - Len;
                    value = Array[index];
                    Array[index] = 0;
                    Len--;
                    return true;
                }
            }
            public void Push(int item)
            {
                lock (Array)
                {
                    Array[Array.Length - Len - 1] = item;
                    Len++;
                }
            }
        }
    }
}
