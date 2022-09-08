using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Cue
{
    public List<Trigger> _triggers;
    public CueTransform cueTransform;

    public abstract void generate();
    public Cue(JToken jsonTriggers)
    {
        _triggers = new List<Trigger>();
        foreach (JObject trigger in jsonTriggers)
        {
            _triggers.Add(new Trigger(initTriggerPoint(trigger["startPoint"]), initTriggerPoint(trigger["endPoint"])));
        }
    }

    private TriggerPoint initTriggerPoint(JToken triggerPoint) {
        try
        {
            return new TriggerPoint(triggerPoint.ToObject<float>());
        }catch(ArgumentException)
        {
            try
            {
                return new TriggerPoint(triggerPoint.ToObject<CueTransform>());
            }catch(ArgumentException)
            {
                throw new Exception("Trigger is not of type cueTransform or float");
            }
        }

    }
}


