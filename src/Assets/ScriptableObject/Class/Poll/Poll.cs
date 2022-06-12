using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cues.Data
{
    public class Poll : Cue
    {
        private readonly PollInfo pollInfo;
        private readonly List<Question> questions;
    }
}