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
                radioRec.localPosition = new Vector3(-120 + (numberQuestion * 20), 90 - (numberQuestion * 95), 0);
                radioRec.localRotation = Quaternion.identity;
                radioRec.localScale = new Vector3(radioRec.localScale.x * 0.01f, radioRec.localScale.y * 0.01f, radioRec.localScale.z * 0.01f);


                if (numberQuestion == 0)
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
                temp.GetComponentInChildren<Toggle>().group = linearGridScript.gameObject.GetComponent<ToggleGroup>();

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
    }
}