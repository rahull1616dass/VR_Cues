using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class HighlightObject : MonoBehaviour {

    public int objectID;
    private float animationTime = 1f;
    //public float threshold = 10.5f;

    private HighlightController controller;
    private Material material;
    private Color normalColor;
    private Color selectedColor;

    private iTween.EaseType easeType;
    private iTween.LoopType loopType;


    public void StartHighlight(Color selectedColorArg, float animationTimeArg, iTween.EaseType ease, iTween.LoopType loop) { 
    
        material = GetComponent<MeshRenderer>().material;
        controller = FindObjectOfType<HighlightController>();

        normalColor = material.color;
        selectedColor = selectedColorArg;
        animationTime = animationTimeArg;

        easeType = ease;
        loopType = loop;
    }



    public void StartHighlight() {
        iTween.ColorTo(gameObject, iTween.Hash(
            "color", selectedColor,
            "time", animationTime,
            "easetype", iTween.EaseType.linear,
            "looptype", iTween.LoopType.pingPong
        ));
    }

    public void StopHighlight() {
        iTween.Stop(gameObject);
        material.color = normalColor;
    }

    private void OnMouseDown() {
        controller.SelectObject(this);
    }
}
