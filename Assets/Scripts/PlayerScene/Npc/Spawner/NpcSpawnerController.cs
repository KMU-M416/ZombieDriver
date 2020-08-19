using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcSpawnerController : MonoBehaviour
{
    [Header("Components")]
    Transform SpeechBubble;

    [Header("Sensibilities")]
    [SerializeField] bool isFirstNpcSpawner;
    [SerializeField] WeaponType type; // NPC 종류 설정
    public float waitTime = 3.0f; // 해당 NPC를 획득하기 위한 필요 대기 시간

    [Header("== TEST ==")] 
    public bool trigger;

    private void Start()
    {
        InitType();

        SpeechBubble = transform.Find("SpeechBubble");
        SpeechBubble.transform.eulerAngles = new Vector3(45, 45, 0);

    }

    private void Update()
    {
        if (trigger)
        {
            trigger = false;
            GetNpcFromSpawner();
        }
    }

    public void InitType()
    {
        if (isFirstNpcSpawner)
            type = WeaponType.pistol;

        else
        {
            type = (WeaponType)Random.Range(0, 4);
            gameObject.SetActive(false);

        }
    }

    /// <summary>
    /// NPC 스포너에서 n초간 대기 후 본 함수를 호출해 해당 NPC를 획득 및 스포너 삭제
    /// </summary>
    public void GetNpcFromSpawner()
    {
        // npc 신규 탑승
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<NPCTaker>().seat[(int)type].childCount == 0)
        {
            // 타겟 NPC 로드 - # 경로 주의 #
            var v = Resources.Load("Prefabs/Objects/NPCs/NPC_" + type.ToString());

            GameObject npc = Instantiate((GameObject)v);
            npc.GetComponentInChildren<NPCInfo>().SitOnTruck();

            npc.transform.parent = GameObject.FindGameObjectWithTag("Player").GetComponent<NPCTaker>().seat[(int)type];
            npc.transform.localPosition = Vector3.zero;

            PlayerUIManager.instance.AddNpc(type, 1, npc.transform);
        }

        // 이미 탑승해 있는 npc.
        // 해당 npc 업그레이드
        else
        {
            int lv = GameObject.FindGameObjectWithTag("Player").GetComponent<NPCTaker>().seat[(int)type].GetChild(0)
                .GetComponentInChildren<AssultController>().LevelUp();

            PlayerUIManager.instance.AddNpc(type, lv);
        }

        


        // 자신 npc 스포너 삭제
        Destroy(gameObject);
        
    }

}
