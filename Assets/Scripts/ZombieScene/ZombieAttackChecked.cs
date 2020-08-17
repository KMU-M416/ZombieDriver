using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAttackChecked : MonoBehaviour
{
    ZombieControler ZC;

    IEnumerator co_Attack;
    bool isAttack;

    private void Start()
    {
        ZC = transform.parent.GetComponent<ZombieControler>();
        co_Attack = Attack();
    }

    private void OnTriggerStay(Collider other)
    {
        if (!isAttack)
        {
            isAttack = true;
            ZC.isAttack = true;

            StartCoroutine(co_Attack);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        StopCoroutine(co_Attack);
        ZC.zombieAnimator.SetBool("isAttack", false);
        ZC.isAttack = false;
        isAttack = false;
    }

    IEnumerator Attack()
    {
        while (true)
        {
            ZC.zombieAnimator.SetBool("isAttack", true);

            PlayerMover.instance.Attacked(ZC.GetAttackInfo(1));

            // 체력관련
            // 채력함수(ZC.GetAttackInfo(1));
            yield return new WaitForSeconds(ZC.GetAttackInfo(0));
        }
    }
}
