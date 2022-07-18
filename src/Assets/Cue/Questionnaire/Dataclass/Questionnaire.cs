
using Cues;
using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;

public class Questionnaire : Cue
{
    List<TimeTrigger> timeTriggers;
    private QInfo qInfo { get; set; }
    private Question[] questions { get; set; }

    public override void generate()
    {
        GenerateCueInScene.instance.generateQuestionnaire(this);

    }

}
