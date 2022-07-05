using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cues.Data
{
    public class Questionnaire : Cue
    {
        private readonly QuestionnaireInfo questionnaireInfo;
        private readonly List<Question> questions;
    }
}