using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using SimpleJSON;
using TMPro;
using UnityEngine;

/// <summary>
/// Dropdown.class
/// 
/// version 1.0
/// date: July 1st, 2020
/// authors: Martin Feick & Niko Kleer
/// </summary>

namespace VRQuestionnaireToolkit
{
    public class Dropdown : MonoBehaviour
    {

        public GameObject Dropbdown;
        public List<GameObject> DropdownList; //contains all radiobuttons which correspond to one question

        [SerializeField] private TextMeshProUGUI questionTextbox;
        private string currentQuestion, currentAnswer;

        private int CurrentQuestionIndex;

        private bool IsAlreadyEnabled;

        //qText look how many q in one file >4 deny
        public List<GameObject> CreateDropdownQuestion(string[] options, int dropdownIndex, RectTransform questionRec)
        {
            if (options.Length > 7)
            {
                throw new InvalidOperationException("We currently only support up to 7 dropdown questions on a single page");
            }

            DropdownList = new List<GameObject>();

            // generate dropdowns and corresponding text labels on a single page
            // Instantiate dropdown prefabs
            GameObject temp = Instantiate(Dropbdown);
            temp.name = "dropdown" + dropdownIndex;

            TMP_Dropdown dropdown = temp.GetComponentInChildren<TMP_Dropdown>();

            // Set dropdown options (Text) ;image also possible
            foreach (var (optionText, index) in options.WithIndex())
                dropdown.options[index].text = optionText;

            dropdown.onValueChanged.AddListener((value) => OnValueChangeDropDown(options, value));

            // Place in hierarchy 
            RectTransform dropbDownRec = temp.GetComponent<RectTransform>();
            dropbDownRec.SetParent(questionRec);
            dropbDownRec.localPosition = new Vector3(0, 80 - (dropdownIndex * 90), 0);
            // dropbDownRec.localRotation = Quaternion.identity;
            dropbDownRec.localScale = new Vector3(dropbDownRec.localScale.x * 0.01f, dropbDownRec.localScale.y * 0.01f, dropbDownRec.localScale.z * 0.01f);
            DropdownList.Add(temp);
            OnValueChangeDropDown(options, 0);
            return DropdownList;
        }

        public void OnValueChangeDropDown(string[] options, int index)
        {
            currentQuestion = questionTextbox.text;
            currentAnswer = options[index];
        }
        private void OnEnable()
        {
            Debug.Log("CallingEnable");

            if (!IsAlreadyEnabled)
            {
                CurrentQuestionIndex = ExportToCSV.QuestionIndex;
                ExportToCSV.QuestionIndex++;
                IsAlreadyEnabled = true; 
                ExportToCSV.SaveDataWhileAnswering("", "", CurrentQuestionIndex);
            }
        }
        private void OnDisable()
        {
            Debug.Log("CallingDisable");

            ExportToCSV.SaveDataWhileAnswering(currentQuestion, currentAnswer, CurrentQuestionIndex);
        }
    }
}