﻿
using Cues;
using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;

public class Questionnaire : Cue
{
    public List<TimeTrigger> timeTriggers;
    public QInfo qInfo { get; set; }
    public Question[] questions { get; set; }

    public override void generate()
    {
        GenerateCueInScene.instance.generateQuestionnaire(this);
    }

}
