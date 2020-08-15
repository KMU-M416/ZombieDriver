using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    public void GameStart()
    {
        LoadingManager.LoadScene(SceneNames.IngameScene.ToString(), SceneNames.TitleScene.ToString());

        //SceneManager.LoadSceneAsync("PlayerScene");
        //SceneManager.LoadSceneAsync("ZombieScene", LoadSceneMode.Additive);
    }

    public void GameQuit()
    {
        Application.Quit();
        
    }
}
