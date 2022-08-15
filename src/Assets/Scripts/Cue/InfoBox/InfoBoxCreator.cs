using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Assets.Extensions;

public class InfoBoxCreator : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] buttonTexts;
    [SerializeField] private UnityEngine.UI.Image[] buttonImages;
    [SerializeField] private TextMeshProUGUI titleText, descriptionText;
    [SerializeField] private UnityEngine.UI.Image infoImage;
    [SerializeField] private UnityEngine.UI.Button[] buttons;

    public void CreateInfoBox(InfoBox infoBox)
    {
        if (!string.IsNullOrEmpty(infoBox.title))
        {
            titleText.text = infoBox.title;
            titleText.gameObject.SetActive(true);
        }
        if (!string.IsNullOrEmpty(infoBox.title))
        {
            descriptionText.text = infoBox.description;
            descriptionText.gameObject.SetActive(true);
        }
        if (!string.IsNullOrEmpty(infoBox.iconReferenceId))
        {
            infoImage.sprite = infoBox.iconReferenceId.createSpriteFromReferenceId();
            infoImage.gameObject.SetActive(true);
        }
        for (int i = 0; i < infoBox.buttons.Length; i++)
        {
            if (!string.IsNullOrEmpty(infoBox.buttons[i].text))
            {
                buttonTexts[i].text = infoBox.buttons[i].text;
                buttonTexts[i].gameObject.SetActive(true);
            }
            _ = buttonImages[i].color;
            ColorUtility.TryParseHtmlString(infoBox.buttons[i].buttonBackgroundColor, out Color tempColor);
            buttonImages[i].color = tempColor;

            _ = buttonTexts[i].color;
            ColorUtility.TryParseHtmlString(infoBox.buttons[i].textColor, out tempColor);
            buttonTexts[i].color = tempColor;

            string feedback = "user feedback to title " + infoBox.title + " \n Description: " + infoBox.description + "\n is " + infoBox.buttons[i].text;
            buttons[i].onClick.AddListener(() => { OnButtonClick(feedback); });
        }
    }

    private void OnButtonClick(string feedback)
    {
        LogHelper.WriteLog(feedback);
        Destroy(gameObject);
    }
}

