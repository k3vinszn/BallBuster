using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HighScore : MonoBehaviour
{
    [SerializeField] private Text HSText;

    private void Start()
    {

        if (HSText != null)
        {
            // calls in the score from the balls
            int highScore = PlayerPrefs.GetInt("HighScore", 0);
            HSText.text = "High Score: " + highScore.ToString();
        }
    }

}