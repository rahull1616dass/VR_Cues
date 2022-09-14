
using System.Net.NetworkInformation;
using Unity.XR.PXR;
using UnityEngine;

public class HapticHandler : MonoBehaviour
{
    private float hapticAmp;
    private const int DEFAULT_DURATION = 1000;
    private PXR_Input.Controller controller = PXR_Input.Controller.LeftController;
    private bool parameterSet = false;

    public void CreateHaptic(float hapticStrength, PXR_Input.Controller hand)
    {
        hapticAmp = hapticStrength;
        controller = hand;
        parameterSet = true;
    }

    private void Update()
    {
        if (parameterSet)
            PXR_Input.SetControllerVibration(1, DEFAULT_DURATION, PXR_Input.Controller.LeftController);
    }
}
