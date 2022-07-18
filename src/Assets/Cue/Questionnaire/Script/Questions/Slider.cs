using System;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Slider.class
/// 
/// version 1.0
/// date: July 1st, 2020
/// authors: Martin Feick & Niko Kleer
/// </summary>

namespace VRQuestionnaireToolkit
{
    public class Slider : MonoBehaviour
    {
        private Sprite _sprite;
        public GameObject Sliders;
        private RectTransform _questionRecTest; // parent rectransform
        public List<GameObject> SliderList; //contains all radiobuttons which correspond to one question

        public List<GameObject> CreateSliderQuestion(Question question, int numberQuestion, RectTransform questionRec)
        {
            {
                if (numberQuestion > 7)
                {
                    throw new InvalidOperationException("We currently only support up to 7 sliders on one page");
                }

                SliderList = new List<GameObject>();
                // Instantiate slider prefabs
                GameObject temp = Instantiate(Sliders);
                temp.name = "slider_" + numberQuestion;

                // Use this for initialization
                _sprite = LoadSprite(question.qData[numberQuestion].qMax);
                temp.GetComponentInChildren<UnityEngine.UI.Slider>().GetComponent<Image>().sprite = _sprite;

                // Set required slider properties
                temp.GetComponentInChildren<UnityEngine.UI.Slider>().minValue = question.qData[numberQuestion].qMin;
                temp.GetComponentInChildren<UnityEngine.UI.Slider>().maxValue = question.qData[numberQuestion].qMax;
                temp.GetComponentInChildren<UnityEngine.UI.Slider>().GetComponentsInChildren<TextMeshProUGUI>()[0].text =
                    question.qData[numberQuestion].qMinLabel;
                temp.GetComponentInChildren<UnityEngine.UI.Slider>().GetComponentsInChildren<TextMeshProUGUI>()[1].text =
                    question.qData[numberQuestion].qMaxLabel;

                //Set Slider start value
                temp.GetComponentInChildren<UnityEngine.UI.Slider>().value = question.qData[numberQuestion].qMax % 2 == 0 ? 
                    question.qData[numberQuestion].qMax / 2 : 0;

                // Place in hierarchy 
                RectTransform sliderRec = temp.GetComponent<RectTransform>();
                sliderRec.SetParent(questionRec);
                sliderRec.localPosition = new Vector3(0, 90 - (numberQuestion * 100), 0);
                sliderRec.localRotation = Quaternion.identity;
                sliderRec.localScale = new Vector3(sliderRec.localScale.x * 0.01f, sliderRec.localScale.y * 0.01f, sliderRec.localScale.z * 0.01f);

                SliderList.Add(temp);

            }

            return SliderList;
        }



        private Sprite LoadSprite(int numberTicks)
        {
            Sprite temp;

            string load = "Sprites/Slider_" + (numberTicks + 1);
            temp = Resources.Load<Sprite>(load);

            return temp;
        }

    }
}