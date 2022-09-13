
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Cues
{
    public class Animation : Cue
    {
        public CueTransform cueTransform { get; set; }
        public string animationReferenceId { get; set; }

        public Animation(JToken triggers) : base(triggers)
        {

        }

        public override void generate()
        {
            GameManager.instance.generateCueInScene.generateAnimation(this);
        }
    }
}