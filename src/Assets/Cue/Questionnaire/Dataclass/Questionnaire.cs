
using Newtonsoft.Json;
using UnityEngine;

public class Questionnaire : Cue
{
    private QInfo qInfo { get; set; }
    private Question[] questions { get; set; }

    public override void generate()
    {
        GenerateCueInScene.instance.generateQuestionnaire(this);

    }

}
