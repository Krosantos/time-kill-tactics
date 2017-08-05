using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{

    public int PlaceholderId;
    public TextAsset Army;

    public void FindGame()
    {
        SceneManager.LoadScene("UnitsTest");
        WebClient.Send(new FindGameMessage());
    }

    public void ShowSettings()
    {
        // Bring up the as-of-yet unbuilt settings window
    }

    public void Quit()
    {
        Application.Quit();
    }
}
