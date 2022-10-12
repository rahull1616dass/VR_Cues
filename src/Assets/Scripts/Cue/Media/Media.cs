
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Cues
{
    public class Media : Cue
    { 
        public string audioReferenceId { get; set; }
        public bool audioShouldLoop { get; set; }
        public string imageReferenceId { get; set; }

        public Media(JToken triggers) : base(triggers)
        {

        }

        public override void generate(int id)
        {
            if (logger != null)
                logger._id = id;
            GameManager.instance.generateCueInScene.generateMedia(this);
        }
    }
}