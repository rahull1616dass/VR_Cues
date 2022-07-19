using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveAroundSceneScript : MonoBehaviour
{
    enum RotateDirection
    {
        None=0,
        Up,
        Down,
        Left,
        Right
    }
    private VRInput movementInput;
    private Transform playerTransform;

    [SerializeField] private float rotateVal = 30f;
    private void Awake()
    {
        playerTransform = transform;
        movementInput = new VRInput();
    }

    private void OnEnable()
    {
        movementInput.Enable();

        movementInput.LeftHand.MoveAround.performed += MoveAroundScene;

        movementInput.RightHand.RotateAround.performed += RotateAroundScene;

    }

    private void OnDisable()
    {
        movementInput.Disable();

        movementInput.LeftHand.MoveAround.performed -= MoveAroundScene;

        movementInput.RightHand.RotateAround.performed -= RotateAroundScene;
    }

    private void RotateAroundScene(InputAction.CallbackContext obj)
    {
        playerTransform.localEulerAngles = GetDirectionFromVector2(obj.ReadValue<Vector2>());
    }

    private void MoveAroundScene(InputAction.CallbackContext obj)
    {
        Vector2 directionVector = obj.ReadValue<Vector2>();
        transform.localPosition = new Vector3(transform.localPosition.x + directionVector.x, transform.localPosition.y, transform.localPosition.z + directionVector.y);
    }

    private Vector3 GetDirectionFromVector2(Vector2 valueFromInput)
    {
        if (valueFromInput.x > 0 && Mathf.Abs(valueFromInput.x) > Mathf.Abs(valueFromInput.y))
            return new Vector3(playerTransform.localEulerAngles.x, 
                                    playerTransform.localEulerAngles.y + rotateVal, 
                                    playerTransform.localEulerAngles.z);
        else if (valueFromInput.x < 0 && Mathf.Abs(valueFromInput.x) > Mathf.Abs(valueFromInput.y))
            return new Vector3(playerTransform.localEulerAngles.x,
                                    playerTransform.localEulerAngles.y - rotateVal,
                                    playerTransform.localEulerAngles.z);
        else if (valueFromInput.y > 0 && Mathf.Abs(valueFromInput.x) < Mathf.Abs(valueFromInput.y))
            return new Vector3(playerTransform.localEulerAngles.x - rotateVal,
                                    playerTransform.localEulerAngles.y,
                                    playerTransform.localEulerAngles.z);
        else if (valueFromInput.y < 0 && Mathf.Abs(valueFromInput.x) < Mathf.Abs(valueFromInput.y))
            return new Vector3(playerTransform.localEulerAngles.x + rotateVal,
                                    playerTransform.localEulerAngles.y,
                                    playerTransform.localEulerAngles.z);
        else
            return new Vector3(playerTransform.localEulerAngles.x,
                                    playerTransform.localEulerAngles.y,
                                    playerTransform.localEulerAngles.z);
    }
}
