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
        await Task.Delay((int)(1000f * triggerTimer));
        cueToTrigger.SetActive(ObjectStateAfterTime);
        SaveLog(ObjectStateAfterTime, triggerTimer);
        if (endTrigger != null)
            endTrigger.SetActive(ObjectStateAfterTime);
    }


    private void SaveLog(bool ObjectState, float triggerTimer)
    {
        Logger cueLogData = cueToTrigger.GetComponent<CueData>().currentLogData;
        if (cueLogData != null)
            if (ObjectState)
            {
                LogHelper.WriteLog($"{cueLogData._id};{cueLogData.startTimeOffset};{cueLogData.endTimeOffset};" +
                    $"{cueLogData.relevantForMeasurementEngine};{transform.position};null;{Time.time}");
            }
            else
            {
                LogHelper.WriteLog($"{cueLogData._id};{cueLogData.startTimeOffset};{cueLogData.endTimeOffset};" +
                    $"{cueLogData.relevantForMeasurementEngine};null;{transform.position};{Time.time}");
            }
    }
}
