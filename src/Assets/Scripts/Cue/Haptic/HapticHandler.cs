using System.Collections;
using System.Collections.Generic;
using Unity.XR.PXR;
using UnityEngine;
using UnityEngine.InputSystem.Haptics;

public class HapticHandler : MonoBehaviour
{
    private float hapticAmp;
    private int hapticDuration;
    private PXR_Input.Controller controller;
    private void OnEnable()
    {
        PXR_Input.SetControllerVibration(hapticAmp, hapticDuration, controller);
    }
    private void OnDisable()
    {
        PXR_Input.SetControllerVibration(hapticAmp, 0, controller);
    }

    public void CreateHaptic(float hapticStrength, int hapticTime, PXR_Input.Controller hand)
    {
        hapticAmp = hapticStrength;
        hapticDuration = hapticTime;
        controller = hand;
    }
}
