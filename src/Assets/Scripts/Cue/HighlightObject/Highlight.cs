
namespace Cues
{
    public class Highlight: Cue
    {
        public string objectId { get; set; }
        public string highlightColor { get; set; }
        public float animationTime { get; set; }
        public int easeType { get; set; }
        public int loopType  { get; set; }


        public override void generate()
        {
            GameManager.instance.generateCueInScene.generateHighlight(this);
        }
    }
}
