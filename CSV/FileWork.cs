using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// The class works with files, which contains source data.
/// </summary>
internal class FileWork
{
    static string path;
    static public string[] titles = new string[1];
    public FileWork(string path1)
    {
        path = path1;
    }

    public string Path
    {
        get
        {
            return path;
        }
    }
    /// <summary>
    /// The method checks correctness of file's path.
    /// </summary>
    /// <returns></returns>
    public bool CheckPath()
    {
        if (!(File.Exists(path) && System.IO.Path.GetExtension(path) == ".csv"))
        {
            Console.WriteLine("Введенный адрес некорректен, это адрес несуществующего файла или файл не формата .csv");
            Console.WriteLine();
            return false;
        }
        return true;
    }
    /// <summary>
    /// The method check encoding of file.
    /// </summary>
    /// <returns></returns>
    public bool CheckEncoding()
    {
        Stream fs = new FileStream(path, FileMode.Open);
        using (StreamReader r = new StreamReader(fs, true))
        {
            if (r.CurrentEncoding != Encoding.UTF8)
            {
                return false;
            }
        }
        return true;
    }
    /// <summary>
    /// The method creates dataframe.
    /// </summary>
    /// <returns></returns>
    public DataFrame CreateDataFrame()
    {
        string[][] dataFrame = null;
        string[] lines = null;
        lines = File.ReadAllLines(path);
        dataFrame = new string[lines.Length][];
        for (int i = 0; i < dataFrame.Length; i++)
        {
            dataFrame[i] = lines[i].Split(',');
        }
        titles = dataFrame[0];
        Console.WriteLine("Данные загружены из файла");
        Console.WriteLine();
        return new DataFrame(dataFrame);
    }
    /// <summary>
    /// The method creates the file's path for saving data of second menu item.
    /// </summary>
    /// <returns></returns>
    public static string CreateShapeTimePath()
    {
        string fileName = System.IO.Path.GetFileName(path);
        string path_UFO_Shape_Time = path.Replace(fileName, "UFO-Shape-Time.csv");
        return path_UFO_Shape_Time;
    }
    /// <summary>
    /// The method creates the file's path for saving data of third menu item.
    /// </summary>
    /// <returns></returns>
    public static string CreateScheduleUFO()
    {
        string fileName = System.IO.Path.GetFileName(path);
        string path_UFOs_Schedule = path.Replace(fileName, "UFOs-Schedule.csv");
        return path_UFOs_Schedule;
    }
    /// <summary>
    /// The method creates the file's path for saving data of fourth menu item.
    /// </summary>
    /// <returns></returns>
    public static string CreateGroupedUFO()
    {
        string fileName = System.IO.Path.GetFileName(path);
        string path_UFO_Group = path.Replace(fileName, "Grouped-UFOs.csv");
        return path_UFO_Group;
    }
}
