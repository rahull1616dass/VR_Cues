

using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;

namespace Cues
{
    public class Image: Cue
    {
        public CueTransform cueTransform { get; set; }
        /*List<Transform> positionalTriggers;
        List<TimeTrigger> timeTriggers;*/

        public string text { get; set; }
        public string referenceId { get; set; }

        public override void generate()
        {
            GameManager.instance.generateCueInScene.generateImage(this);
        }
    }
}

