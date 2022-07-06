using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Question
{
    public string qType { get; set; }
    public string qInstructions { get; set; }
    public QData[] qData { get; set; }
    public string[] qOptions { get; set; }
}
