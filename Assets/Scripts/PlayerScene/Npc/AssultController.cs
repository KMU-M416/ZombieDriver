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

[System.Serializable]
public class WeaponStatus
{
    public int damage;
    public float shotSpeed;
}


public class AssultController : MonoBehaviour
{
    [Header("Components")]
    public GameObject shotEff; // prefs for each weapon type
                               //public Transform muzzle; // 총구


    Animator anim; // auto load on start()
    GameObject shotgunRange; // 샷건 공격 범위, auto load on start().

    [Header("Weapon Status")]
    public WeaponStatus status;

    [Header("Sensibilities")]
    public WeaponType type;

    [Header("Monitor")]
    public Transform target;

    List<GameObject> targetList;
    int maxScore = 0;

    private void Start()
    {
        // == Components == //
        anim = GetComponentInParent<Animator>();
        targetList = new List<GameObject>();

        if (type == WeaponType.shotgun)
            shotgunRange = transform.Find("shotgunRange").gameObject;


        // == Init == //
        anim.SetInteger("weaponType", (int)type);

        StartCoroutine(Assult());
    }

    IEnumerator Assult()
    {
        Transform root = anim.transform;

        while (true)
        {

            // 공격 대상이 없다면 진행 중단
            if (target != null)
            {
                if (!target.gameObject.activeInHierarchy)
                {
                    target = null;
                    continue;
                }

                // 대상 바라보기
                root.LookAt(target);
                root.eulerAngles = new Vector3(0, root.eulerAngles.y, 0);


                // 무기에 따라 다른 공격 진행
                switch (type)
                {
                    // 가장 체력이 낮은 적 공격
                    case WeaponType.pistol:

                        anim.SetTrigger("assult");

                        GameObject tmpP = Instantiate(shotEff, transform.position + transform.TransformVector(0, 1, 1), Quaternion.identity);
                        tmpP.transform.eulerAngles = transform.eulerAngles + new Vector3(0, -90, 0);

                        int minHp = int.MaxValue;


                        for (int i = 0; i < targetList.Count; i++)
                        {
                            if (minHp > targetList[i].GetComponent<ZombieControler>().GetHp())
                            {

                                target = targetList[i].transform;
                                minHp = targetList[i].GetComponent<ZombieControler>().GetHp();
                            }
                        }

                        // 공격 진행
                        if (target != null && target.gameObject.activeInHierarchy)
                        {
                            // 대상이 사망했다면 타겟 리스트에서 제외
                            if (target.GetComponentInParent<ZombieControler>().ReduceHp(status.damage))
                            {
                                targetList.Remove(target.gameObject);
                                target = null;
                            }
                            else
                            {
                                print($"[TEST] pistol shot {status.damage} / {target.GetComponentInParent<ZombieControler>().GetHp()}");
                            }
                        }


                        //yield return new WaitForSeconds(0.5f); // reload
                        yield return new WaitForSeconds(status.shotSpeed);

                        break;

                    // 일반 공격
                    case WeaponType.rifle:

                        anim.SetTrigger("assult"); // play shot anim

                        GameObject tmpR = Instantiate(shotEff, transform.position + transform.TransformVector(0, 1, 1), Quaternion.identity);
                        tmpR.transform.eulerAngles = transform.eulerAngles + new Vector3(0, -90, 0);

                        // 공격 진행
                        // 대상이 사망했다면 타겟 리스트에서 제외
                        if (target.GetComponentInParent<ZombieControler>().ReduceHp(status.damage))
                        {
                            targetList.Remove(target.gameObject);
                            target = null;
                        }

                        //yield return new WaitForSeconds(0.5f); // reload
                        yield return new WaitForSeconds(status.shotSpeed);

                        break;

                    // 가장 뭉쳐 있는 적 공격
                    case WeaponType.rocket:
                        // 가장 많은 개체수가 밀집되어있는 군집 비교 탐색
                        maxScore = 0;

                        for (int i = 0; i < targetList.Count; i++)
                        {
                            if (maxScore < targetList[i].transform.GetChild(3).GetComponent<ZombieGroupMaker>().member.Count)
                            {
                                maxScore = targetList[i].transform.GetChild(3).GetComponent<ZombieGroupMaker>().member.Count;
                                target = targetList[i].transform;
                            }
                        }

                        GameObject tmp;

                        if (maxScore != 0)
                        {
                            anim.SetTrigger("assult");
                            tmp = Instantiate(shotEff, target.position, Quaternion.identity);
                            yield return new WaitForSeconds(0.05f);

                            // 폭발 범위 내 모든 적에게 공격
                            foreach (var v in tmp.GetComponent<RocketAssulter>().AssultList)
                            {
                                // 공격 진행
                                // 대상이 사망했다면 타겟 리스트에서 제외
                                if (v.GetComponentInParent<ZombieControler>().ReduceHp(status.damage, true))
                                {
                                    //targetList.Remove(target.gameObject);
                                    //target = null;

                                    //if (v == target) target = null;

                                }
                            }
                        }

                        //yield return new WaitForSeconds(2.0f); // 연사 속도
                        yield return new WaitForSeconds(status.shotSpeed);

                        break;


                    // 가장 가까이 있는 적 공격 + 넉백
                    case WeaponType.shotgun:

                        anim.SetTrigger("assult");



                        GameObject tmp2 = Instantiate(shotEff,
                            transform.position + transform.TransformVector(0, 1, 2),
                            Quaternion.Euler(transform.eulerAngles));


                        // 범위 내 적 목록 탐색
                        shotgunRange.SetActive(true);
                        yield return new WaitForSeconds(0.05f);

                        // 범위 내 모든 적에게 공격
                        foreach (var v in shotgunRange.GetComponent<ShotgunAssulter>().AssultList)
                        {
                            // 공격 진행
                            // 대상이 사망했다면 타겟 리스트에서 제외
                            if (v.GetComponentInParent<ZombieControler>().ReduceHp(status.damage, true))
                            {
                                //targetList.Remove(target.gameObject);
                                //target = null;

                                if (v == target) target = null;

                            }
                        }
                        shotgunRange.SetActive(false);

                        //target = null;


                        yield return new WaitForSeconds(status.shotSpeed);

                        break;
                }
            }
            yield return null;
        }

    }


    private void OnTriggerStay(Collider other)
    {
        if (type != WeaponType.rocket && type != WeaponType.pistol)
        {
            if (target != null) return;
        }
        else
        {
            if (type == WeaponType.pistol || type == WeaponType.rocket)
            {
                if (other.CompareTag("root") && !targetList.Contains(other.gameObject))
                {
                    targetList.Add(other.gameObject);
                    // other.GetComponent<ZombieControler>().setNPCObject(gameObject);
                }
            }
        }

        target = other.transform;
    }

    private void OnTriggerExit(Collider other)
    {
        if (type != WeaponType.rocket && type != WeaponType.pistol)
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
        print(targetList);

        if (targetList.Contains(obj)) targetList.Remove(obj);
    }
}
