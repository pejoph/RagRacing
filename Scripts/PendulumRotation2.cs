using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PendulumRotation2 : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(60 * Mathf.Sin(Time.time), 0, 0);
    }
}
