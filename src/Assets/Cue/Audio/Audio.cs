

public class Audio : Cue
{
    public string referenceId { get; set; }

    public override void generate()
    {
        GameManager.instance.generateCueInScene.generateAudio(this);
    }
}
