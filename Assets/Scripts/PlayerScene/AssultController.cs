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

    List<GameObject> targetList;
    int maxScore = 0;

    private void Start()
    {
        anim = GetComponentInParent<Animator>();

        anim.SetInteger("weaponType", (int)type);

        targetList = new List<GameObject>();

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
                        maxScore = 0;
                        for (int i = 0; i < targetList.Count; i++)
                        {
                            if (maxScore < targetList[i].GetComponent<ZombieGroupMaker>().member.Count)
                            {
                                maxScore = targetList[i].GetComponent<ZombieGroupMaker>().member.Count;
                                target = targetList[i].transform;
                            }
                        }
                        anim.SetTrigger("assult");
                        print("list : " + targetList.Count);
                        print(maxScore);
                        yield return new WaitForSeconds(2.0f); // 연사 속도
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
        if (type != WeaponType.rocket)
        {
            if (target != null) return;
        }
        else
        {
            if (other.CompareTag("CheckedGroup") && !targetList.Contains(other.gameObject))
            {
                targetList.Add(other.gameObject);
                other.gameObject.GetComponent<ZombieGroupMaker>().setNPCObject(gameObject);
            }
        }

        target = other.transform;
    }

    private void OnTriggerExit(Collider other)
    {
        if (type != WeaponType.rocket)
        {
            if (other.transform == target)
            {
                target = null;
            }
        }
        else
        {
            targetList.Remove(other.gameObject);
        }
    }

    // 좀비 비활성화시 onTriggerExit 함수가 미작동하여 아래 함수로 멤버 제거
    public void deleteList(GameObject obj)
    {
        if (type != WeaponType.rocket) return;

        for (int i = 0; i < targetList.Count; i++)
        {
            targetList.Remove(obj);
        }
    }
}
