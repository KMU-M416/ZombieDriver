using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    ZombieControler ZC;

    bool isDetect = false;

    int moveType;
    bool isMove = false;
    float timer = 0;

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

            if (!isMove)
            {
                moveType = Random.Range(0, 100);

                transform.parent.rotation = Quaternion.Euler(new Vector3(0, transform.parent.rotation.y + Random.Range(-20, 21), 0));
            }           

            if (moveType > 0 && moveType < 50) // 멈춰있는 상태
            {
                ZC.zombieAnimator.SetBool("isWalk", false);
            }
            else
            {
                isMove = true;
                timer += Time.deltaTime;

                transform.parent.Translate(Vector3.forward * ZC.GetAttackInfo(2) * Time.deltaTime ,Space.Self);
                ZC.zombieAnimator.SetBool("isWalk", true);     
                
                if(timer > 7.0f)
                {
                    timer = 0;
                    isMove = false;
                }
            }

            if (!isMove)
                yield return new WaitForSeconds(7.0f);
            else yield return null;
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
