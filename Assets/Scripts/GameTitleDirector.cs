﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameTitleDirector : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            SceneManager.LoadScene("GameScene_2");
        }
    }
}
