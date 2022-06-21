using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSortingLayer : MonoBehaviour
{
    private void OnEnable()
    {
        if (GetComponent<Canvas>() != null)
            GetComponent<Canvas>().sortingOrder = 1;
    }
}
