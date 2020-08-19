using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class PlayerUIManager : MonoBehaviour
{
    static public PlayerUIManager instance;

    [Header("Status Panel Elements")]
    public RawImage[] npcImages; // 탑승 npc
    public Text[] npcLevels; // npc 레벨
    public RectTransform[] npcCams; // npc 얼굴 비추는 카메라

    public Vector3 npcCamOffset = new Vector3(0, 1, 1.5f);

    [Header("System Penel Elements")]
    public Text rescueCount;

    private void Start()
    {
        instance = this;

        for(int i=0;i<npcImages.Length; i++)
        {
            npcImages[i].color = Color.black;
        }
    }

    public void AddNpc(WeaponType type, int curLevel, Transform npc = null)
    {
        // 신규 탑승
        if (curLevel == 1)
        {
            // 카메라가 해당 NPC의 얼굴 앞에 위치토록 설정
            npcImages[(int)type].color = Color.white;
            npcCams[(int)type].transform.position = npc.transform.position + npcCamOffset;
            npcCams[(int)type].transform.parent = npc;
            npcCams[(int)type].eulerAngles = new Vector3(0, 180, 0);
            
            npcLevels[(int)type].text = "LV. " + curLevel;
        }

        // 레벨업
        else
        {
            npcLevels[(int)type].text = "LV. " + curLevel;
        }

        rescueCount.text = StageManager.instance.RescueNpc().ToString() + " / " + StageManager.instance.goalRescueNpcCount;
    }

}
