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
        public int NumRadioButtons;
        public string QuestionnaireId;
        public string QId;
        public string QType;
        public string QInstructions;
        public string QText;
        public bool QMandatory;

        public GameObject RadioButtons;
        public JSONArray QOptions;

        private RectTransform _questionRecTest;
        private bool _isOdd;
        public List<GameObject> RadioList; //contains all radiobuttons which correspond to one question

        //qText look how many q in one file >4 deny
        public List<GameObject> CreateRadioQuestion(Question question, int numberQuestion, RectTransform questionRec)
        {
            if (question.qOptions.Length > 7)
            {
                throw new InvalidOperationException("We currently only support up to 7 options");
            }

            RadioList = new List<GameObject>();

            // generate radio and corresponding text labels on a single page
            for (int j = 0; j < question.qOptions.Length; j++)
            {
                if ((NumRadioButtons % 2) != 0)
                {
                    InitRadioButtonsHorizontal(numberQuestion, j, true); //use odd number layout
                }
                else
                {
                    InitRadioButtonsHorizontal(numberQuestion, j, false); //use even number layout
                }
            }

            return RadioList;
        }


        void InitRadioButtonsHorizontal(int numQuestions, int numOptions, bool isOdd)
        {
            // Instantiate radio prefabs
            GameObject temp = Instantiate(RadioButtons);
            temp.name = "radio_" + numOptions;

            // Set radiobutton label 
            TextMeshProUGUI text = temp.GetComponentInChildren<TextMeshProUGUI>();
            text.text = QOptions[numOptions];

            // Place in hierarchy 
            RectTransform radioRec = temp.GetComponent<RectTransform>();
            radioRec.SetParent(_questionRecTest);

            radioRec.localPosition = isOdd ? new Vector3(-190 + (numOptions * 85), 91 - (numQuestions * 92), 0) : new Vector3(-150 + (numOptions * 85), 90 - (numQuestions * 100), 0);

            radioRec.localRotation = Quaternion.identity;
            radioRec.localScale = new Vector3(radioRec.localScale.x * 0.01f, radioRec.localScale.y * 0.01f, radioRec.localScale.z * 0.01f);

            // Set radiobutton group
            Radio radioScript = temp.GetComponentInParent<Radio>();
            temp.GetComponentInChildren<Toggle>().group = radioScript.gameObject.GetComponent<ToggleGroup>();

            RadioList.Add(temp);
        }
    }
}