////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
///File:             GrabAndClimb.cs                                                                                     ///
///Author(s):        Matthew Mason, Peter Phillips                                                                       ///
///Date Created:     07-Feb-2018                                                                                         ///
///Last Edited:      08-May-2018                                                                                         ///
///Brief:            A Unity Script for Controlling the Grabbing of Objects and the Upwards Motion of the Character      ///
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
///  A unity script class for controlling the grabbing of objects and the upwards motion of the character
/// </summary>
public class GrabAndClimb : MonoBehaviour 
{
    
    #region Inspector Varibles
    /// <summary>
    /// Key used for when the player wants top grab
    /// </summary>
    [SerializeField] private bool IsLeftHand;
    /// <summary>
    /// The Player ragdolls other hand (not required)
    /// </summary>
	[SerializeField] GameObject OtherHandObject;
    /// <summary>
    /// The rigidbody of the player's main body segment (required)
    /// </summary>
    [SerializeField] private Rigidbody BodyRigidbody;
    #endregion

    #region Private Varibles
    private ConfigurableJoint OtherHandJoint;
	private ConfigurableJoint GrabAnchor;
	private ConfigurableJoint GrabbedObjectPivot;
    [HideInInspector] public GameObject PickedUpObject;
    private int PlayerNum;
    private bool Grounded = true;
    private bool CanJump = true;
    [HideInInspector] public bool GrabbingWall = false;
    [HideInInspector] public bool Gravity = true;
    private bool GrabbingObject = false;
    private SoftJointLimitSpring softJointLimitSpring;
    private string sJump;
    private string sGrab;
    //private string sLeftGrab;
    //private string sRightGrab;
    //private float PreviousFrameLeftGrab;
    //private float PreviousFramRightGrab;
    private float PreviousFrameGrab;
    #endregion

    void Start ()
	{
        //setting all the components of the game objects to the correct variables
        GrabAnchor = gameObject.GetComponent<ConfigurableJoint>();
		if (OtherHandObject != null)
		{
			OtherHandJoint = OtherHandObject.GetComponent<ConfigurableJoint>();
		}

        softJointLimitSpring.spring = 200;
        softJointLimitSpring.damper = 200;

        sJump = gameObject.layer == 8 ? "Jump" :
                gameObject.layer == 9 ? "Jump2" :
                gameObject.layer == 10 ? "Jump3" : "Jump4";

        sGrab = gameObject.layer == 8 ? "Grab" :
                    gameObject.layer == 9 ? "Grab2" :
                    gameObject.layer == 10 ? "Grab3" : "Grab4";

        if (IsLeftHand)
        {
            sGrab = "Left" + sGrab;
        }
        else
        {
            sGrab = "Right" + sGrab;
        }

        //sLeftGrab = gameObject.layer == 8 ? "LeftGrab" :
        //            gameObject.layer == 9 ? "LeftGrab2" :
        //            gameObject.layer == 10 ? "LeftGrab3" : "LeftGrab4";
        //
        //sRightGrab = gameObject.layer == 8 ? "RightGrab" :
        //             gameObject.layer == 9 ? "RightGrab2" :
        //             gameObject.layer == 10 ? "RightGrab3" : "RightGrab4";
    }
	
