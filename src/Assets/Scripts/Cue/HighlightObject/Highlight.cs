
using Newtonsoft.Json.Linq;

namespace Cues
{
    public class Highlight: Cue
    {
        public string objectId { get; set; }
        public string highlightColor { get; set; }
        public float animationTime { get; set; }
        public int easeType { get; set; }
        public int loopType  { get; set; }

        public Highlight(JToken triggers) : base(triggers)
        {

        }

        public override void generate(int id)
        {
            logger._id = id;
            GameManager.instance.generateCueInScene.generateHighlight(this);
        }
    }
}
