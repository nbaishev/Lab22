using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab22
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите n");
            int n = Convert.ToInt32(Console.ReadLine());

            Func<object, int[]> func1 = new Func<object, int[]>(GetArray);
            Task<int[]> task1 = new Task<int[]>(func1, n);

            Action<Task<int[]>> action1 = new Action<Task<int[]>>(Sum);
            Task task2 = task1.ContinueWith(action1);

            Action<Task<int[]>> action2 = new Action<Task<int[]>>(Max);
            Task task3 = task1.ContinueWith(action2);

            Action<Task<int[]>> action3 = new Action<Task<int[]>>(PrintArray);
            Task task4 = task1.ContinueWith(action3);

            task1.Start();
            //task1.Wait(); Не получилось организовать упорядоченное выполнение задач
            //task2.Wait(); Выполняются в произвольном порядке
            //task3.Wait(); Можно поменять методы Sum и Max чтобы они возвращали значение массива и в методах поочередно работать с возвращенными значениями, возможен ли другой вариант?
            /*
            пример метода с возвращаемым значением
        static int[] Sum(Task<int[]> task)
        {
            int s=0;
            int[] array = task.Result;
            Console.WriteLine("Запуск метода поиска суммы");
            for (int i = 0; i < array.Count(); i++)
            {
                s += array[i];
            }
            Console.WriteLine("Сумма равна {0}", s);
            return s;
        }
             */
            Console.ReadKey();
        }

        static int[] GetArray(object num)
        {
            int n = (int) num;
            Console.WriteLine("Запуск заполнения массива");
            int[] array = new int[n];
            Random rnd = new Random();
            for (int i = 0; i < n; i++)
            {
                array[i] = rnd.Next(0, 11);
            }
            Console.WriteLine("Массив заполнен");
            return array;
        }

        static void Sum(Task<int[]> task)
        {
            int s=0;
            int[] array = task.Result;
            Console.WriteLine("Запуск метода поиска суммы");
            for (int i = 0; i < array.Count(); i++)
            {
                s += array[i];
            }
            Console.WriteLine("Сумма равна {0}", s);
        }

        static void Max(Task<int[]> task)
        {
            int [] array = task.Result;
            int max = array[0];
            Console.WriteLine("Запуск метода поиска максимума");
            for (int i = 1; i < array.Count(); i++)
            {
                if (array[i]>max)
                {
                    max = array[i];
                }
            }
            Console.WriteLine("Максимум равен {0}", max);
        }

        static void PrintArray(Task<int[]> task)
        {
            int[] array = task.Result;
            Console.WriteLine("Запуск метода печати");
            for (int i = 0; i < array.Count(); i++)
            {
                Console.Write($"{array[i]}  ");
            }
            Console.WriteLine();
        }
    }
}
