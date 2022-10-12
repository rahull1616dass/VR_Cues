using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Haptic : Cue
{
    public float strength { get; set; }

    public string controller { get; set; }

    public Haptic(JToken triggers) : base(triggers)
    {

    }
    public override void generate(int id)
    {
        logger._id = id;
        GameManager.instance.generateCueInScene.generateHaptic(this);
    }
}


