using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Обработка_ini_файлов
{
    class IniParser : IIniParser
    {
        public string File_path { get; set; }
        public List<Section> Sections { get; set; }

        public IniParser(string file_path)
        {
            File_path = file_path;
            Sections = new List<Section>();
        }

        public void Show()
        {
            try
            {
                Console.WriteLine();
                Console.WriteLine("Содержимое файла:");
                Console.WriteLine();

                using (StreamReader read = new StreamReader(File_path))
                {
                    while (!read.EndOfStream)
                    {
                        Console.WriteLine(read.ReadLine());
                    }
                    read.Close();
                }
            }
            catch
            {
                throw new Exception("Файл не найден, возможно он перемещён или удалён");
            }
        }

        public static string RemoveExtraSpaces(string astring) // функция удаления лишних пробелов
        {
            if (!string.IsNullOrEmpty(astring))
            {
                bool found = false;
                StringBuilder buff = new StringBuilder();

                foreach (char chr in astring.Trim())
                {
                    if (char.IsWhiteSpace(chr))
                    {
                        if (found)
                        {
                            continue;
                        }

                        found = true;
                        buff.Append(' ');
                    }
                    else
                    {
                        if (found)
                        {
                            found = false;
                        }

                        buff.Append(chr);
                    }
                }

                return buff.ToString();
            }

            return string.Empty;
        }

        public void Read()
        {
            List<string> str = new List<string>(); // массив строк из файла
            int pos1, pos2; //позиции [ and ]
            int pos11; //позиция = or ;
            int section_id = -1;
            int parameter_id = -1;

            try
            {
                using (StreamReader read = new StreamReader(File_path))
                {
                    while (!read.EndOfStream)
                    {
                        string s = read.ReadLine();
                        
                        pos11 = s.IndexOf(';');
                        if (pos11 != -1) // очистка комментария
                        {
                            s = s.Substring(0, pos11);
                        }

                        s = RemoveExtraSpaces(s); // удаляем лишние пробелы

                        str.Add(s);
                    }
                    read.Close();
                }

                str.RemoveAll(s => string.IsNullOrWhiteSpace(s)); // удаляем пустые строки или состоящие только из пробелов

                //Console.WriteLine();
                //Console.WriteLine("Очищенные данные из файла и сохраненные в список строк:");
                //foreach (var s in str)
                //{
                //    Console.WriteLine(s);
                //}

                foreach (var s in str)
                {
                    pos1 = s.IndexOf('[');
                    pos2 = s.IndexOf(']');
                    if ((pos1 != -1) & (pos2 != -1) & (pos1 < pos2))
                    {
                        section_id += 1;
                        Sections.Add(new Section(s.Substring(pos1 + 1, pos2 - 1)));
                        parameter_id = -1;
                    }

                    pos11 = s.IndexOf('=');
                    if (pos11 != -1)
                    {
                        if (section_id == -1)
                        {
                            section_id += 1;
                            Sections.Add(new Section("WITHOUTASECTION"));
                            parameter_id = -1;
                        }
                        parameter_id += 1;
                        Sections[section_id].Parameters.Add(new Parameter());
                        Sections[section_id].Parameters[parameter_id].Name = s.Substring(0, pos11 - 1).Trim();
                        Sections[section_id].Parameters[parameter_id].Value = s.Substring(pos11 + 1, (s.Length - pos11 - 1)).Trim();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ошибка: {e.Message}");
            }
        }

        public void ShowВata()
        {
            if (Sections.Count == 0)
            {
                Console.WriteLine();
                Console.WriteLine("Данных нет!");
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("Полученные данные:");

                for (int i = 0; i < Sections.Count; i++)
                {
                    Console.WriteLine();
                    Console.WriteLine(Sections[i].Name);
                    foreach (Parameter param in Sections[i].Parameters)
                    {
                        Console.WriteLine("          " + param.Name + " = " + param.Value);
                    }
                }
            }
        }

        public void Receiving(int tip, int section_id, int parameter_id)
        {
            try
            {
                Console.WriteLine();
                Console.WriteLine(" Результат выполнения операции:");
                Console.WriteLine();

                if (tip == 1)
                {
                    Console.WriteLine("   Секция:  " + Sections[section_id].Name + "  Параметр:  " + Sections[section_id].Parameters[parameter_id].Name
                    + "  Значение:  " + Sections[section_id].Parameters[parameter_id].Value);
                }
                else if (tip == 2)
                {
                    int stringToInt = Convert.ToInt32(Sections[section_id].Parameters[parameter_id].Value);
                    Console.WriteLine("   Секция:  " + Sections[section_id].Name + "  Параметр:  " + Sections[section_id].Parameters[parameter_id].Name
                    + "  Значение:  " + stringToInt);
                }
                else if (tip == 3)
                {
                    double stringToDoub = double.Parse(Sections[section_id].Parameters[parameter_id].Value, CultureInfo.InvariantCulture.NumberFormat);
                    Console.WriteLine("   Секция:  " + Sections[section_id].Name + "  Параметр:  " + Sections[section_id].Parameters[parameter_id].Name
                    + "  Значение:  " + stringToDoub);
                }
                Console.WriteLine();
                Console.WriteLine();
            }
            catch (Exception e)
            {
                Console.WriteLine($" Ошибка (неверный тип параметра): {e.Message}");
            }
        }
    }
}
