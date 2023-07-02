using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// The class is responsible for the operation of the menu.
/// </summary>
internal class Menu
{
    int menuItem = 0;
    bool flag = false;
    /// <summary>
    /// Method prints suggested menu.
    /// </summary>
    public void PrintMenu()
    {
        Console.WriteLine("МЕНЮ:" + "\n\r" +
                    "Введите '1', чтобы ввести адрес файла, из которого нужно загрузить данные" + "\n" +
                    "Введите '2', чтобы вывести на экран информацию о всех датах,\r\n\tв которые видели НЛО треугольной формы более 1000 секунд" + "\n" +
                    "Введите '3', чтобы вывести на экран исходный набор данных об НЛО,\r\n\tсгруппированный по столбцу city,\r\n\tпри этом в каждой группе стоит упорядочить записи по возрастанию широты,\r\n\tа также исключить все записи, сделанные во вторник" + "\n" +
                    "Введите '4', чтобы вывести на экран информацию о всех записях,\r\n\tв которых НЛО треугольной, цилиндрической или круглой формы видели в период между 20:00 и 6:30" + "\n" +
                    "Введите '5', чтобы вывести сводную статистику по данным загруженного файла\r\n" +
                    "Введите '-1', чтобы завершить программу\r\n");
        Console.WriteLine();
    }
    /// <summary>
    /// Method is responsible for getting value of menu item and proccessing it. 
    /// </summary>
    public void GetProccessValue()
    {
        Console.WriteLine("Введите -1 или число от 1 до 5, которое соответствует выбранному пункту меню");
        Console.WriteLine();
        bool check_number = !int.TryParse(Console.ReadLine(), out menuItem) || menuItem < 1 || menuItem > 5;
        Console.WriteLine();
        if (menuItem == -1)
        {
            Program.flag = false;
        }
        if (check_number && menuItem != -1)
        {
            Console.WriteLine("Введено некорректое значение");
            Console.WriteLine();
            menuItem = 0;
        }
        else
        {
            ProccessMenuItem(menuItem);
        }
    }
    /// <summary>
    /// Method proccesses choosed menu item.
    /// </summary>
    /// <param name="menuItem">choosen menu item</param>
    public void ProccessMenuItem(int menuItem)
    {
        if (menuItem == 1)
        {
            char directorySeparatorChar = Path.DirectorySeparatorChar; ///Character used to separate directory levels in a path string
                                                                       ///that reflects a hierarchical file system organization.
            char volumeSeparatorChar = Path.VolumeSeparatorChar;       /// Provides a platform-specific volume separator character.
            Console.WriteLine("Введите абсолютную или относительную ссылку на файл с данными\n" +
                $"Для разделения директрорий в пути файла используйте '{directorySeparatorChar}'\n" +
                $"Для отделения диска в пути файла используйте '{volumeSeparatorChar}'");
            Console.WriteLine();
            FileWork file = new FileWork(Console.ReadLine());
            Console.WriteLine();
            if (file.CheckPath())
            {
                Program.df = file.CreateDataFrame();
                flag = true;
            }
            if (!file.CheckEncoding())
            {
                Console.WriteLine("Неверная кодировка файла");
                Console.WriteLine();
                return;
            }
            if(!DataFrame.CheckDataFrameStructure())
            {
                Console.WriteLine("Структура данных в файле не корректна");
                return;
            }
        }
        else if(menuItem == 2)
        {
            if(!flag)
            {
                Console.WriteLine("Не указана ссылка на файл с данными. Вызовите пункт меню '1' и задайте путь к файлу с данными.");
                Console.WriteLine();
                return;
            }
            UFO.UFOShapeTime();
            Console.WriteLine();
        }
        else if( menuItem == 3)
        {
            if (!flag)
            {
                Console.WriteLine("Не указана ссылка на файл с данными. Вызовите пункт меню '1' и задайте путь к файлу с данными.");
                Console.WriteLine();
                return;
            }
            UFO.GroupUFO();
            Console.WriteLine();
        }
        else if(menuItem == 4)
        {
            if (!flag)
            {
                Console.WriteLine("Не указана ссылка на файл с данными. Вызовите пункт меню '1' и задайте путь к файлу с данными.");
                Console.WriteLine();
                return;
            }
            UFO.ScheduleUFO();
            Console.WriteLine();
        }
        else if(menuItem == 5)
        {
            if (!flag)
            {
                Console.WriteLine("Не указана ссылка на файл с данными. Вызовите пункт меню '1' и задайте путь к файлу с данными.");
                Console.WriteLine();
                return;
            }
            UFO.Statistics();
            Console.WriteLine();
        }
    }
}
