using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ZombieStatus
{
    public int hp;
    [HideInInspector] public int currentHp;

    public int moveSpeed;
}

public class ZombieControler : MonoBehaviour
{
    ZombieGenerator ZombieGenerator;

    [Header("Zombie Status")]
    public ZombieStatus status;

    public GameObject rocketNPC, pistolNPC;

    [HideInInspector] // 해당 좀비 타입
    public ZombieType myType;

    IEnumerator co_knockBack;

    private void OnEnable()
    {
        status.currentHp = status.hp; // 사망과 함께 Pool에 반환되었다가 다시 생성되는 순간 HP 회복
    }

    private void OnDisable()
    {
        if (rocketNPC != null && pistolNPC != null)
        {
            rocketNPC.GetComponent<AssultController>().deleteList(gameObject);
            pistolNPC.GetComponent<AssultController>().deleteList(gameObject);
        }
    }

    void Start()
    {
        ZombieGenerator = transform.parent.parent.GetComponent<ZombieGenerator>();

        rocketNPC = GameObject.Find("NPC_Female_Rocket").GetComponentInChildren<AssultController>().gameObject;
        pistolNPC = GameObject.Find("NPC_Male_Pistol").GetComponentInChildren<AssultController>().gameObject;
    }

    void Update()
    {
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
    }

    /// <summary>
    /// 로켓과 권총 NPC만 해당
    /// 해당 좀비가 죽거나, 범위 내에 없을시 타겟리스트를 지우기 위함
    /// </summary>
    /// <param name="NPC"> NPC 오브젝트 </param>
    //public void setNPCObject(GameObject NPC)
    //{
    //    targetNPC = NPC;
    //}

    /// <summary>
    /// 좀비 피격
    /// </summary>
    /// <param name="value">hp 감소 수치</param>
    /// <param name="isKnockBack">넉백이 필요한 경우</param>
    /// <returns> is Dead ? </returns>
    public bool ReduceHp(int value, bool isKnockBack = false)
    {
        status.currentHp -= value;

        if (status.currentHp <= 0)
        {
            Die();
            return true;
        }

        if (isKnockBack)
        {
            co_knockBack = KnockBack();
            StartCoroutine(co_knockBack);
        }

        return false;
    }

    IEnumerator KnockBack()
    {
        float timer = 0f;
        float speed = 15;
        while (timer < 0.2f)
        {
            timer += Time.deltaTime;

            transform.Translate(Vector3.back * Time.deltaTime * speed, Space.Self);
            yield return null;
        }

        yield return null;
    }


    public int GetHp()
    {
        return status.currentHp;
    }

    /// <summary>
    /// 
    /// </summary>
    void Die()
    {
        ZombieGenerator.returnObj(gameObject, myType);
    }

}
