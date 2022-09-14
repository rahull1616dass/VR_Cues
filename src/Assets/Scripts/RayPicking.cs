using System;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class RayPicking : MonoBehaviour
{
    public float translationIncrement = 0.1f;
    public float rotationIncrement = 1.0f;
    public float thumbstickMovementThreshold = 0.5f;  // The thumbstick requires some threshold to pass until we consider a movement to have started.
    public string RayCollisionLayer = "Default";
    public bool PickedUpObjectPositionNotControlledByPhysics = true; //otherwise object position will be still computed by physics engine, even when attached to ray
    public Vector3 scalingIncrement = new Vector3(0.1f, 0.1f, 0.1f);

    private GameObject rightHandController;
    private GameObject trackingSpaceRoot;

    private RaycastHit lastRayCastHit;
    private bool bWasTriggerButtonPressed = false;
    private bool bWasJoystickButtonPressed = false;

    private GameObject objectPickedUP = null;
    private GameObject previousObjectCollidingWithRay = null;
    private GameObject lastObjectCollidingWithRay = null;
    private bool IsThereAnewObjectCollidingWithRay = false;

    [SerializeField] private InputActionProperty action;

    private VRInput movementInput;

    /// 
    ///  Events
    /// 

    void Start()
    {
        GetRightHandDevice();
        GetRighHandController();
        GetTrackingSpaceRoot();
    }

    private void OnEnable()
    {
        movementInput = new VRInput();
        movementInput.Enable();
        movementInput.RightHand.SelectObject.performed += TriggerPressed;
    }
    private void OnDisable()
    {
        movementInput.RightHand.SelectObject.performed -= TriggerPressed;
    }

    private void TriggerPressed(InputAction.CallbackContext obj)
    {
        Debug.Log("ClickingTrigger");
        if (objectPickedUP != null) // already pick up an object?
        {
            if (PickedUpObjectPositionNotControlledByPhysics)
            {
                Rigidbody rb = objectPickedUP.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.isKinematic = false;
                }
            }
            objectPickedUP.transform.parent = null;
            objectPickedUP = null;
            Debug.Log("Object released: " + objectPickedUP);
        }
        else
        {
            GenerateSound();
            GenerateVibrations();

            objectPickedUP = lastRayCastHit.collider.gameObject;
            objectPickedUP.transform.parent = gameObject.transform; // see Transform.parent https://docs.unity3d.com/ScriptReference/Transform-parent.html?_ga=2.21222203.1039085328.1595859162-225834982.1593000816
            if (PickedUpObjectPositionNotControlledByPhysics)
            {
                Rigidbody rb = objectPickedUP.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.isKinematic = true;
                }
            }
            Debug.Log("Object Picked up:" + objectPickedUP);
        }

    }

    void Update()
    {
        if (objectPickedUP == null)
        {
            GetTargetedObjectCollidingWithRayCasting();
            UpdateObjectCollidingWithRay();
            UpdateFlagNewObjectCollidingWithRay();
            OutlineObjectCollidingWithRay();
        }
        MoveTargetedObjectAlongRay();
        RotateTargetedObjectOnLocalUpAxis();
    }


    /// 
    ///  Start Functions (to get VR Devices)
    /// 

    private void GetRightHandDevice()
    {
    }
    private void GetTrackingSpaceRoot()
    {
        var XRRig = GetComponentInParent<XROrigin>(); // i.e Roomscale tracking space 
        trackingSpaceRoot = XRRig.gameObject; // Gameobject representing the center of tracking space in virtual enviroment
    }

    private void GetRighHandController()
    {
        rightHandController = gameObject; // i.e. with this script component and an XR controller component
    }

    /// 
    ///  Update Functions 
    /// 

    private void GetTargetedObjectCollidingWithRayCasting()
    {
        // see raycast example from https://docs.unity3d.com/ScriptReference/Physics.Raycast.html
        if (Physics.Raycast(transform.position,
            transform.TransformDirection(Vector3.forward),
            out RaycastHit hit,
            Mathf.Infinity,
            1 << LayerMask.NameToLayer(RayCollisionLayer))) // 1 << because must use bit shifting to get final mask!
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            // Debug.Log("Ray collided with:  " + hit.collider.gameObject + " collision point: " + hit.point);
            Debug.DrawLine(hit.point, (hit.point + hit.normal * 2));
            lastRayCastHit = hit;
        }
    }

    private void UpdateObjectCollidingWithRay()
    {
        if (lastRayCastHit.collider != null)
        {
            GameObject currentObjectCollidingWithRay = lastRayCastHit.collider.gameObject;
            if (lastObjectCollidingWithRay != currentObjectCollidingWithRay)
            {
                previousObjectCollidingWithRay = lastObjectCollidingWithRay;
                lastObjectCollidingWithRay = currentObjectCollidingWithRay;
            }
        }
    }
    private void UpdateFlagNewObjectCollidingWithRay()
    {
        if (lastObjectCollidingWithRay != previousObjectCollidingWithRay)
        {
            IsThereAnewObjectCollidingWithRay = true;
        }
        else
        {
            IsThereAnewObjectCollidingWithRay = false;
        }
    }
    private void OutlineObjectCollidingWithRay()
    {
        if (IsThereAnewObjectCollidingWithRay)
        {
            //add outline to new one
            if (lastObjectCollidingWithRay != null)
            {
                var outliner = lastObjectCollidingWithRay.GetComponent<OutlineModified>();
                if (outliner == null) // if not, we will add a component to be able to outline it
                {
                    //Debug.Log("Outliner added t" + lastObjectCollidingWithRay.gameObject.ToString());
                    outliner = lastObjectCollidingWithRay.AddComponent<OutlineModified>();
                }

                if (outliner != null)
                {
                    outliner.enabled = true;
                    //Debug.Log("outline new object color"+ lastObjectCollidingWithRay);
                }
                // remove outline from previous one
                //add outline new one
                if (previousObjectCollidingWithRay != null)
                {
                    outliner = previousObjectCollidingWithRay.GetComponent<OutlineModified>();
                    if (outliner != null)
                    {
                        outliner.enabled = false;
                        //Debug.Log("outline new object color"+ previousObjectCollidingWithRay);
                    }
                }
            }

        }
    }

    private void MoveTargetedObjectAlongRay()
    {
        
    }

    private void RotateTargetedObjectOnLocalUpAxis()
    {
        
    }

    private void GenerateVibrations()
    {
    }

    private void GenerateSound()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        if (audioSource != null)
        {
            audioSource.Play();
        }
        else
        {
            Debug.LogError("No Audio Source Found!");
        }
    }

}
