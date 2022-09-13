
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Cues
{
    public class GhostHand : Cue
    {
        public string animationName { get; set; }
        public string handType { get; set; }

        public GhostHand(JToken triggers) : base(triggers)
        {

        }

        public override void generate()
        {
            GameManager.instance.generateCueInScene.generateGhostHand(this);
        }
    }
}