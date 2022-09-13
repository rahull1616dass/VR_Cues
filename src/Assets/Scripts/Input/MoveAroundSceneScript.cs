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
    private Vector2 currentMoveDirection;
    private Vector3 currentRotateValue;
    private Transform playerTransform;

    private bool StartMovement;

    [SerializeField] private float rotateVal = 30f;
    [SerializeField] private float speed = 1f;
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

        movementInput.LeftHand.MoveAround.canceled += MoveAround_canceled;

    }

    private void MoveAround_canceled(InputAction.CallbackContext obj)
    {
        StartMovement = false;
    }

    private void OnDisable()
    {
        movementInput.Disable();

        movementInput.LeftHand.MoveAround.performed -= MoveAroundScene;

        movementInput.RightHand.RotateAround.performed -= RotateAroundScene;

        movementInput.LeftHand.MoveAround.canceled += MoveAround_canceled;
    }

    private void RotateAroundScene(InputAction.CallbackContext obj)
    {
        Debug.Log(obj.ReadValue<Vector2>());
        playerTransform.localEulerAngles += GetDirectionFromVector2(obj.ReadValue<Vector2>());
    }

    private void MoveAroundScene(InputAction.CallbackContext obj)
    {
        Debug.Log(obj.ReadValue<Vector2>());
        currentMoveDirection = obj.ReadValue<Vector2>();
        StartMovement = true;
    }



    private Vector3 GetDirectionFromVector2(Vector2 valueFromInput)
    {
        if (valueFromInput == Vector2.zero)
            return Vector3.zero;
        else if (valueFromInput.x > 0 && Mathf.Abs(valueFromInput.x) > Mathf.Abs(valueFromInput.y))
            return Vector3.up * rotateVal;
        else if (valueFromInput.x < 0 && Mathf.Abs(valueFromInput.x) > Mathf.Abs(valueFromInput.y))
            return Vector3.down * rotateVal;
        else if (valueFromInput.y > 0 && Mathf.Abs(valueFromInput.x) < Mathf.Abs(valueFromInput.y))
            return Vector3.left * rotateVal;
        else if (valueFromInput.y < 0 && Mathf.Abs(valueFromInput.x) < Mathf.Abs(valueFromInput.y))
            return Vector3.right * rotateVal;
        else
            return Vector3.zero;
    }

    private void Update()
    {
        if (StartMovement)
        {
            transform.localPosition += transform.forward * currentMoveDirection.y * speed;
            transform.localPosition += transform.right * currentMoveDirection.x * speed;
        }  
    }
}
