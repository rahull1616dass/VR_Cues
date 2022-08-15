using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InfoBoxCreator : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] buttonText;
    [SerializeField] private UnityEngine.UI.Image[] buttonImage;
    [SerializeField] private Color[] buttonColors, buttonTextColor;
    [SerializeField] private TextMeshProUGUI titleText, descriptionText;
    [SerializeField] private Color textColor;
    public void CreateInfoBox(InfoBox infoBox)
    {
        
    }
}
