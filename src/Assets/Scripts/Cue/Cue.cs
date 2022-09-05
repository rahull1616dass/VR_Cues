using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Cue
{
    public List<Trigger> triggers;

    public abstract void generate();
    public Cue(JToken jsonTriggers)
    {
        triggers = new List<Trigger>();
        foreach (JObject trigger in jsonTriggers)
        {
            triggers.Add(new Trigger(initTriggerPoint(trigger["_startPoint"]), initTriggerPoint(trigger["_endPoint"])));
        }
    }

    private TriggerPoint initTriggerPoint(JToken triggerPoint) {
        Debug.Log(triggerPoint.GetType());
        var tempTriggerPoint = triggerPoint as float;
        if(triggerPoint is float f)
        {
            return new TriggerPoint((float)triggerPoint);
        }
        return null;
        /*if (triggerPoint.GetType() == typeof(CueTransform))
        {
            
            return new TriggerPoint((CueTransform)triggerPoint);
        }*/
    }
}



/*
 * 
 *     public Transform transformDataForCue { get; set; }

    public Transform triggerPointForCue { get; set; }

    public TriggerType typeOfTrigger;
 * public enum TriggerType
{
    TimeTrigger = 0,
    PositionTrigger
}
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

