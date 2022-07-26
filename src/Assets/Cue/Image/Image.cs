

using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;

namespace Cues
{
    public class Image: Cue
    {
        CueTransform cueTransform;
        /*List<Transform> positionalTriggers;
        List<TimeTrigger> timeTriggers;*/

        public string text { get; set; }

        public override void generate()
        {
            throw new System.NotImplementedException();
        }
    }
}

