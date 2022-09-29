using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Logger 
{
    private float startTimeOffset { get; set; } = 0;
    private float endTimeOffset { get; set; }
    private bool relevantForMeasurementEngine { get; set; }
}
