using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class PlayerWaitChecker : MonoBehaviour
{
    Image waitTimeViewer, waitTimeViewerBackground;
    float waitTime;
    float curWaitTime = 0;

    private void Start()
    {
        waitTime = GetComponentInParent<NpcSpawnerController>().waitTime;

        waitTimeViewer = transform.Find("Canvas").GetChild(1).GetComponent<Image>();
        waitTimeViewerBackground = transform.Find("Canvas").GetChild(0).GetComponent<Image>();
        waitTimeViewer.gameObject.SetActive(false);
        waitTimeViewerBackground.gameObject.SetActive(false);

        waitTimeViewer.transform.eulerAngles = new Vector3(45, 45, 0);
        waitTimeViewerBackground.transform.eulerAngles = new Vector3(45, 45, 0);
        waitTimeViewer.fillAmount = 0f;
    }
    

    private void OnTriggerEnter(Collider other)
    {
        //if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        if(other.CompareTag("Player"))
        {
            waitTimeViewer.gameObject.SetActive(true);
            waitTimeViewerBackground.gameObject.SetActive(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        //if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        if(other.CompareTag("Player"))
        {
            curWaitTime += Time.deltaTime;
            waitTimeViewer.fillAmount = curWaitTime / waitTime; 

            if(curWaitTime >= waitTime)
            {
                GetComponentInParent<NpcSpawnerController>().GetNpcFromSpawner();
            }
        }
    }


    private void OnTriggerExit(Collider other)
    {
        // 대기 시간 초기화
        //if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        if(other.CompareTag("Player"))
        {
            curWaitTime = 0;
            waitTimeViewer.gameObject.SetActive(false);
            waitTimeViewerBackground.gameObject.SetActive(false);

        }
    }
}
