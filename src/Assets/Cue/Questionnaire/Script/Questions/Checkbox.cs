using System;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Checkbox.class
/// 
/// version 1.0
/// date: July 1st, 2020
/// authors: Martin Feick & Niko Kleer
/// </summary>

namespace VRQuestionnaireToolkit
{
    public class Checkbox : MonoBehaviour
    {
        public int NumCheckboxButtons;
        public bool qMandatory;

        public GameObject CheckboxButtons;
        public JSONArray QOptions;

        private RectTransform _questionRecTest;
        public List<GameObject> CheckboxList; //contains all radiobuttons which correspond to one question

        //qText look how many q in one file >4 deny
        public List<GameObject> CreateCheckboxQuestion(Question question, int numberQuestion, RectTransform questionRec)
        {
            if (numberQuestion > 7)
            {
                throw new InvalidOperationException("We currently only support up to 7 checkboxes on a single page");
            }

            CheckboxList = new List<GameObject>();
            this.qMandatory = question.qData[numberQuestion].qMandatory;

            // generate checkbox and corresponding text labels on a single page
            for (int j = 0; j < question.qOptions.Length; j++)
            {

                InitCheckBoxButtons(numberQuestion, j, questionRec);

            }
            return CheckboxList;
        }

        void InitCheckBoxButtons(int numQuestions, int numOptions, RectTransform questionRec)
        {
            // Instantiate dropdown prefabs
            GameObject temp = Instantiate(CheckboxButtons);
            temp.name = "checkbox_" + numOptions;

            // Set dropdown options (Text) ;image also possible
            TextMeshProUGUI text = temp.GetComponentInChildren<TextMeshProUGUI>();
            text.text = QOptions[numOptions];

            // Place in hierarchy 
            RectTransform checkBoxRec = temp.GetComponent<RectTransform>();
            checkBoxRec.SetParent(questionRec);
            checkBoxRec.localPosition = new Vector3(-170 + (numQuestions * 140), 60 - (numOptions * 30), 0);
            checkBoxRec.localRotation = Quaternion.identity;
            checkBoxRec.localScale = new Vector3(checkBoxRec.localScale.x * 0.01f, checkBoxRec.localScale.y * 0.01f, checkBoxRec.localScale.z * 0.01f);

            CheckboxList.Add(temp);
        }
    }
}