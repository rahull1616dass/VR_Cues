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
    }
    private void OnTriggerEnter(Collider other)
    {
        if (cueToTrigger != null)
            cueToTrigger.SetActive(isStartTrigger);
        if (endTrigger!= null)
        {
            endTrigger.SetActive(true);
        }
        Destroy(gameObject);
    }
}
