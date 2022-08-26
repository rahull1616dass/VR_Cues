
using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;

namespace Cues
{
    public class Media : Cue
    {
        public CueTransform cueTransform { get; set; }
        public string audioReferenceId { get; set; }
        public bool audioShouldLoop { get; set; }
        public string imageReferenceId { get; set; }

        public override void generate()
        {
            GameManager.instance.generateCueInScene.generateMedia(this);
        }
    }
}