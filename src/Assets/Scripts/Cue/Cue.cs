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
    public Logger logger;

    public abstract void generate(int ID);
    public Cue(JToken jsonTriggers)
    {
        _triggers = new List<Trigger>();
        if(jsonTriggers == null)
        {
            throw new Exception("The triggers are not specified for this cue. Did you forget?");
        }
        foreach (JObject trigger in jsonTriggers)
        {
            try
            {
                _triggers.Add(new Trigger(initTriggerPoint(trigger["startPoint"]), initTriggerPoint(trigger["endPoint"])));
            }
            catch (NullReferenceException) { // Assuming endPoint is not specified
                _triggers.Add(new Trigger(initTriggerPoint(trigger["startPoint"]), null));
            }
        }
    }

    private TriggerPoint initTriggerPoint(JToken triggerPoint) {
        try
        {
            return new TriggerPoint(triggerPoint.ToObject<float>());
        }catch(ArgumentException) // Trigger is either a cueTransform or undefined
        {
            try
            {
                return new TriggerPoint(triggerPoint.ToObject<CueTransform>());
            }catch(ArgumentException) // Trigger is undefined
            {
                throw new Exception("Trigger is not of type cueTransform or float");
            }
        }

    }
}


