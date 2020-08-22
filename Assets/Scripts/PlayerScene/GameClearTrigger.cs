using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameClearTrigger : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CamEffManager.instance.CallGameOverEff();

            print($"[TEST] Game Clear");
        }
    }
}
