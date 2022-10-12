using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CueData : MonoBehaviour
{
    [HideInInspector] public Logger currentLogData;

    public void AddData (Logger data, int id)
    {
        if (data == null)
            currentLogData = new Logger()
            {
                _id = id,
                startTimeOffset = 0f,
                endTimeOffset = 0f,
                relevantForMeasurementEngine = false
            };
        else
            currentLogData = data;
    }
}
