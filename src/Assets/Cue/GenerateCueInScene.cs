using Cues;
using System;
using System.IO;
using UnityEngine;
using VRQuestionnaireToolkit;

public class GenerateCueInScene : MonoBehaviour
{
    [SerializeField] private GameObject questionnairePrefab;
    [SerializeField] private TriggerCues triggerCue;
    [SerializeField] private GameObject infoPrefab;
    [SerializeField] private Transform allCueParent;
    private QuestionnairePageFactory _pageFactory;

    public Transform PlaceCueTransformInScene(CueTransform cueTransform, Transform parentTransform , string objectName = "cueTransform", params Type[] componentsToAdd)
    {
        Transform tempCueTransform = new GameObject(objectName, componentsToAdd).transform;
        tempCueTransform.SetParent(parentTransform);
        tempCueTransform.position = cueTransform.position;
        tempCueTransform.rotation = cueTransform.rotation;
        tempCueTransform.localScale = cueTransform.scale;
        return tempCueTransform;
    }

    public Transform PlaceCueTransformInScene(CueTransform cueTransform, Transform parentTransform, GameObject prefab, string objectName = "cueTransform", params Type[] componentsToAdd)
    {

        Transform tempCueTransform = Instantiate(prefab, parentTransform).transform;
        foreach (Type component in componentsToAdd)
            tempCueTransform.gameObject.AddComponent(component);
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
        Transform questionnaireTransform = PlaceCueTransformInScene(questionnaire.cueTransform, allCueParent);

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

    public void generateImage(Image image)
    {
        Transform transformImage = PlaceCueTransformInScene(image.cueTransform, allCueParent);
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

    public void generateHighlight(Highlight highlight)
    {
        GameObject[] highlightableGameObjects = GameObject.FindGameObjectsWithTag("HighlightedObjects");

        foreach (GameObject o in highlightableGameObjects)
        {
            if (o.GetComponent<HighlightObject>() == null)
            {
                throw new Exception("You should add the HighlightObject script to the objects that needs to be highlighted.");
            }


            HighlightObject highlightObject = o.GetComponent<HighlightObject>();
            if(highlight.objectId == highlightObject.objectID)
            {
                highlightObject.initHighlight(
                highlight.highlightColor,
                highlight.animationTime,
                (iTween.EaseType)highlight.easeType,
               (iTween.LoopType)highlight.loopType
                );

                // Remove it after implement it on the triggers
                highlightObject.StartHighlight();
            }
            
        }

    }

    public void generateInfoBox(InfoBox infoBox)    
    {
        if(infoBox.buttons.Length > 2)
        {
            throw new Exception("For UX purposes, only 0 to 2 buttons are supported!");
        }
        Transform transformInfoBox = PlaceCueTransformInScene(infoBox.cueTransform, allCueParent, infoPrefab);


    }
}
