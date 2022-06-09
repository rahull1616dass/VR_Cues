using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CueData", menuName = "ScriptableObjects/BasicCue", order = 1)]
public class Cue : ScriptableObject
{
    public Vector3 _positionOfTheCue;
    public List<CueTrigger> _cueTriggers;
}


[System.Serializable]
public class CueTrigger
{
    public enum ETriggerType
    {
        None = -1,
        TimeTrigger,
        PositionTrigger
    }
    public ETriggerType _eTriggerType;
}
