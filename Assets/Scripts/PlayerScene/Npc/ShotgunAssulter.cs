﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunAssulter : MonoBehaviour
{

    public List<Transform> AssultList;

    private void OnDisable()
    {
        AssultList.Clear();
    }

    // 중복되지 않게 범위 내의 좀비들 저장
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("root"))
        {
            if (!AssultList.Contains(other.transform))
            {
                AssultList.Add(other.transform);
            }
        }
    }

}