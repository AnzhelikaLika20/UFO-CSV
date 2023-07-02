using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


internal class DataFrame
{
    string[][] data;
    public string[][] Data
    {
        get
        {
            return data;
        }
    }
    /// <summary>
    /// The method checks the correctness of the dataset structure, namely whether the number of columns corresponds to 11.
    /// </summary>
    /// <returns></returns>
    public static bool CheckDataFrameStructure()
    {
        if (Program.df.Data[0].Length != 11)
            return false;
        return true;
    }
    public DataFrame(string[][] dataFrame)
    {
        data = dataFrame;
    }
}

