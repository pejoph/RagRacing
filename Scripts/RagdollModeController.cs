///\=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=/\\\
///\                                                                   /\\\
///\  Filename  : RagdollModeController.cs  						   /\\\
///\  			 													   /\\\
///\  Author(s) : Peter Phillips            				    	   /\\\
///\     		 													   /\\\
///\  Date      : First entry - 07 / 02 / 2018						   /\\\
///\     	      Last entry  - 05 / 03 / 2018						   /\\\
///\                                                                   /\\\
///\  Brief     : ==================================================   /\\\
///\              ==================================================   /\\\
///\                                                                   /\\\
///\=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=/\\\


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollModeController : MonoBehaviour
{
    public bool isRagdoll;

    private Vector3 initialForce;
    private string sRagdoll;

    void Start()
    {
        initialForce = GetComponent<ConstantForce>().force;
        isRagdoll = false;

        sRagdoll = gameObject.layer == 8 ? "Ragdoll" :
                   gameObject.layer == 9 ? "Ragdoll2" :
                   gameObject.layer == 10 ? "Ragdoll3" : "Ragdoll4";
    }

    private void FixedUpdate()
    {
        if (Input.GetAxisRaw(sRagdoll) != 0.0f)
        {
            GetComponent<ConstantForce>().force = new Vector3(0, 0, 0);
            isRagdoll = true;
        }

        else
        {
            GetComponent<ConstantForce>().force = initialForce;
            isRagdoll = false;
        }
    }
}
