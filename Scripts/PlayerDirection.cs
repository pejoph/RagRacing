///\=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=/\\\
///\                                                                   /\\\
///\  Filename  : PlayerDirection.cs								   /\\\
///\  			 													   /\\\
///\  Author(s) : Peter Phillips, Matthew Mason						   /\\\
///\     		 													   /\\\
///\  Date      : First entry - 25 / 01 / 2018						   /\\\
///\     	      Last entry  - 25 / 04 / 2018						   /\\\
///\                                                                   /\\\
///\  Brief     : Script for rotating player according to user input   /\\\
///\              movement controls.                                   /\\\
///\                                                                   /\\\
///\=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=/\\\


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDirection : MonoBehaviour
{
    [SerializeField] private float fSpeed;      ///turning power
    [SerializeField] private GameObject Hips;
    [SerializeField] private GameObject spine;

    private float fCurrentAngle;    ///angle the attached object is facing
    private float fTargetAngle;     ///angle of the movement input

    private void Start()
    {
        // Increase max angular velocity from Unity's standard value of 7 in order to achieve required torque.
        GetComponent<Rigidbody>().maxAngularVelocity = 20.0f;
    }

    private void FixedUpdate()
    {
        if (Hips.GetComponent<PlayerController>().vMovement != Vector3.zero && !spine.GetComponent<RagdollModeController>().isRagdoll)
        {
            // Convert vectors to angles.
            fCurrentAngle = Vector3.Angle(Vector3.forward, transform.forward);                               ///angle between global and local forward vectors
            fTargetAngle = Vector3.Angle(Vector3.forward, Hips.GetComponent<PlayerController>().vMovement);  ///angle between global forward vector and the user input movement vector
            
            // Convert angle range from 0-180° to 0-360°.
            if (transform.forward.x < 0)
            {
                fCurrentAngle = 360.0f - fCurrentAngle;
            }
            if (Hips.GetComponent<PlayerController>().vMovement.x < 0)
            {
                fTargetAngle = 360.0f - fTargetAngle;
            }
            
            // Prevent issues with player rotating the wrong way when angles wrap over from 360° to 0°.
            if (fCurrentAngle > fTargetAngle + 180.0f)
            {
                fTargetAngle += 360.0f;
            }
            else if (fCurrentAngle < fTargetAngle - 180.0f)
            {
                fCurrentAngle += 360.0f;
            }
            
            // Add torque proportional to angle offset in order to rotate player
            GetComponent<Rigidbody>().AddTorque(Vector3.up * (fTargetAngle - fCurrentAngle) * fSpeed);    ///negative numbers result in a negatve torque which rotates the object the opposite way.
        }
    }
}
