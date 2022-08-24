using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using TMPro;
using UnityEditor.Experimental;
using UnityEngine.UI;
using Assets.Extensions;
using System;

/// <summary>
/// PageFactory.class
/// 
/// version 1.0
/// date: July 1st, 2020
/// authors: Martin Feick & Niko Kleer
/// </summary>

namespace VRQuestionnaireToolkit
{
    public class QuestionnairePageFactory : MonoBehaviour
    {
        [SerializeField] private RectTransform qHeader;
        [SerializeField] private RectTransform qBody;


        public int NumPages;
        public int CurrentPage;

        public List<GameObject> PageList;
        public List<List<GameObject>> QuestionList; //contains all questions which belong to one page

        public RectTransform PageParent;
        public GameObject PagePrefab;
        public GameObject RadioHorizontalPrefab;
        public GameObject RadioGridPrefab;
        public GameObject Checkbox;
        public GameObject CheckboxGrid;
        public GameObject Dropdown;
        public GameObject LinearGridPrefab;
        public GameObject LinearSlider;
        public GameObject TextInput;
        public GameObject LastPagePrefab;

        private GameObject _newPage;
        private const int QuestionPerPage = 4;

        struct QuestionTypes
        {
            public const string Radio = "radio";
            public const string RadioGrid = "radioGrid";
            public const string Checkbox = "checkbox";
            public const string CheckboxGrid = "checkboxGrid";
            public const string Dropdown = "dropdown";
            public const string LinearScale = "linearScale";
            public const string TextInput = "textInput";
            public const string LinearGrid = "linearGrid";
        }

        struct Limitations
        {
            public const int MaxRadioQuestions = 5;
            public const int MaxRadioGridQuestions = 8;
            public const int MaxCheckboxQuestions = 7;
        }


        /*
         * This method adds 1..n pages to a questionnaire
        */
        public void AddPage(Question question)
        {
            if (question.qData != null)
            {
                _newPage.SetActive(false); //do only keep one page enabled

                _newPage = Instantiate(PagePrefab);
                _newPage.name = "page_" + PageList.Count;

                //ensuring correct placement and scaling in the UI
                RectTransform pageRec = _newPage.GetComponent<RectTransform>();
                pageRec.SetParent(PageParent);
                SetRec(pageRec);

                //ensuring the anchor of q_panel is centered
                GameObject q_panel = GameObject.Find("Q_Panel");
                RectTransform qPanelRect = q_panel.GetComponent<RectTransform>();
                CenterRec(qPanelRect);

                //display instruction on page
                GameObject q_header = GameObject.Find("Q_Header");
                TextMeshProUGUI descriptionText = q_header.GetComponentInChildren<TextMeshProUGUI>();
                descriptionText.text = question.qInstructions;

                PageList.Add(_newPage);
                NumPages++;

                DetermineQuestionType(question);

                // Debug.Log(qId + " " + qType + " " + qInstructions + " " + qText + " " + qOptions);
                // Debug.Log(qOptions.Count);
            }
        }

        public void InitSetup()
        {
            CurrentPage = 0;

            for (int i = 1; i < NumPages - 1; i++)
                PageList[i].SetActive(false);

            PageList[CurrentPage].SetActive(true);

            GameObject q_footer = GameObject.Find("Q_Footer");
            UnityEngine.UI.Button[] nextButton = q_footer.GetComponentsInChildren<UnityEngine.UI.Button>();
            nextButton[1].gameObject.SetActive(false);
        }


