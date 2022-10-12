using Newtonsoft.Json.Linq;
using System.Collections;
using UnityEngine;

namespace Cues.Data
{
    public class Video : Cue
    {

        public Video(JToken triggers) : base(triggers)
        {

        }

        public override void generate(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}