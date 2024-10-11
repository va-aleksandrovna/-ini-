using System;
using System.IO;

namespace Обработка_ini_файлов
{
    class Program
    {
        static void Main(string[] args)
        {
            string path; //путь к файлу
            string ending; //расширение файла
            string result; //результат проверки файла
            bool showMenu = true; // показ меню
            int section_id;
            int parameter_id;
            int type_id;

            do
            {
                Console.WriteLine(" Введите путь к целевому файлу");

                path = Console.ReadLine(); // C:\Users\DNS\Desktop\primer.ini

                ending = Path.GetExtension(path); //узнаем расширение файла

                if (File.Exists(path))
                {
                    if (ending == ".ini")
                    {
                        result = "Success";
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine(" Файл имеет неверный формат");
                        Console.WriteLine(" Попробуйте снова");
                        Console.WriteLine();
                        result = "Fail";
                    }
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine(" Файл не найден");
                    Console.WriteLine(" Попробуйте снова");
                    Console.WriteLine();
                    result = "Fail";
                }
            } while (result != "Success");

            IIniParser parser = new IniParser(path);

            parser.Read(); //обработка файла

            Console.Clear(); // очистка консоли
            Console.WriteLine(" МЕНЮ:");
            Console.WriteLine(" 'show' или '1' - посмотреть содержимое файла;");
            Console.WriteLine(" 'showdata' или '2' - посмотреть данные полученные после обработки файла;");
            Console.WriteLine(" 'get' или '3' - получить значение параметра по названию секции и имени параметра;");
            Console.WriteLine(" 'clear' или '4' -  очистить консоль;");
            Console.WriteLine(" 'exit' или '5' - выход из программы;");

            while (showMenu)
            {
                Console.Write("\r\n Выберите команду меню ('menu' - для вызова меню): ");

                switch (Console.ReadLine())
                {
                    case "menu":
                        Console.WriteLine();
                        Console.WriteLine(" МЕНЮ:");
                        Console.WriteLine(" 'show' или '1' - посмотреть содержимое файла;");
                        Console.WriteLine(" 'showdata' или '2' - посмотреть данные полученные после обработки файла;");
                        Console.WriteLine(" 'get' или '3' - получить значение параметра по названию секции и имени параметра;");
                        Console.WriteLine(" 'clear' или '4' -  очистить консоль;");
                        Console.WriteLine(" 'exit' или '5' - выход из программы;");
                        showMenu = true;
                        break;
                    case "show" or "1":
                        parser.Show(); //вывод файла в консоль
                        showMenu = true;
                        break;
                    case "showdata" or "2":
                        parser.ShowВata(); //полученные данные
                        showMenu = true;
                        break;
                    case "get" or "3":
                        Console.WriteLine();
                        Console.WriteLine(" Введите номер секции: ");
                        int i = 1;
                        foreach (Section sec in parser.Sections)
                        {
                            Console.Write($" {i++}.");
                            Console.WriteLine(sec.Name);
                        }
                        do
                        {
                            Console.WriteLine();
                            section_id = Convert.ToInt32(Console.ReadLine()) - 1;
                            if (section_id >= i-1)
                            {
                                Console.WriteLine("  Неверный ввод");
                            }
                        }
                        while (section_id >= i-1);

                        Console.WriteLine();
                        Console.WriteLine(" Введите номер параметра: ");
                        i = 1;
                        foreach (Parameter param in parser.Sections[section_id].Parameters)
                        {
                            Console.Write($" {i++}.");
                            Console.WriteLine(param.Name);
                        }
                        do
                        {
                            Console.WriteLine();
                            parameter_id = Convert.ToInt32(Console.ReadLine()) - 1;
                            if (parameter_id >= i-1)
                            {
                                Console.WriteLine("  Неверный ввод");
                            }
                        }
                        while (parameter_id >= i-1);

                        Console.WriteLine();
                        Console.WriteLine(" В каком виде вы хотите получить значение?");
                        Console.WriteLine("  1 - String");
                        Console.WriteLine("  2 - Int");
                        Console.WriteLine("  3 - Double");
                        do
                        {
                            Console.WriteLine();
                            type_id = Convert.ToInt32(Console.ReadLine());
                            if (!((type_id == 1) | (type_id == 2) | (type_id == 3)))
                            {
                                Console.WriteLine("  Неверный ввод");
                            }
                        } while (!((type_id == 1) | (type_id == 2) | (type_id == 3)));

                        parser.Receiving(type_id, section_id, parameter_id);
                        showMenu = true;
                        break;
                    case "clear" or "4":
                        Console.Clear(); // очистка консоли
                        showMenu = true;
                        break;
                    case "exit" or "5":
                        showMenu = false;
                        break;
                    default:
                        Console.WriteLine("  Неизвестная команда");
                        showMenu = true;
                        break;
                }
            }

            //parser.Show(); //вывод файла в консоль
            //parser.ShowВata(); //полученные данные
            //parser.Receiving(1, 3, 0);
            //parser.Receiving(2, 0, 4);
            //parser.Receiving(3, 1, 0);
            //parser.Receiving(2, 2, 1);
        }
    }
}
