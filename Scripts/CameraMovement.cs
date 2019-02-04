using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CameraMovement : MonoBehaviour 
{
    enum CurrentLookDirection 
    {
        Forward,
        Left,
        Back,
        Right
    };
    [SerializeField] private KeyCode PanLeftKey = KeyCode.A;
    [SerializeField] private KeyCode PanRightKey = KeyCode.D;
    [SerializeField] private KeyCode PanForwardsKey = KeyCode.W;
    [SerializeField] private KeyCode PanBackKey = KeyCode.S;
    [SerializeField] private KeyCode RotateRightKey = KeyCode.E;
    [SerializeField] private KeyCode RotateLeftKey = KeyCode.Q;
    [SerializeField] private float PanSpeed = 20;
    [SerializeField] private float ZoomSpeed = 20;
    private Vector3 LeftRotation = new Vector3(0,90,0);
    private Vector3 RightRotation = new Vector3(0,90,0);
    private Vector3 StartRotation = new Vector3(45,0,0);
    private Camera Camera;
    private int FacingDirection = (int)CurrentLookDirection.Forward;


    #region RotationVaribles
    private bool IsRotatingRight = false;
    private bool IsRotatingLeft = false;
    private int RotationTicks = 0;
    private int RotationIncrement = 10;
    #endregion

    private void Start()
    {
        Camera = gameObject.GetComponent<Camera>();
        transform.Rotate(StartRotation);
    }

    // Update is called once per frame
    void Update ()
    {
        if (Input.GetKey(PanLeftKey))
        {
            switch (FacingDirection)
            {
                case (int)CurrentLookDirection.Forward:
                    PanningLeft();
                    break;
                case (int)CurrentLookDirection.Left:
                    PanningForwards();
                    break;
                case (int)CurrentLookDirection.Back:
                    PanningRight();
                    break;
                case (int)CurrentLookDirection.Right:
                    PanningBack();
                    break;
            } 
        }
        if (Input.GetKey(PanRightKey))
        {
            switch (FacingDirection)
            {
                case (int)CurrentLookDirection.Forward:
                    PanningRight();
                    break;
                case (int)CurrentLookDirection.Left:
                    PanningBack();
                    break;
                case (int)CurrentLookDirection.Back:
                    PanningLeft();
                    break;
                case (int)CurrentLookDirection.Right:
                    PanningForwards();
                    break;
            }
        }
        if (Input.GetKey(PanForwardsKey))
        {
            switch (FacingDirection)
            {
                case (int)CurrentLookDirection.Forward:
                    PanningForwards();
                    break;
                case (int)CurrentLookDirection.Left:
                    PanningRight();
                    break;
                case (int)CurrentLookDirection.Back:
                    PanningBack();
                    break;
                case (int)CurrentLookDirection.Right:
                    PanningLeft();
                    break;
            }
        }
        if (Input.GetKey(PanBackKey))
        {
            switch (FacingDirection)
            {
                case (int)CurrentLookDirection.Forward:
                    PanningBack();
                    break;
                case (int)CurrentLookDirection.Left:
                    PanningLeft();
                    break;
                case (int)CurrentLookDirection.Back:
                    PanningForwards();
                    break;
                case (int)CurrentLookDirection.Right:
                    PanningRight();
                    break;
            }
        }
        if (Input.GetKeyDown(RotateLeftKey))
        {
            IsRotatingLeft = true;
        }
        if (Input.GetKeyDown(RotateRightKey))
        {
            IsRotatingRight = true;
        }
        if (IsRotatingLeft == true)
        {
            RotatingLeft();
        }
        if (IsRotatingRight == true)
        {
            RotatingRight();
        }
        Zoom();
    }

    private void PanningLeft()
    {
        Vector3 NextStep = new Vector3(PanSpeed * Time.deltaTime, 0, 0);
        gameObject.transform.position -= NextStep;
    }
    private void PanningRight()
    {
        Vector3 NextStep = new Vector3(PanSpeed * Time.deltaTime, 0, 0);
        gameObject.transform.position += NextStep;
    }
    private void PanningForwards()
    {
        Vector3 NextStep = new Vector3(0, 0, PanSpeed * Time.deltaTime);
        gameObject.transform.position += NextStep;
    }
    private void PanningBack()
    {
        Vector3 NextStep = new Vector3(0, 0, PanSpeed * Time.deltaTime);
        gameObject.transform.position -= NextStep;
    }
    private void RotatingLeft()
    {
        if (RotationTicks < 90)
        {
            Vector3 RotationPoint = transform.InverseTransformPoint(transform.position + new Vector3(0, -1, 0));
            gameObject.transform.RotateAround(RotationPoint, Vector3.up, RotationIncrement);
            RotationTicks += RotationIncrement;
        }
        else
        {
            IsRotatingLeft = false;
            RotationTicks = 0;
            if (FacingDirection < (int)CurrentLookDirection.Right)
            {
                FacingDirection += 1;
                Debug.Log(FacingDirection);
            }
            else
            {
                FacingDirection = (int)CurrentLookDirection.Forward;
                Debug.Log(FacingDirection);
            }
        }
        
    }
    private void RotatingRight()
    {
        if (RotationTicks < 90)
        {
            Vector3 RotationPoint = transform.InverseTransformPoint(transform.position + new Vector3(0, -1, 0));
            gameObject.transform.RotateAround(RotationPoint, Vector3.up, -RotationIncrement);
            RotationTicks += RotationIncrement;
        } else
        {
            IsRotatingRight = false;
            RotationTicks = 0;
            if (FacingDirection > (int)CurrentLookDirection.Forward)
            {
                FacingDirection -= 1;
                Debug.Log(FacingDirection);
            } else
            {
                FacingDirection = (int)CurrentLookDirection.Right;
                Debug.Log(FacingDirection);
            }
        }
        
    }

    private void Zoom()
    {
        Vector3 NextStep = new Vector3(0,0, Input.GetAxis("Mouse ScrollWheel"));
        gameObject.transform.Translate((NextStep * ZoomSpeed) * Time.deltaTime);
    }
}