	void Update ()
	{
		if (Input.GetAxisRaw(sGrab) == 0 && PreviousFrameGrab != 0)
		{
            LetGo();
        }

        PreviousFrameGrab = Input.GetAxisRaw(sGrab);

        if (OtherHandJoint.xMotion == ConfigurableJointMotion.Free && GrabAnchor.xMotion == ConfigurableJointMotion.Free)
        {
            GrabbingWall = false;
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetAxisRaw(sGrab) != 0)
        {
            GrabObject(other);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (Input.GetAxisRaw(sGrab) != 0)
        {
            GrabObject(other);
        }
    }
    void FixedUpdate()
    {
        if (Input.GetAxisRaw(sJump) != 0)
        {
            JumpingOrClimbing();
        }
        if (Grounded == true)
        {
            CanJump = true;
        }
    }

    /// <summary>
    /// Makes the player jump or pull them self’s depending on if they are grabbing an object with no rigid body
    /// </summary>
    private void JumpingOrClimbing()
    {
        if (GrabbingWall == false)
        {
            if (CanJump == true)
            {
                BodyRigidbody.AddForce(Vector3.up * 20000);
                Grounded = false;
                CanJump = false;
            }
        } else if (GrabbingWall)
        {
            BodyRigidbody.AddForce(Vector3.up * 2000);
        }
    }

    /// <summary>
    /// Allow the player to grab and object there hand is next to.
    /// Other parmeter is the object the hand is collidng with.
    /// </summary>
    /// <param name="other"></param>
    private void GrabObject(Collider other)
    {
        //Checking its not grabbing its self
        if (other.gameObject.tag != "Player" && other.gameObject.tag != "CheckPoint" && other.gameObject.tag != "KilZone" && other.gameObject.tag != "WinZone")
        {
            if (!other.GetComponent<Rigidbody>())
            {
                if (!GrabbingObject)
                    {
                    if (GrabAnchor.xMotion == ConfigurableJointMotion.Free)
                    {
                        GrabAnchor.connectedAnchor = transform.position;
                        GrabAnchor.xMotion = ConfigurableJointMotion.Locked;
                        GrabAnchor.yMotion = ConfigurableJointMotion.Locked;
                        GrabAnchor.zMotion = ConfigurableJointMotion.Locked;
                        Debug.Log("grabwall true");
                        GrabbingWall = true;
                    }
                }
            }
            else
            {
                if (!GrabbingWall && !GrabbingObject)
                {
                    //Giving the other object a Locked configurable joint if it is 
                    Debug.Log(other.name);
                    // Turn off gravity on object picked up.
                    PickedUpObject = other.gameObject;
                    ///PickedUpObject.useGravity = false;
                    GrabbedObjectPivot = other.gameObject.AddComponent<ConfigurableJoint>();
                    GrabbedObjectPivot.connectedBody = gameObject.GetComponent<Rigidbody>();
                    GrabbedObjectPivot.connectedAnchor = transform.position;
                    GrabbedObjectPivot.xMotion = ConfigurableJointMotion.Locked;
                    GrabbedObjectPivot.yMotion = ConfigurableJointMotion.Locked;
                    GrabbedObjectPivot.zMotion = ConfigurableJointMotion.Locked;
                    GrabbedObjectPivot.enablePreprocessing = false;
                    GrabbedObjectPivot.linearLimitSpring = softJointLimitSpring;
                    GrabbingObject = true;
                    Gravity = false;
                }
            }
        }
    }

    /// <summary>
    /// Checks if the player is holding somthing (By checking if the x Joint is locked) and lets of if they are
    /// </summary>
    private void LetGo()
    {
        //letting go of the object if it is without a rigidbody but unlocking the joint
        if (GrabAnchor.xMotion == ConfigurableJointMotion.Locked)
        {
            //Debug.Log("hand unlocked");
            GrabAnchor.xMotion = ConfigurableJointMotion.Free;
            GrabAnchor.yMotion = ConfigurableJointMotion.Free;
            GrabAnchor.zMotion = ConfigurableJointMotion.Free;
            if (OtherHandObject == null)
            {
                //Debug.Log("grab false 1");
                GrabbingWall = false;
            } else if (OtherHandJoint.xMotion == ConfigurableJointMotion.Free)
            {
                //Debug.Log("grab false 2"); 
                GrabbingWall = false;
            }
        //Destroying the pivot on the game object it is grabbing
        } else if (GrabbedObjectPivot != null)
        {
            // Turn gravity of the object let go back on.
            ///PickedUpObject.useGravity = true;
            PickedUpObject = null;
            GrabbingObject = false;
            Destroy(GrabbedObjectPivot);
            GrabbedObjectPivot = null;
            Gravity = true;
        }
    }

    /// <summary>
    /// Checking if it collided with the floor
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
            Grounded = true;
    }
}
