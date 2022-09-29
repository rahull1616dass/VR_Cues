using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CueData : MonoBehaviour
{
    [HideInInspector] public Logger currentLogData;

    public void AddData (Logger data)
    {
        currentLogData = data;
    }
}
