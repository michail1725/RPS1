using System;
using System.Linq;
using System.IO;
using Lab1;

namespace UserInterface
{
    class UInterface
    {
        ArrayCalc Calc = new ArrayCalc();
        ArrayFill ArrayFill = new ArrayFill();
        int len;
        int[] arr;
        double result;
        ConsoleKeyInfo keyboardSymbol;
        public void UserChoice() {
            
            Console.WriteLine("Как Вы хотите заполнить массив(Нажмите нужную клавишу)");
            Console.WriteLine("Нажмите 1, чтобы заполнить массив рандомно");
            Console.WriteLine("Нажмите 2, чтобы заполнить массив самостоятельно");
            Console.WriteLine("Нажмите 3, чтобы заполнить массив из файла");
            while (true)
            {
                keyboardSymbol = Console.ReadKey(true);
                if (keyboardSymbol.Key == ConsoleKey.D1 || keyboardSymbol.Key == ConsoleKey.NumPad1)
                { 
                    ArrayFill.Fill(Input(),arr);
                    Console.WriteLine("Генерация массива произведена успешно");
                    Console.WriteLine("Исходный массив: ");
                    for (int i = 0; i < arr.Length; i++)
                    {
                        Console.WriteLine(arr[i]);
                    }
                    Console.WriteLine("Хотите сохранить получившийся массив в файл для дальнейшей работы?\n1.Да\n2.Нет");
                    while (true)
                    {
                        keyboardSymbol = Console.ReadKey(true);
                        if (keyboardSymbol.Key == ConsoleKey.D1 || keyboardSymbol.Key == ConsoleKey.NumPad1)
                        {
                            ArrOutfile(OutfileCheck());
                            break;
                        }
                        else if (keyboardSymbol.Key == ConsoleKey.D2 || keyboardSymbol.Key == ConsoleKey.NumPad2)
                        {
                            break;
                        }
                    }
                    return;
                }
                else if (keyboardSymbol.Key == ConsoleKey.D2 || keyboardSymbol.Key == ConsoleKey.NumPad2)
                {
                    Input();
                    ArrInput();
                    return;
                }
                else if (keyboardSymbol.Key == ConsoleKey.D3 || keyboardSymbol.Key == ConsoleKey.NumPad3)
                {
                    ArrFileOpen();
                    Console.WriteLine("Считывание массива из файла произведено успешно.");
                    Console.WriteLine("Исходный массив: ");
                    for (int i = 0; i < arr.Length; i++)
                    {
                        Console.WriteLine(arr[i]);
                    }
                    return;
                }
            }
            
        }
        public int Input() {
            string str;
            
            Console.WriteLine("Введите размерность массива: ");
            str = Console.ReadLine();
            while (!Int32.TryParse(str, out len) || len<1) {
                Console.WriteLine("Введен некорректный размер массива. Повторите попытку.");
                Console.WriteLine("Введите размерность массива: ");
                str = Console.ReadLine();
            }
            arr = new int[len];
            return len;
        }
        public void Output() {
            Console.WriteLine("Желаете сохранить результат программы?\n1.Да\n2.Нет");
            while (true)
            {
                keyboardSymbol = Console.ReadKey(true);
                if (keyboardSymbol.Key == ConsoleKey.D1|| keyboardSymbol.Key == ConsoleKey.NumPad1)
                {
                    ResultsOutfile(OutfileCheck());
                    break;
                }
                else if (keyboardSymbol.Key == ConsoleKey.D2 || keyboardSymbol.Key == ConsoleKey.NumPad2)
                {
                    break;
                }
            }
        }
        public void ArrInput() {
            Console.WriteLine("Введите элементы массива через Enter: ");
            for (int i = 0; i < len; i++) {
                string Input = Console.ReadLine(); 
                while (!Int32.TryParse(Input, out arr[i])) {
                    Console.WriteLine("Некорректный ввод элемента массива. Повторите ввод:");
                    Input = Console.ReadLine();
                }
            }
            Console.WriteLine("Считывание массива произведено успешно");
           
            Console.WriteLine("Хотите сохранить получившийся массив в файл для дальнейшей работы?\n1.Да\n2.Нет");
            while (true)
            {
                keyboardSymbol = Console.ReadKey(true);
                if (keyboardSymbol.Key == ConsoleKey.D1 || keyboardSymbol.Key == ConsoleKey.NumPad1)
                {
                    ArrOutfile(OutfileCheck());
                    return;
                }
                else if (keyboardSymbol.Key == ConsoleKey.D2 || keyboardSymbol.Key == ConsoleKey.NumPad2) {
                    return;
                }
            }

        }
        public void ArrFileOpen() {
           string filePath;
           FileStream file;
           while (true) {
           Console.WriteLine( "Введите имя файла: " );
		   filePath = Console.ReadLine(); 
                 try
                 {
                    file = File.Open(filePath, FileMode.Open);
                    

                 }
                catch (ArgumentException)
                {
                    Console.WriteLine("Некорректный путь файла. Повторите ввод.");
                    continue;
                }
                catch (FileNotFoundException)
                {
                    Console.WriteLine("Файл не существует. Выберите другой файл."); 
                    continue;
                }
                catch (IOException)
                {
                    Console.WriteLine("Неопределенная ошибка. Повторите попытку или выберите другой файл.");
                    continue;
                }
                FileInfo infileInfo = new FileInfo(filePath);
                if (infileInfo.Length == 0)
                {
                    Console.WriteLine("Файл пуст. Выберите другой файл");
                    continue;
                }
                byte[] array = new byte[file.Length];
                file.Read(array, 0, array.Length);
                file.Close();
                string textFromFile = System.Text.Encoding.Default.GetString(array);
                try
                {
                    arr = textFromFile.Split(new char[] { ' ', '\n' }, StringSplitOptions.RemoveEmptyEntries).Select(Int32.Parse).ToArray();
                }
                catch {
                    Console.WriteLine("Неверный формат исходного файла.Выберите другой файл.");
                    continue;
                }
                break;
            }
            
        }
			
