using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LogHelper
{
    private static string fileName = "";
    public static void CreateLogTxtFile()
    {
        fileName = $"{Application.streamingAssetsPath}/logs/{DateTime.Now.ToString("yyMMdd_HHmmss")}.txt";
    }


    public static void WriteLog(string log)
    {
        TextWriter textWriter = new StreamWriter(fileName, true);
        textWriter.WriteLine($"{DateTime.Now.ToString("yyMMdd_HHmmss")}\t{log}");
        textWriter.Close();
    }
}
