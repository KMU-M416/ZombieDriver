using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NpcSpawnerGenerator : MonoBehaviour
{
    [Header("Elements")]
    public List<GameObject> spawnerPoints;
    
    private void Start()
    {
        GenerateNpcSpawner();   
    }


    /// <summary>
    /// Npc Spawner를 배치하는 함수
    /// </summary>
    public void GenerateNpcSpawner()
    {
        // 스포너 오브젝트 로드
        //GameObject spawnerPref = Resources.Load<GameObject>("Prefabs/Objects/NPCs/NpcSpawner");

        // Normal 기준 13개 생성
        for(int i = 0; i < 13; i++)
        {
            int idx = Random.Range(0, spawnerPoints.Count);

            // 이미 배치된 포인트라면 통과
            if(spawnerPoints[idx].activeSelf == true)
            {
                i--;
            }
            else
            {
                // 활성화
                spawnerPoints[idx].SetActive(true);
            }

            // 생성
            //GameObject obj = Instantiate(spawnerPref, spawnerPoints[idx].position, Quaternion.Euler(spawnerPoints[idx].eulerAngles));
            

            
        }


    }
}
