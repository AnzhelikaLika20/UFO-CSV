using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

internal class UFO
{
    /// <summary>
    /// Due to the long processing of large files, the user enters two numbers
    /// (the maximum number of lines allowed for output to the console, the maximum number of lines allowed for output to the file).
    /// The method checks their correctness.
    /// </summary>
    /// <param name="consoleLines">The maximum number of lines allowed for output to the console</param>
    /// <param name="fileLines">The maximum number of lines allowed for output to the file</param>
    /// <param name="path">The path of output file</param>
    static public void OutputFormat(ref int consoleLines, ref int fileLines, string path)
    {
        Console.WriteLine("Из-за большого объема файла, вывод данных в консоль и запись в файл может занять много времени.\n" +
            $"Введите в отдельных строках 2 целых неотрицательных числа: максимальное число строк, которые можно вывести в консоль и записать в файл по адресу {path}\n" +
            $"Каждое число в диапозоне от 0 до {Program.df.Data.Length}");
        bool check1 = true, check2 = true, test = true;
        do
        {
            check1 = int.TryParse(Console.ReadLine(), out consoleLines);
            check2 = int.TryParse(Console.ReadLine(), out fileLines);
            test = !check1 || !check2 || consoleLines < 0 || fileLines < 0
                    || consoleLines > Program.df.Data.Length || fileLines > Program.df.Data.Length;
            if (test)
            {
                Console.WriteLine("Введены некорректные данные. Повторите ввод двух чисел");
            }
        }
        while (test);
    }
    /// <summary>
    /// The method proccesses dataset, if the second item of menu was called.
    /// </summary>
    static public void UFOShapeTime()
    {
        string path = FileWork.CreateShapeTimePath();
        File.WriteAllText(path, String.Join(",", FileWork.titles) + "\n");
        int fileLines = 0, consoleLines = 0;
        OutputFormat(ref consoleLines, ref fileLines, path);
        int cntConsoleLines = 0, cntFileLines = 0;
        for (int i = 0; i < Program.df.Data.Length; i++)
        {
            bool check_duration = int.TryParse(Program.df.Data[i][5], out int duration);
            if (check_duration && Program.df.Data[i][4] == "triangle" && duration > 1000)
            {
                if (cntConsoleLines < consoleLines)
                {
                    for (int j = 0; j < Program.df.Data[i].Length; j++)
                        Console.Write(Program.df.Data[i][j] + " ");
                    cntConsoleLines++;
                    Console.WriteLine();
                }
                if (cntFileLines < fileLines)
                {

                    File.AppendAllText(path, String.Join(",", Program.df.Data[i]));
                    File.AppendAllText(path, "\n");
                    cntFileLines++;
                }
                if (cntFileLines == fileLines && cntConsoleLines == consoleLines)
                    break;
            }
        }
    }
    /// <summary>
    /// The method proccesses dataset, if the third item of menu was called.
    /// </summary>
    static public void GroupUFO()
    {
        string[][] copy_df = new string[Program.df.Data.Length][];
        Array.Copy(Program.df.Data, copy_df, Program.df.Data.Length);
        System.Linq.IOrderedEnumerable<string[]> sortedDf =
            copy_df[1..].OrderBy(x => x[1]).ThenBy(x => (double.TryParse(x[9].Replace(".", ","), out double d) ? d : -10000));///As there are typos in the dataset, the erroneous data that cannot be converted to a number will be replaced
                                                                                                                              ///with -1000 and in the sorted array will go at the beginning
        string path = FileWork.CreateGroupedUFO();
        int fileLines = 0, consoleLines = 0;
        OutputFormat(ref consoleLines, ref fileLines, path);
        int cntConsoleLines = 0, cntFileLines = 0;
        File.WriteAllText(path, String.Join(",", FileWork.titles) + "\n");
        DateTime dt = new DateTime();
        foreach (string[] s in sortedDf)
        {
            try
            {
                dt = DateTime.Parse(s[0]);
            }
            catch (Exception ex)
            {
                continue;
            }
            if (dt.DayOfWeek != DayOfWeek.Tuesday)
            {
                if (cntFileLines < fileLines)
                {

                    File.AppendAllText(path, String.Join(",", s));
                    File.AppendAllText(path, "\n");
                    cntFileLines++;
                }
                if (cntConsoleLines < consoleLines)
                {
                    for (int j = 0; j < s.Length; j++)
                        Console.Write(s[j] + " ");
                    Console.WriteLine();
                    cntConsoleLines++;
                }
                if (cntFileLines == fileLines && cntConsoleLines == consoleLines)
                    break;
            }
        }
    }
    /// <summary>
    /// The method proccesses dataset, if the fourth item of menu was called.
    /// </summary>
    static public void ScheduleUFO()
    {
        string path = FileWork.CreateScheduleUFO();
        File.WriteAllText(path, String.Join(",", FileWork.titles) + "\n");
        int fileLines = 0, consoleLines = 0;
        OutputFormat(ref consoleLines, ref fileLines, path);
        int cntConsoleLines = 0, cntFileLines = 0;
        for (int i = 1; i < Program.df.Data.Length; i++)
        {
            string dt = Program.df.Data[i][0];
            int hour = int.Parse(Program.df.Data[i][0].Substring(Program.df.Data[i][0].Length - 5, 2));
            int minutes = int.Parse(Program.df.Data[i][0].Substring(Program.df.Data[i][0].Length - 2, 2));
            if ((hour >= 20 || hour < 6 || hour == 6 && minutes <= 30) &&
                (Program.df.Data[i][4] == "triangle" || Program.df.Data[i][4] == "circle" || Program.df.Data[i][4] == "cylinder"))
            {
                if (cntConsoleLines < consoleLines)
                {
                    for (int j = 0; j < Program.df.Data[i].Length; j++)
                        Console.Write(Program.df.Data[i][j] + " ");
                    Console.WriteLine();
                    cntConsoleLines++;
                }
                if (cntFileLines < fileLines)
                {
                    File.AppendAllText(path, String.Join(",", Program.df.Data[i]));
                    File.AppendAllText(path, "\n");
                    cntFileLines++;
                }
                if (cntFileLines == fileLines && cntConsoleLines == consoleLines)
                    break;
            }
        }
    }
    /// <summary>
    /// The method proccesses dataset, if the fifes item of menu was called.
    /// </summary>
    static public void Statistics()
    {
        double cnt = 0;
        Console.WriteLine("Общее количество записей о НЛО: " + $"{Program.df.Data.Length - 1}");
        Console.WriteLine();
        Dictionary<string, int> shapes = new Dictionary<string, int>();
        for (int i = 1; i < Program.df.Data.Length; i++)
        {
            if (!shapes.ContainsKey(Program.df.Data[i][4]))
                shapes.Add(Program.df.Data[i][4], 1);
            else
                shapes[Program.df.Data[i][4]]++;
        }
        Console.WriteLine("Статистику по формам НЛО в процентном отношении: ");
        foreach (var shape in shapes)
        {
            Console.WriteLine($"{shape.Key}: {shape.Value / (double)(Program.df.Data.Length - 1) * 100:f5}%");
        }
        Console.WriteLine();
        System.Linq.IOrderedEnumerable<string[]> sortedDf = Program.df.Data[1..].OrderBy(x => (double.TryParse(x[9].Replace(".", ","), out double d) ? d : -10000));
        int halfSizeDf = Program.df.Data.Length / 2;
        int medIndex = Program.df.Data.Length / 2 + 1;
        if (Program.df.Data.Length % 2 == 0)
            Console.WriteLine($"Медианное значение широт: {sortedDf.ElementAt(halfSizeDf)[9]}");
        else
            Console.WriteLine($"Медианное значение широт: {(double.Parse(sortedDf.ElementAt(halfSizeDf)[9].Replace('.', ',')) + double.Parse(sortedDf.ElementAt(medIndex)[9].Replace('.', ','))) / 2}");
        Console.WriteLine();
    }
}
