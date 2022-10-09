
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

public class InfoBox : Cue
{
    public string title { get; set; }
    public string description { get; set; }

    public string textColor { get; set; }
    public Button[] buttons { get; set; }
    public string iconReferenceId { get; set; }

    public InfoBox(JToken triggers) : base(triggers)
    {

    }

    public override void generate(int id)
    {
        logger._id = id;
        GameManager.instance.generateCueInScene.generateInfoBox(this);
    }
}
