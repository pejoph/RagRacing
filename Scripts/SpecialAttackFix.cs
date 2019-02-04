///\=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=/\\\
///\                                                                   /\\\
///\  Filename  : SpecialAttackFix.cs 	  							   /\\\
///\  			 													   /\\\
///\  Author(s) : Peter Phillips            				    	   /\\\
///\     		 													   /\\\
///\  Date      : First entry - 19 / 04 / 2018						   /\\\
///\     	      Last entry  - 19 / 04 / 2018						   /\\\
///\                                                                   /\\\
///\  Brief     : ==================================================   /\\\
///\              ==================================================   /\\\
///\                                                                   /\\\
///\=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=/\\\


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAttackFix : MonoBehaviour
{
	void Update ()
    {
		if (GetComponent<Rigidbody>().velocity.magnitude > 10)
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
	}
}
