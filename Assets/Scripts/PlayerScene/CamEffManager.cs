using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class CamEffManager : MonoBehaviour
{
    public static CamEffManager instance;

    [Header("Elements")]
    [SerializeField] Image attackedEff;
    [SerializeField] Image gameOverEff;


    bool isGameOver;

    IEnumerator coroutine;

    private void Start()
    {
        instance = this;
    }



    /// <summary>
    /// 플레이어 피격 이펙트
    /// </summary>
    public void CallAttackedEff()
    {
        if(coroutine != null)
            StopCoroutine(coroutine);

        coroutine = AttackedEff();
        StartCoroutine(coroutine);
    }

    IEnumerator AttackedEff()
    {
        Color effColor = attackedEff.color;
        effColor.a = 1f;

        while (effColor.a > 0.1f)
        {
            effColor.a = Mathf.Lerp(effColor.a, 0, Time.deltaTime);
            attackedEff.color = effColor;

            yield return null;
        }
        effColor.a = 0f;
        attackedEff.color = effColor;
    }


    /// <summary>
    /// 게임 종료 페이드아웃 이펙트
    /// </summary>
    public void CallGameOverEff()
    {
        if (!isGameOver)
        {
            isGameOver = true;
            StartCoroutine(GameOverEff());
        }
    }
    
    IEnumerator GameOverEff()
    {
        print($"[TEST] GameOverEff Called");

        float timer = 0f;
        while(timer < 5f)
        {
            timer += Time.deltaTime;

            gameOverEff.color = Color.Lerp(gameOverEff.color, gameOverEff.color + new Color(0, 0, 0, 1), Time.deltaTime * 0.33f);

            yield return null;
        }
        
        //while (!Input.GetKeyDown(KeyCode.Escape))
        //{
        //    // 대기

        //    yield return null;
        //}
        
        LoadingManager.LoadScene(SceneNames.TitleScene.ToString(), SceneNames.IngameScene.ToString());
    }


}
