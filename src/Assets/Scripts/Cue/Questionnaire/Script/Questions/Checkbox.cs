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

        private RectTransform _questionRecTest;
        public List<GameObject> CheckboxList; //contains all radiobuttons which correspond to one question


        [SerializeField] private TextMeshProUGUI questionTextbox;
        private string currentQuestion, currentAnswer;

        private int CurrentQuestionIndex;

        private bool IsAlreadyEnabled;
        //qText look how many q in one file >4 deny
        public List<GameObject> CreateCheckboxQuestion(string[] options, int questionIndex, RectTransform questionRec)
        {

            CheckboxList = new List<GameObject>();

            // generate checkbox and corresponding text labels on a single page
            for (int optionIndex = 0; optionIndex < options.Length; optionIndex++)
            {

                // Instantiate dropdown prefabs
                GameObject temp = Instantiate(CheckboxButtons);
                temp.name = "checkbox_" + optionIndex;

                // Set dropdown options (Text) ;image also possible
                TextMeshProUGUI text = temp.GetComponentInChildren<TextMeshProUGUI>();
                text.text = options[optionIndex];

                // Place in hierarchy 
                RectTransform checkBoxRec = temp.GetComponent<RectTransform>();
                checkBoxRec.SetParent(questionRec);
                checkBoxRec.localPosition = new Vector3(-170 + (questionIndex * 140), 60 - (optionIndex * 30), 0);
                checkBoxRec.localRotation = Quaternion.identity;
                checkBoxRec.localScale = new Vector3(checkBoxRec.localScale.x * 0.01f, checkBoxRec.localScale.y * 0.01f,
                    checkBoxRec.localScale.z * 0.01f);

                checkBoxRec.transform.GetChild(0).GetComponent<Toggle>().onValueChanged.AddListener((value) => OnValueChangeCheckBox(text.text, value));

                CheckboxList.Add(temp);
            }
            return CheckboxList;
        }



        public void OnValueChangeCheckBox(string answer, bool value)
        {
            if (value)
            {
                currentQuestion = questionTextbox.text;
                currentAnswer += ";" +answer;
            }
            else
            {
                currentAnswer = "";
                string[] Answers = currentAnswer.Split(';');
                for (int i = 0; i < Answers.Length; i++)
                {
                    if (Answers[i] == answer)
                        continue;
                    currentAnswer += ";" + Answers[i];
                }
            }
        }

        private void OnEnable()
        {
            //Debug.Log("CallingEnable");
            if (!IsAlreadyEnabled)
            {
                CurrentQuestionIndex = ExportToCSV.QuestionIndex;
                ExportToCSV.QuestionIndex++;
                IsAlreadyEnabled = true; 
                ExportToCSV.SaveDataWhileAnswering("", "", "", CurrentQuestionIndex);
            }
        }
        private void OnDisable()
        {
            //Debug.Log("CallingDisable");
            ExportToCSV.SaveDataWhileAnswering("CheckBox",currentQuestion, currentAnswer, CurrentQuestionIndex);
        }
    }
}