﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCTaker : MonoBehaviour
{
    public Transform[] seat;





    public void TakeNpc()
    {

    }
    

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("NPC"))
        {
            collision.transform.GetComponentInParent<NPCInfo>().SitOnTruck();
            int i = collision.transform.GetComponentInParent<NPCInfo>().seatIdx;

            Transform npc = collision.transform.root;

            // 본 CollisionEnter가 두 번 반복되는 현상 존재
            // Truck에 탑승한 상태에서 본 구문이 반복되며 발생되는 버그를 방지하기 위함
            if (npc.gameObject.layer != LayerMask.NameToLayer("NPC")) return;
            
            npc.parent = seat[i];
            npc.localPosition = Vector3.zero;
        }
    }
}