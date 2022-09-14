using System;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace VRQuestionnaireToolkit
{
    public class LinearGrid : MonoBehaviour
    {
        private Sprite _sprite;

        public GameObject LinearGridButton;
        public JSONArray QOptions;
        public bool qMandatory;

        private bool _isOdd;
        public List<GameObject> LinearGridList; //contains all radiobuttons which correspond to one question

        [SerializeField] private TextMeshProUGUI questionTextbox;
        private string currentQuestion, currentAnswer;
        private int CurrentQuestionIndex;

        private bool IsAlreadyEnabled;
        //qText look how many q in one file >4 deny
        public List<GameObject> CreateLinearGridQuestion(QData subQuestion, int numberQuestion, RectTransform questionRec)
        {
            this.qMandatory = subQuestion.qMandatory;

            if (subQuestion.qMax > 20)
            {
                throw new InvalidOperationException("We currently only support up to 20 gridCells");
            }

            LinearGridList = new List<GameObject>();


            // generate radio and corresponding text labels on a single page
            for (int j = 0; j < subQuestion.qMax; j++)
            {
                // Instantiate radio prefabs
                GameObject temp = Instantiate(LinearGridButton);
                temp.name = "linearButton_" + numberQuestion;

                // Place in hierarchy 
                RectTransform radioRec = temp.GetComponent<RectTransform>();
                radioRec.SetParent(questionRec);
                radioRec.localPosition = new Vector3(-120 + (j * 20), 90 - (numberQuestion * 95), 0);
                radioRec.localRotation = Quaternion.identity;
                radioRec.localScale = new Vector3(radioRec.localScale.x * 0.01f, radioRec.localScale.y * 0.01f, radioRec.localScale.z * 0.01f);


                if (j == 0)
                {
                    temp.GetComponentInChildren<Toggle>().GetComponentsInChildren<TextMeshProUGUI>()[0].text =
                        subQuestion.qMinLabel;
                    temp.GetComponentInChildren<Toggle>().GetComponentsInChildren<TextMeshProUGUI>()[1].text =
                        subQuestion.qMaxLabel;
                }
                else
                {
                    temp.GetComponentInChildren<Toggle>().GetComponentsInChildren<TextMeshProUGUI>()[0].text =
                        "";
                    temp.GetComponentInChildren<Toggle>().GetComponentsInChildren<TextMeshProUGUI>()[1].text =
                        "";
                    temp.GetComponentInChildren<Toggle>().GetComponentsInChildren<Image>()[2].gameObject.SetActive(false);
                }

                // Set radiobutton group
                LinearGrid linearGridScript = temp.GetComponentInParent<LinearGrid>();

                Toggle currentToggle = temp.GetComponentInChildren<Toggle>();
                currentToggle.group = linearGridScript.gameObject.GetComponent<ToggleGroup>();
                currentToggle.onValueChanged.AddListener((value) => OnValueChangeCheckBox(j.ToString(), value)) ;

                LinearGridList.Add(temp);
            }
            return LinearGridList;
        }

        private void SetMiddle()
        {
            _sprite = LoadSprite();

        }

        private Sprite LoadSprite()
        {
            Sprite temp;

            string load = "stick";
            temp = Resources.Load<Sprite>(load);

            return temp;
        }

        private int CountRadioButtons(JSONArray qOptions)
        {
            int counter = 0;
            for (int i = 0; i < qOptions.Count; i++)
            {
                if (qOptions[i] != "")
                    counter++;
            }

            return counter;
        }

        public void OnValueChangeCheckBox(string answer, bool value)
        {
            if (value)
            {
                currentQuestion = questionTextbox.text;
                currentAnswer = answer;
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

            ExportToCSV.SaveDataWhileAnswering("Range", currentQuestion, currentAnswer, CurrentQuestionIndex);
        }
    }
}