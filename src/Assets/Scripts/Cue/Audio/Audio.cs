

public class Audio : Cue
{
    public string referenceId { get; set; }
    public bool shouldLoop { get; set; }
    public CueTransform? cueTransform { get; set; }

     public Audio(CueTransform cueTransform, string referenceId, bool shouldLoop)
    {
        this.referenceId = referenceId;
        this.shouldLoop = shouldLoop;
        this.cueTransform = cueTransform;
    }

    public override void generate()
    {
        GameManager.instance.generateCueInScene.generateAudio(this);
    }
}
