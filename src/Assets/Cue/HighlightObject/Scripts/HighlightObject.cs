using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class HighlightObject : MonoBehaviour {

    public string objectID;
    private float animationTime = 1f;
    //public float threshold = 10.5f;

    private HighlightController controller;
    private Material material;
    private Color normalColor;
    private Color selectedColor;

    private iTween.EaseType easeType;
    private iTween.LoopType loopType;


    public void initHighlight(string hexColor, float animationTimeArg, iTween.EaseType ease, iTween.LoopType loop) { 
    
        material = GetComponent<MeshRenderer>().material;
        controller = FindObjectOfType<HighlightController>();

        normalColor = material.color;
        ColorUtility.TryParseHtmlString(hexColor, out selectedColor);
        animationTime = animationTimeArg;

        easeType = ease;
        loopType = loop;
    }



    public void StartHighlight() {
        iTween.ColorTo(gameObject, iTween.Hash(
            "color", selectedColor,
            "time", animationTime,
            "easetype", easeType,
            "looptype", loopType
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
