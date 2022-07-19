using System;
using System.Collections.Generic;
using SimpleJSON;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Toggle = UnityEngine.UI.Toggle;

/// <summary>
/// Radio.class
/// 
/// version 1.0
/// date: July 1st, 2020
/// authors: Martin Feick & Niko Kleer
/// </summary>

namespace VRQuestionnaireToolkit
{
    public class Radio : MonoBehaviour
    {
        public bool QMandatory;

        public GameObject RadioButtons;
        public List<GameObject> RadioList; //contains all radiobuttons which correspond to one question

        //qText look how many q in one file >4 deny
        public List<GameObject> CreateRadioQuestion(string[] qOptions, int subQuestionIndex, RectTransform questionRec)
        {
            if (qOptions.Length > 7)
            {
                throw new InvalidOperationException("We currently only support up to 7 options for any radio question.");
            }

            RadioList = new List<GameObject>();
            bool isEven = qOptions.Length % 2 == 0;

            // generate radio and corresponding text labels on a single page
            foreach (var (optionText, optionIndex) in qOptions.WithIndex()) 
            {
                // Instantiate radio prefabs
                GameObject temp = Instantiate(RadioButtons);
                temp.name = "radio_" + optionIndex;

                // Set radiobutton label 
                TextMeshProUGUI text = temp.GetComponentInChildren<TextMeshProUGUI>();
                text.text = optionText;

                // Place in hierarchy 
                RectTransform radioRec = temp.GetComponent<RectTransform>();
                radioRec.SetParent(questionRec);

                radioRec.localPosition = isEven ? 
                    new Vector3(-150 + (optionIndex * 85), 90 - (subQuestionIndex * 100), 0) : 
                    new Vector3(-190 + (optionIndex * 85), 91 - (subQuestionIndex * 92), 0);

                radioRec.localRotation = Quaternion.identity;
                radioRec.localScale = new Vector3(radioRec.localScale.x * 0.01f, radioRec.localScale.y * 0.01f, radioRec.localScale.z * 0.01f);

                // Set radiobutton group
                Radio radioScript = temp.GetComponentInParent<Radio>();
                temp.GetComponentInChildren<Toggle>().group = radioScript.gameObject.GetComponent<ToggleGroup>();

                RadioList.Add(temp);
            }

            return RadioList;
        }
    }
}