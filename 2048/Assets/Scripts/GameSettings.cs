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
        else if (Input.GetKeyDown(KeyCode.X))
        {
            ResetGame();
        }
    }

    public void ReloadGame()
    {
        SceneManager.LoadScene(0);
    }

    public void ResetGame()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
