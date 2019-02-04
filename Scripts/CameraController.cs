///\=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=/\\\
///\                                                                   /\\\
///\  Filename  : CameraController.cs 								   /\\\
///\  			 													   /\\\
///\  Author(s) : Peter Phillips            				    	   /\\\
///\     		 													   /\\\
///\  Date      : First entry - 07 / 02 / 2018						   /\\\
///\     	      Last entry  - 18 / 02 / 2018						   /\\\
///\                                                                   /\\\
///\  Brief     : ==================================================   /\\\
///\              ==================================================   /\\\
///\                                                                   /\\\
///\=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=/\\\


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject target;
    
    void LateUpdate ()
    {
        transform.position += (target.transform.position - transform.position) / 30.0f;
    }    
}
