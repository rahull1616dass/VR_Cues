
using Cues;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Questionnaire : Cue
{
    public CueTransform cueTransform { get; set; }
    public QInfo qInfo { get; set; }
    public Question[] questions { get; set; }

    public Questionnaire(JToken triggers) : base(triggers)
    {
        
    }
    
    public override void generate()
    {
        GameManager.instance.generateCueInScene.generateQuestionnaire(this);
       
    }

}
