using UnityEngine;
using System.Collections;

public class LoadOnClick : MonoBehaviour
{

    public void LoadScene(string sceneName)
    {
        Application.LoadLevel(sceneName);
    }

    public void QuitGame()
    {
            Application.Quit();
    }

}