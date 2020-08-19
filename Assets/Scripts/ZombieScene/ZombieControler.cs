using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

[System.Serializable]
public class ZombieStatus
{
    public int hp;
    [HideInInspector] public int currentHp;

    public int moveSpeed;
    public int attackSpeed;
    public int damage;
}

public class ZombieControler : MonoBehaviour
{
    ZombieGenerator ZombieGenerator;

    [HideInInspector] // 해당 좀비 타입
    public ZombieType myType;

    [Header("Zombie Status")]
    public ZombieStatus status;
    public Transform target; // 트럭

    [HideInInspector]
    public Animator zombieAnimator;
    [HideInInspector]
    public bool isAttack;
    [HideInInspector]
    public bool isDead;
    bool initRotate = false;

   public NavMeshAgent agent; // AI

    IEnumerator co_knockBack;

    private void OnEnable()
    {
        status.currentHp = status.hp; // 사망과 함께 Pool에 반환되었다가 다시 생성되는 순간 HP 회복

        isDead = isAttack = false;
        GetComponent<CapsuleCollider>().enabled = true;

        if (myType == ZombieType.smartAI)
        {
            if (agent == null) agent = GetComponent<NavMeshAgent>();

            agent.enabled = true;

            target = GameObject.Find("Truck").transform;
        }
        initRotate = false;
    }

    private void OnDisable()
    {
        //if (rocketNPC != null && pistolNPC != null)
        //{
        //    rocketNPC.GetComponent<AssultController>().deleteList(gameObject);
        //    pistolNPC.GetComponent<AssultController>().deleteList(gameObject);
        //}
    }

    void Start()
    {
        ZombieGenerator = transform.parent.parent.GetComponent<ZombieGenerator>();
        zombieAnimator = GetComponent<Animator>();

        //rocketNPC = GameObject.Find("NPC_Female_Rocket").GetComponentInChildren<AssultController>().gameObject;
        //pistolNPC = GameObject.Find("NPC_Male_Pistol").GetComponentInChildren<AssultController>().gameObject;
    }

    void Update()
    {
        if (!initRotate)
        {
            initRotate = true;
            transform.Rotate(0, Random.Range(-359, 360), 0);
        }

        if (!isDead && !isAttack)
        {
            if (myType == ZombieType.smartAI)
            {
                agent.enabled = true;
                zombieAnimator.SetBool("isWalk", true);
                agent.SetDestination(target.position);
            }
            else
            {
                transform.position = new Vector3(transform.position.x, 0, transform.position.z);
            }
        }
        else if (myType == ZombieType.smartAI)
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            agent.enabled = false;
        }
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
    public bool ReduceHp(float value, bool isKnockBack = false)
    {
        status.currentHp -= (int)value;

        if (status.currentHp <= 0 && !isDead)
        {
            isDead = true;           
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
    /// 좀비 공격 정보를 받아오는 함수
    /// </summary>
    /// <param name="type">0: 공격 속도,  1: 공격력</param>
    public int GetAttackInfo(int type)
    {
        if (type == 0) return status.attackSpeed;
        else return status.damage;
    }

    /// <summary>
    /// 
    /// </summary>
    void Die()
    {
        if (GameObject.Find("NPC_Rocket(Clone)") == true)
        {
            AssultController rocketNPC = GameObject.Find("NPC_Rocket(Clone)").GetComponentInChildren<AssultController>();
            rocketNPC.deleteList(gameObject);
        }

        if (GameObject.Find("NPC_Pistol(Clone)") == true)
        {
            AssultController pistolNPC = GameObject.Find("NPC_Pistol(Clone)").GetComponentInChildren<AssultController>();
            pistolNPC.deleteList(gameObject);
        }

        //if (GameObject.Find("NPC_Rifle(Clone)") == true)
        //{
        //    AssultController pistolNPC = GameObject.Find("NPC_Rifle(Clone)").GetComponentInChildren<AssultController>();
        //    pistolNPC.deleteList(gameObject);
        //}

        if (myType == ZombieType.smartAI)
        {
            agent.enabled = false;
            GetComponent<Rigidbody>().velocity = Vector3.zero;
        }

        target = null; // 플레이어 삭제

        zombieAnimator.SetTrigger("isDead");

        GetComponent<CapsuleCollider>().enabled = false;

        StartCoroutine(DieAni());

    }

    // 애니메이션 재생 후 애니메이션의 절반이 넘어가면 오브젝트 반환
    IEnumerator DieAni()
    {
        while (true)
        {
            if (zombieAnimator.GetCurrentAnimatorStateInfo(0).IsName("10-death_fall_backward") && zombieAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.5f)
                ZombieGenerator.returnObj(gameObject, myType);
            yield return null;
        }
    }
}
