using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Haptic : Cue
{
    public float amplitude { get; set; }
    public float duration { get; set; }

    public override void generate()
    {
        GameManager.instance.generateCueInScene.generateHaptic(this);
    }
}
