using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using TMPro;
using UnityEditor.Experimental;
using UnityEngine.UI;

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
        public GameObject LinearGrid;
        public GameObject LinearSlider;
        public GameObject TextInput;
        public GameObject LastPagePrefab;

        private GameObject _newPage;
        private const int QuestionPerPage = 4;

        public enum QuestionType
        {
            Radio,
            RadioGrid,
            Checkbox,
            CheckboxGrid,
            Dropdown,
            LinearScale,
            TextInput
        }

        private QuestionType _type;

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
            Button[] nextButton = q_footer.GetComponentsInChildren<Button>();
            nextButton[1].gameObject.SetActive(false);
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
                case "radio":
                    if (question.qData.Length < QuestionPerPage)
                    {
                        foreach (var (subQuestion, index) in question.qData.WithIndex())
                        {
                            temp = Instantiate(RadioHorizontalPrefab) as GameObject;
                            temp.name = "radioHorizontal_" + index;

                            radioHorizontalRec = temp.GetComponent<RectTransform>();
                            q_main = GameObject.Find("Q_Main");
                            radioHorizontalRec.SetParent(q_main.GetComponent<RectTransform>());

                            //ensuring correct placement and scaling in the UI
                            text = temp.GetComponentInChildren<TextMeshProUGUI>();

                            // If question mandatory -> add " * "
                            if (subQuestion.qMandatory)
                                text.text = subQuestion.qText + " *";
                            else
                                text.text = subQuestion.qText;

                            text.transform.localPosition = new Vector3(0, 120 - (index * 92), text.transform.localPosition.z);
                            SetRec(radioHorizontalRec);

                            QuestionList.Add(temp.GetComponent<Radio>().CreateRadioQuestion(subQuestion.qOptions, index, radioHorizontalRec));
                        }
                    }
                    else
                    {
                        Debug.LogError("We currently only support up to 5 radioquestions per page");
                    }
                    break;
                case "radioGrid":
                    if (question.qConditions.Length < 8)
                    {
                        for (int i = 0; i < question.qConditions.Length; i++)
                        {
                            temp = Instantiate(RadioGridPrefab) as GameObject;
                            temp.name = "radioGrid_" + i;

                            radioHorizontalRec = temp.GetComponent<RectTransform>();
                            q_main = GameObject.Find("Q_Main");
                            radioHorizontalRec.SetParent(q_main.GetComponent<RectTransform>());

                            //ensuring correct placement and scaling in the UI
                            textArray = temp.GetComponentsInChildren<TextMeshProUGUI>();
                            textArray[0].text = "";

                            if (i == 0)
                            {

                                // If question mandatory -> add " * " to question in UI
                                if (question.qData[i].qMandatory)
                                    textArray[0].text = question.qData[0].qText + " *";
                                else
                                    textArray[0].text = question.qData[0].qText;

                            }

                            //Differentiate between 5-Point and 7-Point Likert Scale
                            int temp2 = 0;
                            foreach (var option in question.qOptions)
                            {
                                if (option != "")
                                    temp2++;
                            }

                            if (temp2 == 5)
                            {
                                textArray[1].text = question.qConditions[i];
                                textArray[1].transform.localPosition = new Vector3(-195, 17 - (i * 50), textArray[1].transform.localPosition.z);
                            }
                            else
                            {
                                textArray[1].text = question.qConditions[i];
                                textArray[1].transform.localPosition = new Vector3(-245, 17 - (i * 50), textArray[1].transform.localPosition.z);
                            }

                            textArray[0].transform.localPosition = new Vector3(0, 120, textArray[0].transform.localPosition.z);

                            SetRec(radioHorizontalRec);
                            QuestionList.Add(temp.GetComponent<RadioGrid>().CreateRadioGridQuestion(question, i, radioHorizontalRec));
                        }
                    }
                    else
                    {
                        Debug.LogError("We currently only support up to 8 conditions");
                    }
                    break;
                case "checkbox":

                    for (int i = 0; i < question.qData.Length; i++)
                    {
                        temp = Instantiate(Checkbox) as GameObject;
                        temp.name = "checkbox_" + i;

                        radioHorizontalRec = temp.GetComponent<RectTransform>();
                        q_main = GameObject.Find("Q_Main");
                        radioHorizontalRec.SetParent(q_main.GetComponent<RectTransform>());

                        //ensuring correct placement and scaling in the UI
                        text = temp.GetComponentInChildren<TextMeshProUGUI>();
                        text.text = question.qData[i].qText;
                        text.transform.localPosition = new Vector3(10, 110 - (i * 50), text.transform.localPosition.z);
                        SetRec(radioHorizontalRec);

                        QuestionList.Add(temp.GetComponent<Checkbox>().CreateCheckboxQuestion(question, i, radioHorizontalRec));
                    }
                    break;
                case "checkboxGrid":
                    Debug.LogError("Checkboxgrid is not supported ATM");
                    break;
                case "linearGrid":
                    for (int i = 0; i < question.qData.Length; i++)
                    {
                        temp = Instantiate(LinearGrid) as GameObject;
                        temp.name = "linearGrid_" + i;
                        radioHorizontalRec = temp.GetComponent<RectTransform>();
                        q_main = GameObject.Find("Q_Main");
                        radioHorizontalRec.SetParent(q_main.GetComponent<RectTransform>());

                        //ensuring correct placement and scaling in the UI
                        text = temp.GetComponentInChildren<TextMeshProUGUI>();

                        if (question.qData[i].qMandatory)
                            text.text = question.qData[i].qText + " *";
                        else
                            text.text = question.qData[i].qText;

                        text.transform.localPosition = new Vector3(0, 100 - (i * 95), text.transform.localPosition.z);
                        SetRec(radioHorizontalRec);

                        QuestionList.Add(temp.GetComponent<LinearGrid>().CreateLinearGridQuestion(question, i, radioHorizontalRec));
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