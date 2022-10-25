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
        fileName = $"{Application.streamingAssetsPath}/logs/{DateTime.Now.ToString("yyMMdd_HHmmss")}.csv";
        WriteLog("Timestamp;CueId;StartTimeOffset;EndTimeOffset;RelevantForMeasurementEngine;StartTriggerPosition;EndTriggerPosition;TimeOfInteraction", false);
    }

    public static void WriteLog(string log, bool includeTime = true)
    {
        TextWriter textWriter = new StreamWriter(fileName, true);
        if(includeTime)
            textWriter.WriteLine($"{DateTime.Now.ToString("HH:mm:ss")};{log}");
        else
            textWriter.WriteLine(log);      
        textWriter.Close();
    }
}
