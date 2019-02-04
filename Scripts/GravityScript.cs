///\=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=/\\\
///\                                                                   /\\\
///\  Filename  : GravityScript.cs 	    							   /\\\
///\  			 													   /\\\
///\  Author(s) : Peter Phillips            				    	   /\\\
///\     		 													   /\\\
///\  Date      : First entry - 08 / 05 / 2018						   /\\\
///\     	      Last entry  - 08 / 05 / 2018						   /\\\
///\                                                                   /\\\
///\  Brief     : ==================================================   /\\\
///\              ==================================================   /\\\
///\                                                                   /\\\
///\=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=/\\\


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityScript : MonoBehaviour
{
    [SerializeField] private GameObject RightHand;
    [SerializeField] private GameObject LeftHand;

    private float Drag;

    private void Start()
    {
        Drag = GetComponent<Rigidbody>().drag;
    }

    void Update ()
    {
        if (!RightHand.GetComponent<GrabAndClimb>().Gravity || !LeftHand.GetComponent<GrabAndClimb>().Gravity)
        {
            GetComponent<Rigidbody>().useGravity = false;
            GetComponent<Rigidbody>().drag = 0;
        }
        else
        {
            GetComponent<Rigidbody>().useGravity = true;
            GetComponent<Rigidbody>().drag = Drag;
        }
	}
}
