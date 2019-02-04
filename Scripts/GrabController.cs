///\=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=/\\\
///\                                                                   /\\\
///\  Filename  : GrabController.cs 	  							   /\\\
///\  			 													   /\\\
///\  Author(s) : Peter Phillips            				    	   /\\\
///\     		 													   /\\\
///\  Date      : First entry - 07 / 02 / 2018						   /\\\
///\     	      Last entry  - 19 / 04 / 2018						   /\\\
///\                                                                   /\\\
///\  Brief     : ==================================================   /\\\
///\              ==================================================   /\\\
///\                                                                   /\\\
///\=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=/\\\


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabController : MonoBehaviour
{
    [SerializeField] private GameObject leftHand;
    [SerializeField] private GameObject rightHand;
    [SerializeField] private GameObject leftFoot;
    [SerializeField] private GameObject rightFoot;
    [SerializeField] private GameObject leftShoulder;
    [SerializeField] private GameObject rightShoulder;

    private string sLeftRaise;
    private string sRightRaise;
    private string sLeftGrab;
    private string sRightGrab;

    private void Start()
    {
        sLeftRaise = gameObject.layer == 8 ? "LeftRaise" :
                     gameObject.layer == 9 ? "LeftRaise2" :
                     gameObject.layer == 10 ? "LeftRaise3" : "LeftRaise4";
        sRightRaise = gameObject.layer == 8 ? "RightRaise" :
                      gameObject.layer == 9 ? "RightRaise2" :
                      gameObject.layer == 10 ? "RightRaise3" : "RightRaise4";
        sLeftGrab = gameObject.layer == 8 ? "LeftGrab" :
                    gameObject.layer == 9 ? "LeftGrab2" :
                    gameObject.layer == 10 ? "LeftGrab3" : "LeftGrab4";
        sRightGrab = gameObject.layer == 8 ? "RightGrab" :
                     gameObject.layer == 9 ? "RightGrab2" :
                     gameObject.layer == 10 ? "RightGrab3" : "RightGrab4";
    }

    void Update ()
    {
        if (Input.GetAxisRaw(sLeftRaise) != 0)
        {
            //lift
            leftHand.GetComponent<ConstantForce>().force = Vector3.up * 500;            
            leftFoot.GetComponent<ConstantForce>().force = Vector3.down * 500;            
            leftShoulder.GetComponent<ConstantForce>().force = Vector3.zero;
        }
        else if (Input.GetAxisRaw(sLeftGrab) != 0)
        {
            //grab
            leftHand.GetComponent<ConstantForce>().force = transform.forward * 500;
            leftShoulder.GetComponent<ConstantForce>().force = -transform.forward * 500;
        }
        else
        {
            //Don't
            leftHand.GetComponent<ConstantForce>().force = Vector3.zero;
            leftFoot.GetComponent<ConstantForce>().force = Vector3.zero;
            leftShoulder.GetComponent<ConstantForce>().force = Vector3.zero;
        }

        if (Input.GetAxisRaw(sRightRaise) != 0)
        {
            //lift            
            rightHand.GetComponent<ConstantForce>().force = Vector3.up * 500;            
            rightFoot.GetComponent<ConstantForce>().force = Vector3.down * 500;
            rightShoulder.GetComponent<ConstantForce>().force = Vector3.zero;
        }
        else if (Input.GetAxisRaw(sRightGrab) != 0)
        {
            //grab
            rightHand.GetComponent<ConstantForce>().force = transform.forward * 500;
            rightShoulder.GetComponent<ConstantForce>().force = -transform.forward * 500;
        }
        else
        {
            //don't
            rightHand.GetComponent<ConstantForce>().force = Vector3.zero;
            rightFoot.GetComponent<ConstantForce>().force = Vector3.zero;
            rightShoulder.GetComponent<ConstantForce>().force = Vector3.zero;
        }        
    }
}