		public void ArrOutfile(string path) {
            File.WriteAllLines(path, arr.Select(s => s.ToString()).ToArray());
            Console.WriteLine("Сохранение произведено успешно");
        }
        public void ResultsOutfile(string path) {
            File.WriteAllText(path, "Среднее арифметическое исходного массива: " + result);
            Console.WriteLine("Сохранение произведено успешно");
        }
        public string OutfileCheck() { 
            string filePath;
            FileStream file;
            while(true)
            {

                try

                {
                    Console.WriteLine("Введите путь нового файла:");
                    filePath = Console.ReadLine();
                    file = File.Open(filePath, FileMode.OpenOrCreate);
                    file.Close();

                }

                catch (ArgumentException)

                {

                    Console.WriteLine("Неверное имя файла или файл недоступен для записи, попробуйте снова");
                    filePath = Console.ReadLine();

                    continue;

                }

                catch (IOException)

                {

                    Console.WriteLine("Неверное имя файла, попробуйте снова");
                    filePath = Console.ReadLine();

                    continue;

                }

                catch (UnauthorizedAccessException)

                {

                    Console.WriteLine("Неверное имя файла, попробуйте еще раз!");
                    filePath = Console.ReadLine();

                    continue;

                }

                FileInfo infileInfo = new FileInfo(filePath);
                bool check = true;
                if (infileInfo.Length != 0)
                { 
                    Console.WriteLine("Файл уже существует, вы хотите перезаписать его?\n1.Да\n2.Нет");
                    while (true)
                    {
                        keyboardSymbol = Console.ReadKey(true);
                        if (keyboardSymbol.Key == ConsoleKey.D1 || keyboardSymbol.Key == ConsoleKey.NumPad1)
                        {
                            
                            break;
                        }
                        else if (keyboardSymbol.Key == ConsoleKey.D2 || keyboardSymbol.Key == ConsoleKey.NumPad2)
                        {
                            check = false;
                            break;
                        }
                    }
                }
                if (!check)
                {
                    continue;
                }
                else
                    break;
       
            }
            return filePath;

        }
    
        public void ViewResult() {
            result = Calc.AverageValue(arr);
            if (result != -1.0) { 
                Console.WriteLine("Среднее арифметическое исходного массива: " + result);
                Output();
            }
            
            ExitProgram();
        }
        public void ExitProgram()
        {
            ConsoleKeyInfo continueSymbol;
            Console.WriteLine("Для выхода из программы нажмите ESC. Для продложения работы с программой нажмите Enter.");
            while (true)
            {
                continueSymbol = Console.ReadKey(true);
                if (continueSymbol.Key == ConsoleKey.Escape)
                {
                    break;
                }
                else if (continueSymbol.Key == ConsoleKey.Enter)
                {
                    UserChoice();
                    ViewResult();
                    break;
                }

            }
        }
    }
}
