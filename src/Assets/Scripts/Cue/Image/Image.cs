

using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;

namespace Cues
{
    public class Image
    {
        public CueTransform cueTransform { get; set; }
        /*List<Transform> positionalTriggers;
        List<TimeTrigger> timeTriggers;*/
        public string referenceId { get; set; }


        public Image(CueTransform cueTransform, string referenceId)
        {
            this.cueTransform = cueTransform;
            this.referenceId = referenceId;
        }
    }
}

