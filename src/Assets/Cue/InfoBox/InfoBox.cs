
public class InfoBox : Cue
{
    public CueTransform cueTransform { get; set; }
    public string title { get; set; }
    public string description { get; set; }
    public Button[] buttons { get; set; }


    public override void generate()
    {
        throw new System.NotImplementedException();
    }
}
