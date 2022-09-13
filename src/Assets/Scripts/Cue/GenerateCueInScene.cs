using Assets.Extensions;
using Cues;
using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using VRQuestionnaireToolkit;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class GenerateCueInScene : MonoBehaviour
{
    [SerializeField] private GameObject questionnairePrefab;
    [SerializeField] private TriggerCues triggerCue;
    [SerializeField] private GameObject infoPrefab;
    [SerializeField] private Transform allCueParent;

    public Transform CueTransformToTransform(CueTransform cueTransform, Transform parentTransform , string objectName = "cueTransform", 
        params Type[] componentsToAdd)

    {
        if (cueTransform.attachToPlayer)
        {
            parentTransform = Camera.main.transform;
        }
        Transform tempCueTransform = new GameObject(objectName, componentsToAdd).transform;
        tempCueTransform.SetParent(parentTransform);
        tempCueTransform.localPosition = cueTransform.position;
        tempCueTransform.localEulerAngles = cueTransform.rotation;
        tempCueTransform.localScale = cueTransform.scale;
        return tempCueTransform;
    }

    public Transform CreateCueFromPrefab(CueTransform cueTransform, Transform parentTransform, GameObject prefab, string objectName = "cueTransform", 
        params Type[] componentsToAdd)
    {
        if (cueTransform.attachToPlayer)
        {
            parentTransform = Camera.main.transform;
        }
        Transform tempCueTransform = Instantiate(prefab, parentTransform).transform;
        foreach (Type component in componentsToAdd)
            tempCueTransform.gameObject.AddComponent(component);
        tempCueTransform.localPosition = cueTransform.position;
        tempCueTransform.localEulerAngles = cueTransform.rotation;
        tempCueTransform.localScale = cueTransform.scale;
        return tempCueTransform;
    }

    public void generateQuestionnaire(Questionnaire questionnaire)
    {
        GameObject currentQuestionnaire = Instantiate<GameObject>(questionnairePrefab);

        // Place in hierarchy 
        RectTransform rectTransformQuestionnaire = currentQuestionnaire.GetComponent<RectTransform>();
        Transform questionnaireTransform = CueTransformToTransform(questionnaire.cueTransform, allCueParent);

        // Resetting transform
        rectTransformQuestionnaire.SetParent(questionnaireTransform);
        rectTransformQuestionnaire.localPosition = new Vector3(0, 0, 0);
        rectTransformQuestionnaire.localRotation = Quaternion.identity;
        rectTransformQuestionnaire.localScale = new Vector3(rectTransformQuestionnaire.localScale.x * 0.01f, rectTransformQuestionnaire.localScale.y * 0.01f, rectTransformQuestionnaire.localScale.z * 0.01f);

        QuestionnairePageFactory pageFactory = questionnaireTransform.GetComponentInChildren<QuestionnairePageFactory>();

        //----------- Read metadata from .JSON file ----------//
        string title = questionnaire.qInfo.qTitle;
        string instructions = questionnaire.qInfo.qInstructions;

        // Generates the first page
        pageFactory.GenerateAndDisplayFirstAndLastPage(true, instructions, title);

        foreach (var (question, index) in questionnaire.questions.WithIndex())
        {
            pageFactory.AddPage(question);
        }

        // Generates the last page
        pageFactory.GenerateAndDisplayFirstAndLastPage(false, questionnaire.qInfo.qMessage, questionnaire.qInfo.qAcknowledgments);

        // Initialize (Dis-/enable GameObjects)
        pageFactory.InitSetup();

        triggerCue.SetTrigger(questionnaire._triggers, currentQuestionnaire);
    }

    public void generateMedia(Media media)
    {
        GameObject mediaObj = null;
        if (!string.IsNullOrEmpty(media.imageReferenceId))
        {
            mediaObj = generateImage(new Image(media.cueTransform, media.imageReferenceId));
        }
        else if (!string.IsNullOrEmpty(media.audioReferenceId))
        {
            mediaObj = generateAudio(new Audio(media.cueTransform, media.audioReferenceId, media.audioShouldLoop));
        }
        triggerCue.SetTrigger(media._triggers, mediaObj);
    }

    public GameObject generateImage(Image image)
    {
        Transform transformImage = CueTransformToTransform(image.cueTransform, allCueParent);
       
        // Assign sprite to the instantiated image here.
        SpriteRenderer spriteRenderer = transformImage.gameObject.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = image.referenceId.createSpriteFromReferenceId();
        return transformImage.gameObject;
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
        Transform transformInfoBox = CreateCueFromPrefab(infoBox.cueTransform, allCueParent, infoPrefab);
        transformInfoBox.gameObject.GetComponent<InfoBoxCreator>().CreateInfoBox(infoBox);
        triggerCue.SetTrigger(infoBox._triggers, transformInfoBox.gameObject);
    }

    public void generateHaptic(Haptic haptic)
    {
        // Pico.generateHaptic(haptic.amplitude, haptic.duration);
    }

    public GameObject generateAudio(Audio audio)
    {
        AudioClip clip = Resources.Load<AudioClip>("audio/" + audio.referenceId);
        return AudioManager.Instance.Play(clip,1f, audio.shouldLoop, audio.cueTransform.attachToPlayer, audio.cueTransform).gameObject;
    }

    
}
