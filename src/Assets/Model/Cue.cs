using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Cue
{
    public List<Cue> cues { get; set; }
    public string cueType { get; set; }
}
/*
[CreateAssetMenu(fileName = "CueData", menuName = "ScriptableObjects/BasicCue", order = 1)]
public class Cue : ScriptableObject
{
    public PositionData _positionData;
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

    public float _eTriggerTime;

    public Vector3 _eTriggerPosition;

    public Vector3 _eTriggerSize;
}

[System.Serializable]
public class PositionData
{
    public Transform _cueTransform;
    public Transform _parent;
}*/

