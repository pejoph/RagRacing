/// 01.02.2018 - 24.04.2018
/// Matthew Mason, Peter Phillips
///

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceCameraController : MonoBehaviour 
{
    [SerializeField] private GameObject[] Players = new GameObject[4];
    [SerializeField] private GameObject FocalPointObject;
    [SerializeField] private GameObject GhostOfFocalPointObject;
    private GameObject LastPlayer;
    private GameObject FirstPlayer;
    private float Distance = 0;

    private float FirstZPos = 0;
    private float LastZPos = 0;
    private float GhostLastZPos = 0;
    private float GhostHeight = 0;
    private float GhostBottom = 0;
	private int PlayerAmount = 0;

	private void Start()
	{
		foreach (GameObject Player in Players)
		{
			if (Player != null)
			{
				Debug.Log("Player Was alive");
				++PlayerAmount;
			}
			else
			{
				Debug.Log("Player Was NUll!");
			}
		}

	}

	void Update()
    {
        transform.LookAt(GhostOfFocalPointObject.transform);
        FirstZPos = Players[0].transform.position.z;
        LastZPos = Players[0].transform.position.z;
        for (int i = 0; i < PlayerAmount; i++)
        {
            if (Players[i].transform.position.z > FirstZPos)
            {
                FirstZPos = Players[i].transform.position.z;
                FirstPlayer = Players[i];
            }

            if (Players[i].transform.position.z < LastZPos)
            {
                LastZPos = Players[i].transform.position.z;
                LastPlayer = Players[i];
            }
        }

        Distance = FirstZPos - LastZPos;
        FocalPointObject.transform.position = new Vector3(FocalPointObject.transform.position.x,
                                                          FocalPointObject.transform.position.y, 
                                                          (FirstZPos + LastZPos) / 2);

        Debug.Log(FirstZPos);
        Debug.Log(LastZPos);

        transform.position = CamPos();
    }
    
    private Vector3 CamPos()
    {
        Distance = Mathf.Abs(FirstZPos - LastZPos);
        float CameraTilt = -.5f; 
        float Hypotenuse = Distance * (Mathf.Sin(135 - CameraTilt) / Mathf.Sin(45)) - 2;
        GhostLastZPos += (LastZPos - GhostLastZPos) / 30.0f;
		float Bottom;
		float Height;
		switch (PlayerAmount)
		{
			case 1:
				Bottom = Mathf.Clamp(Mathf.Cos(CameraTilt) * Hypotenuse, 10.0f, 20.0f) +
					   Mathf.Abs(Players[0].transform.position.y) +
					   Mathf.Abs(Players[0].transform.position.x) / 2.0f;
				GhostBottom += (Bottom - GhostBottom) / 30.0f;
				Height = Mathf.Clamp(Mathf.Sin(CameraTilt) * Hypotenuse, 5.0f, 15.0f) +
						Mathf.Abs(Players[0].transform.position.y) +
						Mathf.Abs(Players[0].transform.position.x) / 2.0f;
				GhostHeight += (Height - GhostHeight) / 30.0f;
				break;
			case 2:
				Bottom = Mathf.Clamp(Mathf.Cos(CameraTilt) * Hypotenuse, 10.0f, 20.0f) +
						Mathf.Max(Mathf.Abs(Players[0].transform.position.y), Mathf.Abs(Players[1].transform.position.y)) +
						Mathf.Max(Mathf.Abs(Players[0].transform.position.x), Mathf.Abs(Players[1].transform.position.x)) / 2.0f;
				GhostBottom += (Bottom - GhostBottom) / 30.0f;
				Height = Mathf.Clamp(Mathf.Sin(CameraTilt) * Hypotenuse, 5.0f, 15.0f) +
						Mathf.Max(Mathf.Abs(Players[0].transform.position.y), Mathf.Abs(Players[1].transform.position.y)) +
						Mathf.Max(Mathf.Abs(Players[0].transform.position.x), Mathf.Abs(Players[1].transform.position.x)) / 2.0f;
				GhostHeight += (Height - GhostHeight) / 30.0f;
				break;
			case 3:
				Bottom = Mathf.Clamp(Mathf.Cos(CameraTilt) * Hypotenuse, 10.0f, 20.0f) +
						Mathf.Max(Mathf.Abs(Players[0].transform.position.y), Mathf.Abs(Players[1].transform.position.y), Mathf.Abs(Players[2].transform.position.y)) +
						Mathf.Max(Mathf.Abs(Players[0].transform.position.x), Mathf.Abs(Players[1].transform.position.x), Mathf.Abs(Players[2].transform.position.x)) / 2.0f;
				GhostBottom += (Bottom - GhostBottom) / 30.0f;
				Height = Mathf.Clamp(Mathf.Sin(CameraTilt) * Hypotenuse, 5.0f, 15.0f) +
						Mathf.Max(Mathf.Abs(Players[0].transform.position.y), Mathf.Abs(Players[1].transform.position.y), Mathf.Abs(Players[2].transform.position.y)) +
						Mathf.Max(Mathf.Abs(Players[0].transform.position.x), Mathf.Abs(Players[1].transform.position.x), Mathf.Abs(Players[2].transform.position.x)) / 2.0f;
				GhostHeight += (Height - GhostHeight) / 30.0f;
				break;
			case 4:
				Bottom = Mathf.Clamp(Mathf.Cos(CameraTilt) * Hypotenuse, 10.0f, 20.0f) +
						Mathf.Max(Mathf.Abs(Players[0].transform.position.y), Mathf.Abs(Players[1].transform.position.y), Mathf.Abs(Players[2].transform.position.y), Mathf.Abs(Players[3].transform.position.y)) +
						Mathf.Max(Mathf.Abs(Players[0].transform.position.x), Mathf.Abs(Players[1].transform.position.x), Mathf.Abs(Players[2].transform.position.x), Mathf.Abs(Players[3].transform.position.x)) / 2.0f;
				GhostBottom += (Bottom - GhostBottom) / 30.0f;
				Height = Mathf.Clamp(Mathf.Sin(CameraTilt) * Hypotenuse, 5.0f, 15.0f) +
						Mathf.Max(Mathf.Abs(Players[0].transform.position.y), Mathf.Abs(Players[1].transform.position.y), Mathf.Abs(Players[2].transform.position.y), Mathf.Abs(Players[3].transform.position.y)) +
						Mathf.Max(Mathf.Abs(Players[0].transform.position.x), Mathf.Abs(Players[1].transform.position.x), Mathf.Abs(Players[2].transform.position.x), Mathf.Abs(Players[3].transform.position.x)) / 2.0f;
				GhostHeight += (Height - GhostHeight) / 30.0f;
				break;

		}
        return new Vector3(0, GhostHeight, GhostLastZPos - GhostBottom);
    }
}
