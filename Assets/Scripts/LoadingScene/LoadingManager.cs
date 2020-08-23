using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum SceneNames
{
    TitleScene,
    IngameScene,
}

public class LoadingManager : MonoBehaviour
{
    static string nextScene, beforeScene;

    [Header("Components")]
    public Image progressBar;
    public Text logs;

    public static void LoadScene(string nextSceneName, string beforeSceneName)
    {
        nextScene = nextSceneName;
        beforeScene = beforeSceneName;

        SceneManager.LoadSceneAsync("LoadingScene", LoadSceneMode.Additive);
    }


    private void Start()
    {
        try
        {
            logs.text += ">> Scene Load Start() called [LoadingManager.cs]\n";

            if (beforeScene != null)
            {
                if (beforeScene == SceneNames.IngameScene.ToString())
                {
                    logs.text += ">> Unload PlayerScene [LoadingManager.cs]\n";
                    logs.text += ">> Unload ZombieScene [LoadingManager.cs]\n";
                    SceneManager.UnloadSceneAsync("PlayerScene");
                    SceneManager.UnloadSceneAsync("ZombieScene");
                }
                else
                {
                    logs.text += ">> Unload " + beforeScene + " [LoadingManager.cs]\n";
                    SceneManager.UnloadSceneAsync(beforeScene);
                }
            }

            beforeScene = null;
            StartCoroutine(LoadSceneProcess());
        }
        catch (System.Exception e)
        {
            //logs.text += e;
            Debug.Log(e);
        }

    }

    IEnumerator LoadSceneProcess()
    {

        logs.text += ">> LoadSceneProcess() coroutine called [LoadingManager.cs]\n";

        yield return new WaitForSeconds(1f);

        AsyncOperation op;
        if (nextScene == SceneNames.IngameScene.ToString())
        {
            logs.text += ">> Load PlayerScene [LoadingManager.cs]\n";
            logs.text += ">> Load ZombieScene [LoadingManager.cs]\n";
            op = SceneManager.LoadSceneAsync("PlayerScene", LoadSceneMode.Additive);
            SceneManager.LoadSceneAsync("ZombieScene", LoadSceneMode.Additive);
        }
        else if (nextScene == SceneNames.TitleScene.ToString())
        {
            logs.text += ">> Destroy [Main Camera] used in Ingame Scene\n";
            logs.text += ">> Destroy [Directional Light] used in Ingame Scene";

            Destroy(GameObject.Find("Main Camera"));
            Destroy(GameObject.Find("Directional Light"));

            logs.text += ">> Load " + nextScene + " [LoadingManager.cs]\n";
            op = SceneManager.LoadSceneAsync(nextScene, LoadSceneMode.Additive);
        }
        else
        {
            logs.text += ">> Load " + nextScene + " [LoadingManager.cs]\n";
            op = SceneManager.LoadSceneAsync(nextScene, LoadSceneMode.Additive);
        }

        op.allowSceneActivation = false;

        float timer = 0f;

        while (!op.isDone)
        {
            yield return null;

            timer += Time.deltaTime;

            if (op.progress < 0.9f)
            {
                progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, op.progress, timer);
                if (progressBar.fillAmount >= op.progress) timer = 0f;
            }
            else
            {
                progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, 1.0f, timer);

                if (progressBar.fillAmount >= 1f)
                {
                    logs.text += ">> Loading Finish!! wait for start ...\n";
                    yield return new WaitForSeconds(1f);
                    op.allowSceneActivation = true;
                }
            }
        }

        SceneManager.UnloadSceneAsync("LoadingScene");
    }

}
