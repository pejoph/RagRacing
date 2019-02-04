///\=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=/\\\
///\                                                                   /\\\
///\  Filename  : PlayerController.cs 								   /\\\
///\  			 													   /\\\
///\  Author(s) : Peter Phillips, Matthew Mason				    	   /\\\
///\     		 													   /\\\
///\  Date      : First entry - 25 / 01 / 2018						   /\\\
///\     	      Last entry  - 25 / 04 / 2018						   /\\\
///\                                                                   /\\\
///\  Brief     : ==================================================   /\\\
///\              ==================================================   /\\\
///\                                                                   /\\\
///\=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=/\\\


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Vector3 vMovement;

    public float fSpeed;
    public float fAltSpeed;

    [SerializeField] private float fUpSpeed;
    [SerializeField] private GameObject spine;

    private float fHorizontal;
    private float fVertical;
    private float fOriginalSpeed;
    private float fOriginalAltSpeed;
    private string sSprint;
    private string sHorizontal;
    private string sVertical;

    void Start ()
    {
        vMovement = Vector3.zero;
        fOriginalSpeed = fSpeed;
        fOriginalAltSpeed = fAltSpeed;

        sSprint = gameObject.layer == 8 ? "Sprint" :
                  gameObject.layer == 9 ? "Sprint2" :
                  gameObject.layer == 10 ? "Sprint3" : "Sprint4";
        sHorizontal = gameObject.layer == 8 ? "Horizontal" :
                      gameObject.layer == 9 ? "Horizontal2" :
                      gameObject.layer == 10 ? "Horizontal3" : "Horizontal4";
        sVertical = gameObject.layer == 8 ? "Vertical" :
                    gameObject.layer == 9 ? "Vertical2" :
                    gameObject.layer == 10 ? "Vertical3" : "Vertical4";
    }

    void Update()
    {
        fSpeed = fOriginalSpeed * (1 + .75f * Input.GetAxisRaw(sSprint));
        fAltSpeed = fOriginalAltSpeed * (1 + .75f * Input.GetAxisRaw(sSprint));

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            fHorizontal = Input.GetAxisRaw(sHorizontal) * Mathf.Sqrt(1.0f - Mathf.Abs(Input.GetAxisRaw(sVertical)) / 2.0f);
            fVertical = Input.GetAxisRaw(sVertical) * Mathf.Sqrt(1.0f - Mathf.Abs(Input.GetAxisRaw(sHorizontal)) / 2.0f);
        }
        else
        {
            fHorizontal = Input.GetAxisRaw(sHorizontal);
            fVertical = Input.GetAxisRaw(sVertical);
        }

        vMovement = new Vector3(fHorizontal, 0.0f, fVertical);
    }

    private void FixedUpdate()
    {
        if (vMovement != Vector3.zero && !spine.GetComponent<RagdollModeController>().isRagdoll)
        {
            if ((Time.time * 1.0f) - (int)(Time.time * 1.0f) <= .5f)
            {
                GetComponent<Rigidbody>().AddForce(vMovement * fSpeed * 1.25f);
                GetComponent<Rigidbody>().AddForce(Vector3.up * fUpSpeed * (fSpeed) * 1.25f);
            }
            else
            {
                GetComponent<Rigidbody>().AddForce(vMovement * fAltSpeed * 1.25f);
                GetComponent<Rigidbody>().AddForce(Vector3.up * fUpSpeed * (fAltSpeed) * 1.25f);
            }
        }
    }
}
