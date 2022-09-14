using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class TriggerTimeForCues : MonoBehaviour
{
    private GameObject cueToTrigger, endTrigger;

    public void StartByTimeTrigger(GameObject refCue, float TriggerTime, GameObject endTriggerObj = null)
    {
        cueToTrigger = refCue;
        endTrigger = endTriggerObj;
        if (endTrigger != null)
            endTrigger.SetActive(false);
        RunTimeToTriggerCue(TriggerTime, true);
    }

    public void EndByTimeTrigger(GameObject refCue, float TriggerTime)
    {
        cueToTrigger = refCue;
        RunTimeToTriggerCue(TriggerTime, false);
    }

    private async void RunTimeToTriggerCue(float triggerTimer, bool ObjectStateAfterTime)
    {
        double startupTime = Time.realtimeSinceStartupAsDouble;
        while (startupTime < triggerTimer)
        {
            await Task.Yield();
            triggerTimer = triggerTimer - Time.deltaTime;
            //Debug.Log($"Time={triggerTimer}");
        }
        cueToTrigger.SetActive(ObjectStateAfterTime);
        if (endTrigger != null)
            endTrigger.SetActive(ObjectStateAfterTime);
    }
}
