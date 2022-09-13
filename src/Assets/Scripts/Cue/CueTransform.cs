using System;
using UnityEngine;

[Serializable]
public class CueTransform
{
    public bool attachToPlayer;
    public Vector3 position;
    public Vector3 rotation;
    public Vector3 scale;

    public CueTransform(Vector3 position, Vector3 rotation, Vector3 scale, bool attachToPlayer = false)
    {
        this.attachToPlayer = attachToPlayer;
        this.position = position;
        this.rotation = rotation;
        this.scale = scale;
    }

    public CueTransform() { }
}
