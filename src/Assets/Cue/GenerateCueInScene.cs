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
        //----------- Read metadata from .JSON file ----------//
        string title = questionnaire.qInfo.qTitle;
        string instructions = questionnaire.qInfo.qInstructions;

        // Generates the first page
        _pageFactory.GenerateAndDisplayFirstAndLastPage(true, instructions, title);

        foreach (var (question, index) in questionnaire.questions.WithIndex())
        {
            int pageId = index;
            _pageFactory.AddPage(question);
        }

        // Generates the last page
        _pageFactory.GenerateAndDisplayFirstAndLastPage(false, questionnaire.qInfo.qMessage, questionnaire.qInfo.qAcknowledgments);

        // Initialize (Dis-/enable GameObjects)
        _pageFactory.InitSetup();
    }

    public void generateImage(Image image)
    {

    }

    public void generateHighlightObject(HighlightObject highlightObject)
    {

    }
}
