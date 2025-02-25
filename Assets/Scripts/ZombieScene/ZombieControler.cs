﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;


public class ZombieControler : MonoBehaviour
{
    ZombieGenerator ZombieGenerator;

    [HideInInspector] // 해당 좀비 타입
    public ZombieType myType;

    [Header("Zombie Status")]
    public ZombieStatus status;
    [HideInInspector] public int currentHp;
    public Transform target; // 트럭

    [HideInInspector]
    public Animator zombieAnimator;
    [HideInInspector]
    public bool isAttack;
    [HideInInspector]
    public bool isDead;
    bool initRotate = false;

    public NavMeshAgent agent; // AI
    CapsuleCollider _CapSule;
    Rigidbody _Rd;

    IEnumerator co_knockBack;

    private void OnEnable()
    {
        if (status != null)
            currentHp = status.Hp; // 사망과 함께 Pool에 반환되었다가 다시 생성되는 순간 HP 회복

        isDead = isAttack = false;
        _CapSule.enabled = true;

        if (myType == ZombieType.smartAI)
        {
            if (agent == null) agent = GetComponent<NavMeshAgent>();

            agent.enabled = true;
        }
        initRotate = false;
    }

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        _CapSule = GetComponent<CapsuleCollider>();
        _Rd = GetComponent<Rigidbody>();
        ZombieGenerator = transform.parent.parent.GetComponent<ZombieGenerator>();
        zombieAnimator = GetComponent<Animator>();

        Application.targetFrameRate = 60;
    }

    void Start()
    {
        target = TruckGameObject.Truck;
    }

    void Update()
    {
        if (!initRotate)
        {
            initRotate = true;
            transform.Rotate(0, Random.Range(-180, 180), 0);
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
            agent.enabled = false;
            _Rd.velocity = Vector3.zero;
        }
    }

    /// <summary>
    /// 좀비 피격
    /// </summary>
    /// <param name="value">hp 감소 수치</param>
    /// <param name="isKnockBack">넉백이 필요한 경우</param>
    /// <returns> is Dead ? </returns>
    public bool ReduceHp(float value, bool isKnockBack = false)
    {
        currentHp -= (int)value;

        if (currentHp <= 0 && !isDead)
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
        return currentHp;
    }

    /// <summary>
    /// 좀비 공격 정보를 받아오는 함수
    /// </summary>
    /// <param name="type">0: 공격 속도,  1: 공격력</param>
    public int GetAttackInfo(int type)
    {
        if (type == 0) return status.AttackSpeed;
        else if (type == 1) return status.Damage;
        else return status.MoveSpeed;
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

        if (myType == ZombieType.smartAI)
        {
            agent.enabled = false;
           _Rd.velocity = Vector3.zero;
        }

        target = null; // 플레이어 삭제

        zombieAnimator.SetTrigger("isDead");

        _CapSule.enabled = false;

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
