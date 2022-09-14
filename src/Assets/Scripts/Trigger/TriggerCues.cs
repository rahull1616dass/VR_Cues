using Cues;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System;

public class TriggerCues : MonoBehaviour
{
    [SerializeField] private Transform allTriggerParant;
   public void SetTrigger(List<Trigger> allTriggers, GameObject refCue)
   {
        refCue.SetActive(false);
        for (int i = 0; i < allTriggers.Count; i++)
        {
            CreateTriggers(allTriggers[i]._startPoint, allTriggers[i]._endPoint, refCue, i);
        }
        
   }

    private GameObject CreateEndTriggers(TriggerPoint endTrigger, GameObject refCue, int index)
    {
        if (endTrigger == null)
            return null;
        if (endTrigger._triggerPoint.GetType() == typeof(float))
        {
            Transform triggerTimer = GameManager.instance.generateCueInScene.CueTransformToTransform(new CueTransform(), 
                allTriggerParant, "CueTrigger_" + refCue.name + "_" + index + 1, typeof(TriggerTimeForCues));
            triggerTimer.GetComponent<TriggerTimeForCues>().EndByTimeTrigger(refCue, (float)endTrigger._triggerPoint);
        }
        else if(endTrigger._triggerPoint.GetType() == typeof(CueTransform))
        {
            Transform triggerCollider = GameManager.instance.generateCueInScene.CueTransformToTransform((CueTransform)endTrigger._triggerPoint, 
                allTriggerParant, "CueTrigger_" + refCue.name + "_" + index + 1, typeof(TriggerColliderForCues), typeof(BoxCollider));
            triggerCollider.GetComponent<TriggerColliderForCues>().ArrangeTheTrigger(refCue, null, false);
            return triggerCollider.gameObject;
        }
        return null;
    }

    private void CreateTriggers(TriggerPoint startTriggers, TriggerPoint endTrigger, GameObject refCue, int index)
    {
        
        if (startTriggers._triggerPoint.GetType() == typeof(float))
        {
            Transform triggerTimer = GameManager.instance.generateCueInScene.CueTransformToTransform(new CueTransform(),
                allTriggerParant, "CueTrigger_" + refCue.name + "_" + index + 1, typeof(TriggerTimeForCues));
            triggerTimer.GetComponent<TriggerTimeForCues>().StartByTimeTrigger(refCue, (float)startTriggers._triggerPoint, CreateEndTriggers(endTrigger, refCue, index));
        }
        else if (startTriggers._triggerPoint.GetType() == typeof(CueTransform))
        {
            Transform triggerCollider = GameManager.instance.generateCueInScene.CueTransformToTransform((CueTransform)startTriggers._triggerPoint,
                allTriggerParant, "CueTrigger_" + refCue.name + "_" + index + 1, typeof(TriggerColliderForCues), typeof(BoxCollider));
            triggerCollider.GetComponent<TriggerColliderForCues>().ArrangeTheTrigger(refCue, CreateEndTriggers(endTrigger, refCue, index), true);
        }
    }
}
