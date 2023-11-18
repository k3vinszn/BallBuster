using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DestroyOnlyOnGameOver: MonoBehaviour
{
    private void Awake()
    {
        // Check if the current scene is not the "GameOver" scene
        if (SceneManager.GetActiveScene().name != "GameOver")
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}