using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "animationData", menuName = "SO/animationData")]
public class AnimationLayerClass : ScriptableObject
{
    [SerializeField] private List<AniamtionLayers> animationLayers;

    public int getAnimationLayerByName(string animationName)
    {
        foreach (AniamtionLayers layer in animationLayers)
            if(layer.layerName == animationName)
                return layer.layerIndex;
        return -1;
    }

}

[System.Serializable]
public class AniamtionLayers
{
    public string layerName;
    public int layerIndex;
}
