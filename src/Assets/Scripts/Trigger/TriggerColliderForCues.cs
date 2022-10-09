using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerColliderForCues : MonoBehaviour
{
    private GameObject cueToTrigger, endTrigger;
    private bool isStartTrigger;

    public void ArrangeTheTrigger(GameObject refCue, GameObject endTriggerObj = null, bool isStartTriggerObj = false)
    {
        cueToTrigger = refCue;
        endTrigger = endTriggerObj;
        isStartTrigger = isStartTriggerObj;
        GetComponent<BoxCollider>().isTrigger = true;
        if (endTrigger != null)
            endTrigger.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (cueToTrigger != null)
            {
                SaveLog(isStartTrigger);
                cueToTrigger.SetActive(isStartTrigger);
            }
            if (endTrigger != null)
            {
                endTrigger.SetActive(true);
            }
            Destroy(gameObject);
        }
    }

    private void SaveLog(bool ObjectState)
    {
        
        Logger cueLogData = cueToTrigger.GetComponent<CueData>().currentLogData;
        if (cueLogData != null)
            LogHelper.WriteLog("\n \n \n ID: " + cueLogData._id +
                " ::::: StartTimeOffset: " + cueLogData.startTimeOffset +
                " ::::: EndTimeOffSet: " + cueLogData.endTimeOffset +
                " :::: relevantForMeasurementEngine: " + cueLogData.relevantForMeasurementEngine +
                " :::: " + (ObjectState ? "StartTriggerPostion: ":"EndTriggerPostion: ") +transform.position +
                " :::: Time of Interraction" + Time.time);
        
    }
}
