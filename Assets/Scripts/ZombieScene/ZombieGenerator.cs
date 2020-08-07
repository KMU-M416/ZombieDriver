using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ZombieType
{
    normalAI,
    smartAI,
    randomAI
}

public class ZombieGenerator : MonoBehaviour
{
    [Header("Zombie")]
    public GameObject[] Zombies;
    Transform zombiesParent;

    [Header("Setting")]
    public float spawnTime;
    [Tooltip("한번 소환때 소환할 수")]
    public int spawnCount;
    [Tooltip("최대 소환 수")]
    public int maxSpawnCount;
    public ZombieType zombieType;

    // 현재 포탈에서 소환한 좀비 수
    int currentSpawnCount = 0;

    // 풀링
    Queue<GameObject> poolingNormalZombie = new Queue<GameObject>();
    Queue<GameObject> poolingSmartZombie = new Queue<GameObject>();

    // 코루틴 담음
    IEnumerator generator;

    // Start is called before the first frame update
    void Start()
    {
        zombiesParent = transform.GetChild(0).transform;

        Init();

        generator = Generator();
        StartCoroutine(generator);
    }

    // 초기화 함수
    void Init()
    {
        for (int i = 0; i < 100; i++)
        {
            var zombieNormalType = Instantiate(Zombies[0], zombiesParent);
            var zombieSmartType = Instantiate(Zombies[1], zombiesParent);

            zombieNormalType.gameObject.SetActive(false);
            zombieSmartType.gameObject.SetActive(false);

            zombieNormalType.GetComponent<ZombieControler>().myType = ZombieType.normalAI;
            zombieSmartType.GetComponent<ZombieControler>().myType = ZombieType.smartAI;

            poolingNormalZombie.Enqueue(zombieNormalType);
            poolingSmartZombie.Enqueue(zombieSmartType);
        }

        print("좀비 풀링 완료");
    }

    // 설정값에 따른 좀비 생성 함수
    IEnumerator Generator()
    {
        print("생성 시작");

        int type;

        while (true)
        {
            for (int i = 0; i < spawnCount; i++)
            {
                if (zombieType == ZombieType.normalAI) type = 0;
                else if (zombieType == ZombieType.smartAI) type = 1;
                else type = Random.Range(0, 2);

                if (type == 0)
                {
                    var zombieNormalType = poolingNormalZombie.Dequeue();
                    zombieNormalType.gameObject.SetActive(true);
                }
                else
                {
                    var zombieSmartType = poolingSmartZombie.Dequeue();
                    zombieSmartType.gameObject.SetActive(true);
                }

                currentSpawnCount++;
            }

            if (currentSpawnCount > maxSpawnCount)
            {
                print("소환 수가 최대치 입니다.");
                StopCoroutine(generator);
            }               

            yield return new WaitForSeconds(spawnTime);
        }
    }

    // 빌려간거 다시 돌려줌
    public void returnObj(GameObject obj, ZombieType zombieType)
    {
        obj.SetActive(false);

        if (zombieType == ZombieType.normalAI) poolingNormalZombie.Enqueue(obj);
        else poolingSmartZombie.Enqueue(obj);
    }
}
