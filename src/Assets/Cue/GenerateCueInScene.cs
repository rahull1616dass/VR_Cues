using Cues;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRQuestionnaireToolkit;

public class GenerateCueInScene : MonoBehaviour
{
    [SerializeField] private GameObject questionnairePrefab;
    [SerializeField] private TriggerCues triggerCue;
    private QuestionnairePageFactory _pageFactory;

    private Transform cueTransformToTransform(CueTransform cueTransform)
    {
        Transform tempCueTransform = new GameObject("cueTransform").transform;
        tempCueTransform.position = cueTransform.position;
        tempCueTransform.rotation = cueTransform.rotation;
        tempCueTransform.localScale = cueTransform.scale;
        return tempCueTransform;
    }

    public void generateQuestionnaire(Questionnaire questionnaire)
    {
        GameObject currentQuestionnaire = Instantiate<GameObject>(questionnairePrefab);

        // Place in hierarchy 
        RectTransform radioGridRec = currentQuestionnaire.GetComponent<RectTransform>();
        Transform questionnaireTransform = cueTransformToTransform(questionnaire.cueTransform);
        radioGridRec.SetParent(questionnaireTransform);
        radioGridRec.localPosition = new Vector3(0, 0, 0);
        radioGridRec.localRotation = Quaternion.identity;
        radioGridRec.localScale = new Vector3(radioGridRec.localScale.x * 0.01f, radioGridRec.localScale.y * 0.01f, radioGridRec.localScale.z * 0.01f);

        _pageFactory = questionnaireTransform.GetComponentInChildren<QuestionnairePageFactory>();

        //----------- Read metadata from .JSON file ----------//
        string title = questionnaire.qInfo.qTitle;
        string instructions = questionnaire.qInfo.qInstructions;

        // Generates the first page
        _pageFactory.GenerateAndDisplayFirstAndLastPage(true, instructions, title);

        foreach (var (question, index) in questionnaire.questions.WithIndex())
        {
            _pageFactory.AddPage(question);
        }

        // Generates the last page
        _pageFactory.GenerateAndDisplayFirstAndLastPage(false, questionnaire.qInfo.qMessage, questionnaire.qInfo.qAcknowledgments);

        // Initialize (Dis-/enable GameObjects)
        _pageFactory.InitSetup();

        triggerCue.PositionTrigger(questionnaire.positionTrigger, currentQuestionnaire);
    }

    public void generateImage(Image image)
    {

    }

    public void generateHighlightObject(HighlightObject highlightObject)
    {

    }
}
