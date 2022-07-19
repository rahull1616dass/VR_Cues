using Cues;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRQuestionnaireToolkit;

public class GenerateCueInScene : MonoBehaviour
{
    [SerializeField] private GameObject questionnairePrefab;
    private QuestionnairePageFactory _pageFactory;


    public void generateQuestionnaire(Questionnaire questionnaire)
    {
        GameObject currentQuestionnaire = Instantiate<GameObject>(questionnairePrefab);

        // Place in hierarchy 
        RectTransform radioGridRec = currentQuestionnaire.GetComponent<RectTransform>();
        //radioGridRec.SetParent(QuestionRecTest);
        radioGridRec.localPosition = new Vector3(0, 0, 0);
        radioGridRec.localRotation = Quaternion.identity;
        radioGridRec.localScale = new Vector3(radioGridRec.localScale.x * 0.01f, radioGridRec.localScale.y * 0.01f, radioGridRec.localScale.z * 0.01f);

        _pageFactory = this.GetComponentInChildren<QuestionnairePageFactory>();

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
