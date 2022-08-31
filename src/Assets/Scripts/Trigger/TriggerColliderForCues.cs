using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerColliderForCues : MonoBehaviour
{
    [HideInInspector] public GameObject cueToTrigger;
    private void OnTriggerEnter(Collider other)
    {
        cueToTrigger.SetActive(true);
        Destroy(gameObject);
    }
}
