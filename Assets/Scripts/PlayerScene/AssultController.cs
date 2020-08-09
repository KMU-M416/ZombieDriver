using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    pistol, // 권총
    rifle, // 돌격소총
    rocket, // 로켓 발사기
    shotgun // 샷건
}

public class AssultController : MonoBehaviour
{
    [Header("Components")]
    Animator anim;

    [Header("Sensibilities")]
    public WeaponType type;
    

    [Header("Monitor")]
    public Transform target;

    private void Start()
    {
        anim = GetComponentInParent<Animator>();

        anim.SetInteger("weaponType", (int)type);

        StartCoroutine(Assult());
    }


    private void Update()
    {
        //Assult();
    }

    IEnumerator Assult()
    {
        Transform root = anim.transform;

        while (true)
        {
            // 공격 대상이 없다면 진행 중단
            if (target != null)
            {
                // 대상 바라보기
                root.LookAt(target);
                root.eulerAngles = new Vector3(0, root.eulerAngles.y, 0);


                // 무기에 따라 다른 공격 진행
                switch (type)
                {
                    case WeaponType.pistol:

                        break;

                    case WeaponType.rifle:
                        anim.SetTrigger("assult");
                        Debug.DrawLine(transform.position, target.position, Color.red);
                        yield return new WaitForSeconds(0.5f); // 연사 속도
                        break;

                    case WeaponType.rocket:

                        break;

                    case WeaponType.shotgun:

                        break;
                }

            }

            yield return null;
        }

    }

    //void Assult()
    //{
    //    // 공격 대상이 없다면 진행 중단
    //    if (target == null) return;

    //    switch (type)
    //    {
    //        case WeaponType.pistol:
    //            break;
    //        case WeaponType.rifle:

    //            anim.SetTrigger("assult");

    //            break;
    //        case WeaponType.rocket:
    //            break;
    //        case WeaponType.shotgun:
    //            break;
    //    }

    //    Debug.DrawLine(transform.position, target.position, Color.red);
    //}
    
    private void OnTriggerStay(Collider other)
    {

        if (target != null) return;
        
        target = other.transform;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform == target) target = null;
    }
}
