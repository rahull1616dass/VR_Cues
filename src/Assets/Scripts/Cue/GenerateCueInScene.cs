using Assets.Extensions;
using Assets.Scripts.Extensions;
using Cues;
using System;
using System.Collections;
using System.IO;
using Unity.XR.PXR;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UIElements;
using VRQuestionnaireToolkit;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;
using Image = Cues.Image;

public class GenerateCueInScene : MonoBehaviour
{
    [SerializeField] private GameObject questionnairePrefab;
    [SerializeField] private TriggerCues triggerCue;
    [SerializeField] private GameObject infoPrefab;
    [SerializeField] private Transform allCueParent;
    [SerializeField] private GameObject leftGhostHandPrefab;
    [SerializeField] private GameObject rightGhostHandPrefab;

    private int cueIndex;

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
        Transform questionnaireTransform = CueTransformToTransform(questionnaire.cueTransform, allCueParent, "QuestionCue", typeof(CueData));
        questionnaireTransform.GetComponent<CueData>().AddData(questionnaire.logger, cueIndex++);

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
            mediaObj = generateImage(new Image(media.cueTransform, media.imageReferenceId), media.logger);
        }
        else if (!string.IsNullOrEmpty(media.audioReferenceId))
        {
            mediaObj = generateAudio(new Audio(media.cueTransform, media.audioReferenceId, media.audioShouldLoop));
            mediaObj.AddComponent<CueData>().AddData(media.logger, cueIndex++);
        }
        triggerCue.SetTrigger(media._triggers, mediaObj);
    }

    public GameObject generateImage(Image image, Logger logger)
    {
        Transform transformImage = CueTransformToTransform(image.cueTransform, allCueParent, "ImageCue", typeof(SpriteRenderer), typeof(CueData));
        transformImage.GetComponent<CueData>().AddData(logger, cueIndex++);

        // Assign sprite to the instantiated image here.
        SpriteRenderer spriteRenderer = transformImage.GetComponent<SpriteRenderer>();
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
                o.AddComponent<CueData>().AddData(highlight.logger, cueIndex++);
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
        if (infoBox.buttons.Length > 2)
        {
            throw new Exception("For UX purposes, only 0 to 2 buttons are supported!");
        }
        Transform transformInfoBox = CreateCueFromPrefab(infoBox.cueTransform, allCueParent, infoPrefab, "InfoBox", typeof(CueData));
        transformInfoBox.GetComponent<InfoBoxCreator>().CreateInfoBox(infoBox);
        transformInfoBox.GetComponent<CueData>().AddData(infoBox.logger, cueIndex++);
        triggerCue.SetTrigger(infoBox._triggers, transformInfoBox.gameObject);
    }

    public void generateHaptic(Haptic haptic)
    {
        switch (haptic.controller)
        {
            case ControllerDirections.Left:
                Transform leftHaptic = CueTransformToTransform(haptic.cueTransform, allCueParent, "Haptic", typeof(HapticHandler), typeof(CueData));
                leftHaptic.GetComponent<HapticHandler>().CreateHaptic(haptic.strength, PXR_Input.Controller.LeftController);
                leftHaptic.GetComponent<CueData>().AddData(haptic.logger, cueIndex++);
                triggerCue.SetTrigger(haptic._triggers, leftHaptic.gameObject);
                break;
            case ControllerDirections.Right:
                Transform rightHaptic = CueTransformToTransform(haptic.cueTransform, allCueParent, "Haptic", typeof(HapticHandler), typeof(CueData));
                rightHaptic.GetComponent<HapticHandler>().CreateHaptic(haptic.strength, PXR_Input.Controller.RightController);
                rightHaptic.GetComponent<CueData>().AddData(haptic.logger, cueIndex++);
                triggerCue.SetTrigger(haptic._triggers, rightHaptic.gameObject);
                break;
            default: throw new Exception($"{haptic.controller} is not a valid controller!");
        }

    }

    public GameObject generateAudio(Audio audio)
    {
        AudioClip clip = Resources.Load<AudioClip>("audio/" + audio.referenceId);
        return AudioManager.Instance.Play(clip,1f, audio.shouldLoop, audio.cueTransform.attachToPlayer, audio.cueTransform).gameObject;
    }

    public void generateGhostHand(GhostHand ghostHand)
    {
        GameObject handPrefab;
        switch (ghostHand.handType)
        {
            case ControllerDirections.Left:
                handPrefab = leftGhostHandPrefab;
                break;
            case ControllerDirections.Right:
                handPrefab = rightGhostHandPrefab;
                break;
            default: throw new Exception($"{ghostHand.handType} is not a valid controller!");
        }
        Transform transformGhostHand = CreateCueFromPrefab(ghostHand.cueTransform, allCueParent, handPrefab, "GhostCue", typeof(CueData));
        transformGhostHand.GetComponent<CueData>().AddData(ghostHand.logger, cueIndex++);

        Animator handAnimation = transformGhostHand.GetChild(0).GetComponent<Animator>();
        handAnimation.Play(ghostHand.animationName, -1, 0f);
        triggerCue.SetTrigger(ghostHand._triggers, transformGhostHand.gameObject);
    }


}
