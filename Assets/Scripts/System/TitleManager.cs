using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    public void GameStart()
    {
        SceneManager.LoadSceneAsync("PlayerScene");
        SceneManager.LoadSceneAsync("JombieScene", LoadSceneMode.Additive);
    }

    public void GameQuit()
    {
        Application.Quit();
        
    }
}
