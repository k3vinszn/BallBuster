using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class UI : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("Gameplay1");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
