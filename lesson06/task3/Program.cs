using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace task3
{
    class Program
    {
        // Создайте программу, в которой предоставьте пользователю доступ к сборке из Задания 2.
        // Реализуйте использование метода конвертации значения температуры из шкалы Цельсия в
        // шкалу Фаренгейта.Выполняя задание используйте только рефлексию
        static void Main(string[] args)
        {
            var assembly = Assembly.Load("task2");

            dynamic converter = Activator.CreateInstance(assembly.GetType("task2.TemperatureConverter"));

            var tC = 21d;
            var tF = converter.ConvertCelciusToFahrenheit(tC);

            Console.WriteLine($"{tC} °C равно {tF} по °F");

            Console.ReadKey();
        } 
    }
}
