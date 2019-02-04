///\=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=/\\\
///\                                                                   /\\\
///\  Filename  : PendulumFix.cs 	    							   /\\\
///\  			 													   /\\\
///\  Author(s) : Peter Phillips              				    	   /\\\
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

public class PendulumFix : MonoBehaviour
{
    [SerializeField] private GameObject RightHand;
    [SerializeField] private GameObject LeftHand;

    private Vector3 Distance;

    void Start ()
    {
        Distance = Vector3.zero;
	}
	
	void LateUpdate ()
    {
        if (!RightHand.GetComponent<GrabAndClimb>().Gravity)
        {
            if (Distance == Vector3.zero)
            {
                Distance = RightHand.GetComponent<GrabAndClimb>().PickedUpObject.transform.position - transform.position;
            }

            transform.position = RightHand.GetComponent<GrabAndClimb>().PickedUpObject.transform.position - Distance;
        }
        else if (!LeftHand.GetComponent<GrabAndClimb>().Gravity)
        {
            if (Distance == Vector3.zero)
            {
                Distance = LeftHand.GetComponent<GrabAndClimb>().PickedUpObject.transform.position - transform.position;
            }

            transform.position = LeftHand.GetComponent<GrabAndClimb>().PickedUpObject.transform.position - Distance;
        }
        else
        {
            Distance = Vector3.zero;
        }
    }
}
