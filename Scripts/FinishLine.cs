using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets
{
	public enum PlayerColour
	{
		Green,
		Red,
		Blue,
		Yellow,
	}

	public class FinishLine : MonoBehaviour 
	{
		[SerializeField] private GameObject Positions;
		[SerializeField] private GameObject YellowPlayerImage;
		[SerializeField] private GameObject GreenPlayerImage;
		[SerializeField] private GameObject BluePlayerImage;
		[SerializeField] private GameObject RedPlayerImage;
		[SerializeField] private GameObject SceneCanvas;
		private static int PlayersPastFinishLine;
		public static int PlayersInLevel = 0;
		public static int CurrentPlayer;
		private GameObject[] PostionalImages;

		private void Start()
		{
			PostionalImages = new GameObject[PlayersInLevel];
		}

		private void OnEnable()
		{
			PlayerRespawningAndCheckpoints.CrossedFinishLine += OnCrossingFinishLine;
		}
		private void OnDisable()
		{
			PlayerRespawningAndCheckpoints.CrossedFinishLine -= OnCrossingFinishLine;
		}

		private void OnCrossingFinishLine(PlayerColour ColourOfPlayerCrossingFinish)
		{
			switch (ColourOfPlayerCrossingFinish)
			{
				case PlayerColour.Blue:
					PostionalImages[PlayersPastFinishLine] = BluePlayerImage;
					break;
				case PlayerColour.Green:
					PostionalImages[PlayersPastFinishLine] = GreenPlayerImage;
					break;
				case PlayerColour.Yellow:
					PostionalImages[PlayersPastFinishLine] = YellowPlayerImage;
					break;
				case PlayerColour.Red:
					PostionalImages[PlayersPastFinishLine] = RedPlayerImage;
					break;
			}
			++PlayersPastFinishLine;
			if (PlayersPastFinishLine >= PlayersInLevel)
			{
				GameObject NewCanvasElement = Instantiate(Positions, new Vector3(100, Screen.height/2), new Quaternion(0,0,0,0));
				NewCanvasElement.transform.SetParent(SceneCanvas.transform);
				for (int i = 0; i < PlayersInLevel; ++i)
				{
					NewCanvasElement = Instantiate(PostionalImages[i], new Vector3(400.0f, ((Screen.height / 2) + 200) - (100 * i), 0.0f), new Quaternion(0, 0, 0, 0));
					NewCanvasElement.transform.SetParent(SceneCanvas.transform);
				}
			}
		}
	}
}
