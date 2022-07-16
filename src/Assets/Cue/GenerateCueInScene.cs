using Cues;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRQuestionnaireToolkit;

public class GenerateCueInScene : MonoBehaviour
{
    [SerializeField] private GameObject questionnairePrefab;
    public static GenerateCueInScene instance;
    private QuestionnairePageFactory _pageFactory;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public void generateQuestionnaire(Questionnaire questionnaire)
    {
        Instantiate<GameObject>(questionnairePrefab);

    }

    public void generateImage(Image image)
    {

    }

    public void generateHighlightObject(HighlightObject highlightObject)
    {

    }
}
