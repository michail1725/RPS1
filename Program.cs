using System;
using UserInterface;

namespace Lab1
{
    
    class Program
    {
        
        static void Main(string[] args)
        {
            UInterface UserInterface = new UInterface();

            Console.WriteLine("Лабораторную работу выполнил студент группы 484 Шаронов Михаил. ");
            Console.WriteLine("Программа предназначена для нахождения среднего арифметического одномерного массива. ");
            Console.WriteLine("Для работы с программой пользователю нужно будет выбрать один из трех вариантов заполнения массива.");
            Console.WriteLine("Для начала работы с программой нажмите любую клавишу...");
            Console.ReadKey(true);
            
            UserInterface.UserChoice();
            UserInterface.ViewResult();
        }
        
    }
}
