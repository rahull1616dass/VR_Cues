using UnityEngine.UI; 
using UnityEngine;

public class AssignCameraToCanvas : MonoBehaviour
{
    private void OnEnable()
    {
        if (gameObject.GetComponent<Canvas>() != null)
            gameObject.GetComponent<Canvas>().worldCamera = Camera.main;
    }
}
