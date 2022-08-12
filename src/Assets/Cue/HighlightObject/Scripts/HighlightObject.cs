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


    public void StartHighlight(Color selectedColorArg, float animationTimeArg = 1f)
    {
        material = GetComponent<MeshRenderer>().material;
        controller = FindObjectOfType<HighlightController>();

        normalColor = material.color;
        selectedColor = selectedColorArg;
        animationTime = animationTimeArg;

        
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
