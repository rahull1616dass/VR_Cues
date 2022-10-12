using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Logger 
{
    public int _id;

    public float startTimeOffset { get; set; } = 0;
    public float endTimeOffset { get; set; }
    public bool relevantForMeasurementEngine { get; set; }
}
