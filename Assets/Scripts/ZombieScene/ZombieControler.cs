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

    [Header("Sensibilities")]
    
    [HideInInspector] // 해당 좀비 타입
    public ZombieType myType;
    
    [SerializeField]
    int destoryTime;
    // 타임과 이 좀비가 죽을 타임
    // 테스트 용으로써 나중에 수정 예정
    float timer;

    IEnumerator co_knockBack;

    private void OnEnable()
    {
        timer = 0;
        destoryTime = Random.Range(2, 30);

        status.currentHp = status.hp; // 사망과 함께 Pool에 반환되었다가 다시 생성되는 순간 HP 회복
    }
    
    void Start()
    {
        ZombieGenerator = transform.parent.parent.GetComponent<ZombieGenerator>();
    }
    
    void Update()
    {
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);

        //timer += Time.deltaTime;

        //if (timer > destoryTime)
        //{
        //    ZombieGenerator.returnObj(gameObject, myType);
        //    timer = 0;
        //}
    }


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
        while(timer < 0.2f)
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
