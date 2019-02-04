///\=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=/\\\
///\                                                                   /\\\
///\  Filename  : LavaAnimation.cs 	    							   /\\\
///\  			 													   /\\\
///\  Author(s) : Peter Phillips            				    	   /\\\
///\     		 													   /\\\
///\  Date      : First entry - 05 / 05 / 2018						   /\\\
///\     	      Last entry  - 05 / 05 / 2018						   /\\\
///\                                                                   /\\\
///\  Brief     : ==================================================   /\\\
///\              ==================================================   /\\\
///\                                                                   /\\\
///\=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=/\\\


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaAnimation : MonoBehaviour
{
    [SerializeField] private Material Lava;

    //private float ScaleX;
    //private float ScaleY;
    //private float ScaleZ;
    private float LavaSpeed;
    //private float LavaStretch;
    //private float PosX;
    //private float PosY;
    //private float PosZ;

    void Start ()
    {
        //ScaleX = transform.localScale.x;
        //ScaleY = transform.localScale.y;
        //ScaleZ = transform.localScale.z;
        LavaSpeed = .125f;                     ///waves per second
        //LavaStretch = 2.5f;                 ///stretch percentage
        //PosX = transform.position.x;
        //PosY = transform.position.y;
        //PosZ = transform.position.z;
        Lava.mainTextureOffset = Vector2.zero;
    }
	
	void Update ()
    {
        //transform.localScale = new Vector3(ScaleX, ScaleY + (ScaleY * LavaStretch / 100f) * Mathf.Sin(Time.time * LavaSpeed * Mathf.PI), ScaleZ);
        //transform.position = new Vector3(PosX + 4 * Mathf.Sin(Time.time * LavaSpeed / 8 * Mathf.PI), PosY, PosZ);
        //transform.position = new Vector3(PosX, PosY, PosZ + 4 * Mathf.Sin(Time.time * LavaSpeed / 8 * Mathf.PI));
        //GetComponent<Material>().mainTextureOffset = new Vector2(.1f * -Time.time, 0);
        Lava.mainTextureOffset = new Vector2(.025f * Time.time, .025f * Mathf.Sin(Time.time * LavaSpeed * Mathf.PI));
    }
}
