using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Question
{
    public string tag { get; set; }
    public string qType { get; set; }
    public string qInstructions { get; set; }
    public QData[] qData { get; set; }
    public string[] qOptions { get; set; }
    public string[] qConditions { get; set; }
}
