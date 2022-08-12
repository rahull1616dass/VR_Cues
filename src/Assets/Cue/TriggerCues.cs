using Cues;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class TriggerCues : MonoBehaviour
{
    [SerializeField] private Transform allTriggerParant;
   public void PositionTrigger(List<CueTransform> positions, GameObject refCue)
   {
        refCue.SetActive(false);
        CreateTriggerObj(positions, refCue);
   }

    public void TimeTrigger(List<TimeTrigger> positions, GameObject refCue)
    {
        refCue.SetActive(false);

    }


    private List<TriggerColliderForCues> CreateTriggerObj(List<CueTransform> positionTriggers, GameObject cueToTrigger)
    {
        List<TriggerColliderForCues> tempTriggerColliders = new List<TriggerColliderForCues>();
        for (int i = 0; i < positionTriggers.Count; i++)
        {
            Transform triggerCollider = GameManager.instance.generateCueInScene.PlaceCueTransformInScene(positionTriggers[i],allTriggerParant, "CueTrigger_" + cueToTrigger.name + "_" + i + 1, typeof(TriggerColliderForCues), typeof(BoxCollider));
            TriggerColliderForCues tempRefTriggerCollider = triggerCollider.GetComponent<TriggerColliderForCues>();
            tempRefTriggerCollider.cueToTrigger = cueToTrigger;
            tempTriggerColliders.Add(tempRefTriggerCollider);
        }
        return tempTriggerColliders;
    }

    private void SetTimeTriggers(List<TimeTrigger> triggerTimers, GameObject refCue)
    {
        foreach (var trigger in triggerTimers)
        {
            RunTimeToTriggerCue(trigger, refCue);
        }
    }

    private async void RunTimeToTriggerCue(TimeTrigger triggerTimer, GameObject refCue)
    {
        double startupTime = Time.realtimeSinceStartupAsDouble;
        while(startupTime+(double)triggerTimer.startTime>Time.realtimeSinceStartupAsDouble)
        {
            await Task.Yield();
        }
        refCue.SetActive(true);
        while (startupTime + (double)triggerTimer.endTime > Time.realtimeSinceStartupAsDouble)
        {
            await Task.Yield();
        }
        refCue.SetActive(false);
    }
}
