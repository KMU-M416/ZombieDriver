using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    [Header("Elements")]
    public GameObject settingTab;

    public GameObject startSelectTab; // 난이도 버튼 탭
    public RectTransform startBtn;
    public RectTransform[] otherBtns; // Start를 제외한, 나머지 버튼들


    IEnumerator coroutine;

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


    /// <summary>
    /// 시작 버튼 클릭시 호출.
    /// 난이도를 고를 수 있는 버튼 보이기 및 숨기기
    /// </summary>
    public void CallStartBtns()
    {
        if(coroutine == null)
        {
            coroutine = StartBtns();
            StartCoroutine(StartBtns());
        }
    }

    IEnumerator StartBtns()
    {
        print($"[TEST] coroutine called");

        // 효과 시간
        float timer = 0f;
        float limit = 0.5f;
        
        // 난이도 탭이 열려있다면
        if (startSelectTab.activeSelf)
        {
            startSelectTab.SetActive(false);

            // 닫는 애니메이션 실행
            while (timer < limit)
            {
                timer += Time.deltaTime;

                startBtn.anchoredPosition = Vector3.Lerp(startBtn.anchoredPosition, new Vector3(0, 300, 0), timer * 2);

                otherBtns[0].anchoredPosition = Vector3.Lerp(otherBtns[0].anchoredPosition, new Vector3(0, 100, 0), timer * 2);
                otherBtns[1].anchoredPosition = Vector3.Lerp(otherBtns[1].anchoredPosition, new Vector3(0, -100, 0), timer * 2);
                otherBtns[2].anchoredPosition = Vector3.Lerp(otherBtns[2].anchoredPosition, new Vector3(0, -300, 0), timer * 2);


                yield return null;
            }
        }

        // 난이도 탭이 닫혀있다면
        else
        {
            startSelectTab.SetActive(true);

            // 여는 애니메이션 실행
            while (timer < limit)
            {
                timer += Time.deltaTime;

                startBtn.anchoredPosition = Vector3.Lerp(startBtn.anchoredPosition, new Vector3(0, 400, 0), timer * 2);

                otherBtns[0].anchoredPosition = Vector3.Lerp(otherBtns[0].anchoredPosition, new Vector3(0, 0, 0), timer * 2);
                otherBtns[1].anchoredPosition = Vector3.Lerp(otherBtns[1].anchoredPosition, new Vector3(0, -200, 0), timer * 2);
                otherBtns[2].anchoredPosition = Vector3.Lerp(otherBtns[2].anchoredPosition, new Vector3(0, -400, 0), timer * 2);


                yield return null;
            }
        }
        
        coroutine = null;
    }


    public void OnSettingTab()
    {
        settingTab.SetActive(true);
    }
}
