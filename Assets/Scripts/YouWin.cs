﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class YouWin : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
		Cursor.visible = true;
		Screen.lockCursor = false;

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}