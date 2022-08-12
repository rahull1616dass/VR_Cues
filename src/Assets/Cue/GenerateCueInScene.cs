using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using VRQuestionnaireToolkit;

public class GenerateCueInScene : MonoBehaviour
{
    [SerializeField] private GameObject questionnairePrefab;
    [SerializeField] private TriggerCues triggerCue;
    private QuestionnairePageFactory _pageFactory;

    public Transform PlaceCueTransformInScene(CueTransform cueTransform, string objectName = "cueTransform", params System.Type[] componentsToAdd)
    {
        Transform tempCueTransform = new GameObject(objectName, componentsToAdd).transform;
        tempCueTransform.position = cueTransform.position;
        tempCueTransform.rotation = cueTransform.rotation;
        tempCueTransform.localScale = cueTransform.scale;
        return tempCueTransform;
    }

    public void generateQuestionnaire(Questionnaire questionnaire)
    {
        GameObject currentQuestionnaire = Instantiate<GameObject>(questionnairePrefab);

        // Place in hierarchy 
        RectTransform rectTransformQuestionnaire = currentQuestionnaire.GetComponent<RectTransform>();
        Transform questionnaireTransform = PlaceCueTransformInScene(questionnaire.cueTransform);

        // Resetting transform
        rectTransformQuestionnaire.SetParent(questionnaireTransform);
        rectTransformQuestionnaire.localPosition = new Vector3(0, 0, 0);
        rectTransformQuestionnaire.localRotation = Quaternion.identity;
        rectTransformQuestionnaire.localScale = new Vector3(rectTransformQuestionnaire.localScale.x * 0.01f, rectTransformQuestionnaire.localScale.y * 0.01f, rectTransformQuestionnaire.localScale.z * 0.01f);

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

    public void generateImage(Cues.Image image)
    {
        Transform transformImage = PlaceCueTransformInScene(image.cueTransform);

        //Creates texture and loads byte array data to create image
        Texture2D texture2DImage = new Texture2D(100, 100);
        texture2DImage.LoadImage(File.ReadAllBytes($"{Application.streamingAssetsPath}/images/{image.referenceId}"));

        //Creates a new Sprite based on the Texture2D
        Sprite spriteImage = Sprite.Create(texture2DImage, 
            new Rect(0.0f, 0.0f, texture2DImage.width, texture2DImage.height), new Vector2(0.5f, 0.5f), 100.0f);

        // Assign sprite to the instantiated image here.
        SpriteRenderer spriteRenderer = transformImage.gameObject.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = spriteImage;
    }

    public void generateHighlightObject(HighlightObject highlightObject)
    {

    }
}
