///\=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=/\\\
///\                                                                   /\\\
///\  Filename  : JumpController.cs 								   /\\\
///\  			 													   /\\\
///\  Author(s) : Peter Phillips            				    	   /\\\
///\     		 													   /\\\
///\  Date      : First entry - 06 / 02 / 2018						   /\\\
///\     	      Last entry  - 05 / 03 / 2018						   /\\\
///\                                                                   /\\\
///\  Brief     : ==================================================   /\\\
///\              ==================================================   /\\\
///\                                                                   /\\\
///\=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=/\\\


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpController : MonoBehaviour
{
    [SerializeField] private float fJump;
    [SerializeField] private GameObject rightHand;
    [SerializeField] private GameObject leftHand;
    [SerializeField] private GameObject spine;

    private float fJumpCharge;
    private GrabAndClimb right;
    private GrabAndClimb left;
    private string sJump;

    void Start ()
    {
        fJumpCharge = 0.0f;
        right = rightHand.GetComponent<GrabAndClimb>();
        left = leftHand.GetComponent<GrabAndClimb>();

        sJump = gameObject.layer == 8 ? "Jump" :
                gameObject.layer == 9 ? "Jump2" :
                gameObject.layer == 10 ? "Jump3" : "Jump4";
    }
	
	void Update ()
    {
        if (fJumpCharge < 1 && GetComponent<Rigidbody>().velocity.y > -0.1f && GetComponent<Rigidbody>().velocity.y < 0.1f)
        {
            fJumpCharge += 10 * Time.deltaTime;
            fJumpCharge = Mathf.Clamp(fJumpCharge, 0.0f, 1.0f);
        }
    }    

    private void FixedUpdate()
    {
        if (GetComponent<Rigidbody>().velocity.y < -.5f)
        {
            GetComponent<Rigidbody>().AddForce(400.0f * Physics.gravity);
        }

        if (!spine.GetComponent<RagdollModeController>().isRagdoll && fJumpCharge >= 1.0f && Input.GetAxisRaw(sJump) != 0 && !right.GrabbingWall && !left.GrabbingWall)
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * fJump);
            fJumpCharge = 0.0f;
        }
    }
}
