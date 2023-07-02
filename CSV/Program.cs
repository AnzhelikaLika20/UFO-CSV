using System;
using System.IO;

internal class Program
{
    public static DataFrame df = null;
    public static bool flag = true;
    static void Main(string[] args)
    {
        Menu menu = new Menu();
        menu.PrintMenu();
        do
        {
            try
            {
                menu.GetProccessValue();
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine();
            }
        }
        while(flag);
    }
}