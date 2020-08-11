using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInfo : MonoBehaviour
{
    
    public int seatIdx; // 착석할 자리
    public Collider bodyCollider;
    public Collider enemyDetectTrigger;

    private void Start()
    {
        bodyCollider.enabled = true;
        enemyDetectTrigger.enabled = false;
    }

    public void SitOnTruck()
    {
        // 트럭에 올라탄 상태로 중복 충돌 방지
        bodyCollider.enabled = false;

        // 좀비 감지 및 사격
        enemyDetectTrigger.enabled = true;
    }
}
