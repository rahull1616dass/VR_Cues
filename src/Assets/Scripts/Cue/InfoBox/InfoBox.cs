
public class InfoBox : Cue
{
    public CueTransform cueTransform { get; set; }
    public string title { get; set; }
    public string description { get; set; }

    public string textColor { get; set; }
    public Button[] buttons { get; set; }
    public string iconReferenceId { get; set; }

    public override void generate()
    {
        GameManager.instance.generateCueInScene.generateInfoBox(this);
    }
}
