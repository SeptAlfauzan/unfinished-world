﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
 
    public void PlayGame(){
        SceneManager.LoadScene("SampleScene");
    }


    public void QuitGame(){
        Application.Quit();
    }
}
