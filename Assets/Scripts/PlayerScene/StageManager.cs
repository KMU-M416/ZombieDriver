using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    static public StageManager instance;

    public ZombieGenerator[] zombieSpawnerList;

    public GameObject Gate;

    public int curRescueNpcCount;
    public int goalRescueNpcCount = 8;

    bool isClear;

    private void Start()
    {
        instance = this;
    }

    /// <summary>
    /// NPC 획득 시 호출되는 함수.
    /// 스테이지 클리어가 가능 여부를 체크한다.
    /// </summary>
    /// <returns>총 구한 npc 수</returns>
    public int RescueNpc()
    {
        curRescueNpcCount++;
 
        if(curRescueNpcCount >= goalRescueNpcCount)
        {
            ClearPossible();
        }

        return curRescueNpcCount;
    }


    void ClearPossible()
    {
        if (isClear) return;

        isClear = true;

        // 게임 클리어를 위한 구문
        // 다리로 나갈 수 있도록 한다거나 뭐 그런거

        Destroy(Gate);
    }
}
