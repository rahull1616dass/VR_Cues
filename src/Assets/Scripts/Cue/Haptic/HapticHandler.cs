
using System.Net.NetworkInformation;
using Unity.XR.PXR;
using UnityEngine;

public class HapticHandler : MonoBehaviour
{
    private float hapticAmp;
    private int hapticDuration;
    private PXR_Input.Controller controller = PXR_Input.Controller.LeftController;
    private bool parameterSet = false;

    public void CreateHaptic(float hapticStrength, int hapticTime, PXR_Input.Controller hand)
    {
        hapticAmp = hapticStrength;
        hapticDuration = hapticTime;
        controller = hand;
        parameterSet = true;
    }

    private void Update()
    {
        if (parameterSet)
            PXR_Input.SetControllerVibration(1, 1000, PXR_Input.Controller.LeftController);
    }
}
