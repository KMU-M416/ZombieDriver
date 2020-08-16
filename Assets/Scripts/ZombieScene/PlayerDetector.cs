using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    ZombieControler ZC;

    bool isDetect = false;

    int moveType;

    private void Start()
    {
        ZC = transform.parent.GetComponent<ZombieControler>();

        StartCoroutine(Move());
    }


    IEnumerator Move()
    {
        while (true)
        {
            if (isDetect || ZC.isDead || ZC.isAttack) yield return null;

            moveType = Random.Range(0, 100);

            transform.parent.rotation = Quaternion.Euler(new Vector3(0, transform.parent.rotation.y + Random.Range(-20, 21), 0));

            if (moveType > 0 && moveType < 50) // 멈춰있는 상태
            {
                ZC.zombieAnimator.SetBool("isWalk", false);
            }
            else
            {
                ZC.zombieAnimator.SetBool("isWalk", true);
            }

            yield return new WaitForSeconds(7.0f);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        isDetect = true;
        if (!ZC.isDead)
        {
            transform.parent.LookAt(other.transform);
            ZC.zombieAnimator.SetBool("isWalk", true);
        }
           
    }

    private void OnTriggerExit(Collider other)
    {
        isDetect = false;
    }
}
