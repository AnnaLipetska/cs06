using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task2
{
    // Создайте свою пользовательскую сборку по примеру сборки CarLibrary из урока, сборка будет
    // использоваться для работы с конвертером температуры.
    public class TemperatureConverter
    {
        public double ConvertCelciusToFahrenheit(double temperature)
        {
            return temperature * 9 / 5 + 32;
        }
    }
}
