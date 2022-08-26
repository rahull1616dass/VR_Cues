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

        [SerializeField] private TextMeshProUGUI questionTextbox;
        private string currentQuestion, currentAnswer;
        int CurrentQuestionIndex;

        //qText look how many q in one file >4 deny
        public List<GameObject> CreateRadioQuestion(QData subQuestion, int subQuestionIndex, RectTransform questionRec)
        {
            CurrentQuestionIndex = ExportToCSV.QuestionIndex;
            ExportToCSV.QuestionIndex++;

            if (subQuestion.qOptions.Length > 7)
            {
                throw new InvalidOperationException("We currently only support up to 7 options for any radio question.");
            }

            this.QMandatory = subQuestion.qMandatory;

            RadioList = new List<GameObject>();
            bool isEven = subQuestion.qOptions.Length % 2 == 0;

            // generate radio and corresponding text labels on a single page
            foreach (var (optionText, optionIndex) in subQuestion.qOptions.WithIndex())
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

                Toggle currentToggle = temp.GetComponentInChildren<Toggle>();
                currentToggle.group = radioScript.gameObject.GetComponent<ToggleGroup>();
                currentToggle.onValueChanged.AddListener((value) => OnValueChangeCheckBox(text.text, value));

                RadioList.Add(temp);
            }

            return RadioList;
        }

        public void OnValueChangeCheckBox(string answer, bool value)
        {
            if (value)
            {
                currentQuestion = questionTextbox.text;
                currentAnswer = answer;
            }
        }


        private void OnDisable()
        {
            ExportToCSV.SaveDataWhileAnswering(currentQuestion, currentAnswer, CurrentQuestionIndex);
        }
    }
}