        private GameObject instantiateTypedQuestion(string questionType, int questionIndex, GameObject questionPrefab)
        {
            GameObject typedQuestion = Instantiate(questionPrefab) as GameObject;
            typedQuestion.name = $"{questionType}_{questionIndex}";
            RectTransform typedQuestionRect = typedQuestion.GetComponent<RectTransform>();
            typedQuestionRect.SetParent(GameObject.Find("Q_Main").GetComponent<RectTransform>());
            SetRec(typedQuestionRect);
            return typedQuestion;
        }
        /*
         * This method instantiates the required components based on the JSON Input
         */
        public void DetermineQuestionType(Question question)
        {
            GameObject temp;
            RectTransform radioHorizontalRec;
            GameObject q_main;
            TextMeshProUGUI text;
            TextMeshProUGUI[] textArray;

            switch (question.qType)
            {
                case QuestionTypes.Radio:
                    if (question.qData.Length > Limitations.MaxRadioQuestions)
                    {
                        throw new InvalidOperationException($@"We currently only support up to 
                            {Limitations.MaxRadioQuestions} radioquestions per page");
                    }

                    if (question.qData.Length < QuestionPerPage)
                    {
                        foreach (var (subQuestion, index) in question.qData.WithIndex())
                        {
                            GameObject typedQuestion = instantiateTypedQuestion(QuestionTypes.Radio, index, RadioHorizontalPrefab);
                            //ensuring correct placement and scaling in the UI
                            text = typedQuestion.GetComponentInChildren<TextMeshProUGUI>();
                            // If question mandatory -> add " * "
                            text.text = subQuestion.qText.adjustMandatoryText(subQuestion.qMandatory);
                            text.transform.localPosition = new Vector3(0, 120 - (index * 92), text.transform.localPosition.z);
                            QuestionList.Add(typedQuestion.GetComponent<Radio>()
                                .CreateRadioQuestion(subQuestion, index, typedQuestion.GetComponent<RectTransform>()));
                        }
                    }
                    break;

                case QuestionTypes.RadioGrid:
                    if (question.qConditions.Length > Limitations.MaxRadioQuestions)
                    {
                        throw new InvalidOperationException($@"We currently only support up to 
                            {Limitations.MaxRadioQuestions} 8 radioGrid conditions");
                    }
                    foreach (var (condition, index) in question.qConditions.WithIndex())
                      {
                        GameObject typedQuestion = instantiateTypedQuestion(QuestionTypes.RadioGrid, index, RadioGridPrefab);

                        //ensuring correct placement and scaling in the UI
                        textArray = typedQuestion.GetComponentsInChildren<TextMeshProUGUI>();
                        textArray[0].text = question.qData[0].qText.adjustMandatoryText(question.qData[index].qMandatory);
                        textArray[1].text = question.qConditions[index];
                       
                        //Differentiate between 5-Point and 7-Point Likert Scale
                        textArray[1].transform.localPosition = new Vector3(
                            question.qOptions.Length switch
                            {
                                5 => -195,
                                7 => -245,
                                _ => throw new InvalidOperationException("Likert Scale must have 5 or 7 points")
                            }, 
                            17 - (index * 50), 
                            textArray[1].transform.localPosition.z
                            );

                        textArray[0].transform.localPosition = new Vector3(0, 120, textArray[0].transform.localPosition.z);
                        QuestionList.Add(typedQuestion.GetComponent<RadioGrid>()
                            .CreateRadioGridQuestion(question, index, typedQuestion.GetComponent<RectTransform>()));
                    }
                    break;

                case QuestionTypes.Checkbox:
                    if (question.qData.Length > Limitations.MaxCheckboxQuestions)
                    {
                        throw new InvalidOperationException($@"We currently only support up to {Limitations.MaxCheckboxQuestions} 
                                                            checkboxes on a single page");
                    }
                    foreach (var (subQuestion, index) in question.qData.WithIndex())
                    {
                        GameObject typedQuestion = instantiateTypedQuestion(QuestionTypes.Checkbox, index, RadioGridPrefab);

                        //ensuring correct placement and scaling in the UI
                        text = typedQuestion.GetComponentInChildren<TextMeshProUGUI>();
                        text.text = subQuestion.qText;
                        text.transform.localPosition = new Vector3(10, 110 - (index * 50), text.transform.localPosition.z);

                        QuestionList.Add(typedQuestion.GetComponent<Checkbox>().CreateCheckboxQuestion(subQuestion.qOptions, index, typedQuestion.GetComponent<RectTransform>()));
                    }
                    break;
                case "checkboxGrid":
                    Debug.LogError("Checkboxgrid is not supported ATM");
                    break;
                case QuestionTypes.LinearGrid:
                    foreach (var (subQuestion, index) in question.qData.WithIndex())
                    {
                        GameObject typedQuestion = instantiateTypedQuestion(QuestionTypes.LinearGrid, index, LinearGridPrefab);

                        //ensuring correct placement and scaling in the UI
                        text = typedQuestion.GetComponentInChildren<TextMeshProUGUI>();
                        text.text = subQuestion.qText.adjustMandatoryText(subQuestion.qMandatory);
                        text.transform.localPosition = new Vector3(0, 100 - (index * 95), text.transform.localPosition.z);
                        QuestionList.Add(typedQuestion.GetComponent<LinearGrid>()
                            .CreateLinearGridQuestion(subQuestion, index, typedQuestion
                            .GetComponent<RectTransform>()));
                    }
                    break;
                case "linearSlider":
                    for (int i = 0; i < question.qData.Length; i++)
                    {
                        temp = Instantiate(LinearSlider) as GameObject;
                        temp.name = "linearSlider_" + i;
                        radioHorizontalRec = temp.GetComponent<RectTransform>();
                        q_main = GameObject.Find("Q_Main");
                        radioHorizontalRec.SetParent(q_main.GetComponent<RectTransform>());

                        //ensuring correct placement and scaling in the UI
                        text = temp.GetComponentInChildren<TextMeshProUGUI>();
                        text.text = question.qData[i].qText;
                        text.transform.localPosition = new Vector3(0, 100 - (i * 100), text.transform.localPosition.z);
                        SetRec(radioHorizontalRec);
                        text.text = question.qData[i].qText;

                        QuestionList.Add(temp.GetComponent<Slider>().CreateSliderQuestion(question, i, radioHorizontalRec));
                    }
                    break;
                case "dropdown":
                    for (int i = 0; i < question.qData.Length; i++)
                    {
                        temp = Instantiate(Dropdown) as GameObject;
                        temp.name = "dropdown_" + i;
                        radioHorizontalRec = temp.GetComponent<RectTransform>();
                        q_main = GameObject.Find("Q_Main");
                        radioHorizontalRec.SetParent(q_main.GetComponent<RectTransform>());

                        //ensuring correct placement and scaling in the UI
                        text = temp.GetComponentInChildren<TextMeshProUGUI>();
                        text.text = question.qData[i].qText;
                        text.transform.localPosition = new Vector3(0, 120 - (i * 90), text.transform.localPosition.z);
                        SetRec(radioHorizontalRec);

                        QuestionList.Add(temp.GetComponent<Dropdown>().CreateDropdownQuestion(question, i, radioHorizontalRec));
                    }
                    break;
                case "textInput":
                    Debug.LogError("TextInput is not supported ATM");
                    break;
                default:
                    Debug.LogError("We do not support this questiontype");
                    break;
            }
        }

        #region SetParent
        private void SetRec(RectTransform rec)
        {
            rec.localPosition = new Vector3(0, 0, 0);
            rec.localRotation = Quaternion.identity;
            rec.localScale = new Vector3(rec.localScale.x * 0.01f, rec.localScale.y * 0.01f, rec.localScale.z * 0.01f);
            // rec.localScale = new Vector3(1, 1, 1);
        }


        //centering the anchor and the position of the RectTransform
        private void CenterRec(RectTransform rec)
        {
            rec.anchorMax = new Vector2(0.5f, 0.5f);
            rec.anchorMin = new Vector2(0.5f, 0.5f);
            rec.pivot = new Vector2(0.5f, 0.5f);
            rec.localPosition = new Vector3(0, 0, 0);
        }


        #endregion

        #region FirstAndLastPage
        public void GenerateAndDisplayFirstAndLastPage(bool firstPage, string qText, string qInstructions)
        {
            GameObject temp;

            if (firstPage)
            {
                _newPage = Instantiate(PagePrefab);
                _newPage.name = "page_first";
                QuestionList = new List<List<GameObject>>();
                PageList = new List<GameObject>();

            }
            else
            {
                _newPage.SetActive(false);
                _newPage = Instantiate(PagePrefab);
                _newPage.name = "page_final";
            }

            //ensuring correct placement and scaling in the UI
            RectTransform pageRec = _newPage.GetComponent<RectTransform>();
            pageRec.SetParent(PageParent);
            SetRec(pageRec);

            //ensuring the anchor of q_panel is centered
            GameObject q_panel = GameObject.Find("Q_Panel");
            RectTransform qPanelRect = q_panel.GetComponent<RectTransform>();
            CenterRec(qPanelRect);

            //display instruction on page
            GameObject q_header = GameObject.Find("Q_Header");
            TextMeshProUGUI descriptionText = q_header.GetComponentInChildren<TextMeshProUGUI>();
            descriptionText.text = qInstructions;

            if (!firstPage)
            {
                GameObject q_footer = GameObject.Find("Q_Footer");
                q_footer.SetActive(false);
            }

            PageList.Add(_newPage);
            NumPages++;

            if (!firstPage)
            {
                temp = Instantiate(LastPagePrefab) as GameObject;
                temp.name = "final";
            }
            else
            {
                temp = Instantiate(LastPagePrefab) as GameObject;
                temp.name = "first";
            }

            RectTransform pageFinalRec = temp.GetComponent<RectTransform>();
            GameObject q_main = GameObject.Find("Q_Main");
            pageFinalRec.SetParent(q_main.GetComponent<RectTransform>());

            //ensuring correct placement and scaling in the UI
            TextMeshProUGUI text = temp.GetComponentInChildren<TextMeshProUGUI>();
            text.text = qText;
            text.transform.localPosition = new Vector3(0, 60, text.transform.localPosition.z);
            SetRec(pageFinalRec);

            _newPage.SetActive(false);
        }


        #endregion

    }
}