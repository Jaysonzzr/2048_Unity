using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSettings : MonoBehaviour
{
    void Start()
    {
        Screen.SetResolution(1920, 1080, true);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            Application.Quit();
        }
    }

    public void ReloadGame()
    {
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
