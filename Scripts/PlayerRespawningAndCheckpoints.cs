using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets
{
	public class PlayerRespawningAndCheckpoints : MonoBehaviour
	{
		[SerializeField] private PlayerColour playerColour;
		private Vector3 RespawnPosition;
        private bool FinishFlag = false;

		public delegate void PlayerPastFinishLine(PlayerColour ColourOfPlayerCrossingFinish);
		public static PlayerPastFinishLine CrossedFinishLine;

		private void Start()
		{
			RespawnPosition = gameObject.transform.position;
		}
		private void OnEnable()
		{
			FinishLine.PlayersInLevel++;
		}

		private void OnTriggerEnter(Collider other)
		{
			if (other.gameObject.tag == "KillZone")
			{
				gameObject.transform.position = RespawnPosition;
			}
			else if (other.gameObject.tag == "CheckPoint")
			{
				RespawnPosition = new Vector3(RespawnPosition.x, other.transform.position.y, other.transform.position.z);
			}
			else if (other.gameObject.tag == "WinZone" && !FinishFlag)
			{
				RespawnPosition = new Vector3(RespawnPosition.x, other.transform.position.y, other.transform.position.z);
                CrossedFinishLine(playerColour);
                FinishFlag = true;
			}
		}
	}
}
