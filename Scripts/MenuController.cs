﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
	public void QuitGame()
	{
		Application.Quit();
	}
	public void OpenControls()
	{
		SceneManager.LoadScene(2, LoadSceneMode.Single);
	}
	public void OpenMainGame()
	{
		SceneManager.LoadScene(1, LoadSceneMode.Single);
	}
}
