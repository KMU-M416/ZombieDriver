using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAttackChecked : MonoBehaviour
{
    ZombieControler ZC;

    public IEnumerator co_Attack;
    bool isAttack;
    Rigidbody ParentRd;

    private void Start()
    {
        ZC = transform.parent.GetComponent<ZombieControler>();
        ParentRd = transform.parent.GetComponent<Rigidbody>();
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
            ParentRd.velocity = Vector3.zero;
            ZC.zombieAnimator.SetBool("isAttack", true);

            if (ZC.isDead)
                StopCoroutine(co_Attack);
            else PlayerMover.instance.Attacked(ZC.GetAttackInfo(1));
                       
            yield return null;
        }
    }
}
