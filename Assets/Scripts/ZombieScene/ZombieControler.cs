using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZombieControler : MonoBehaviour
{
    ZombieGenerator ZombieGenerator;

    [HideInInspector] // 해당 좀비 타입
    public ZombieType myType;
    
    // 타임과 이 좀비가 죽을 타임
    // 테스트 용으로써 나중에 수정 예정
    float timer;
    [SerializeField]
    int destoryTime;

    private void OnEnable()
    {
        timer = 0;
        destoryTime = Random.Range(2, 30);
    }
    
    void Start()
    {
        ZombieGenerator = transform.parent.parent.GetComponent<ZombieGenerator>();
    }
    
    void Update()
    {
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);

        timer += Time.deltaTime;

        if (timer > destoryTime)
        {
            ZombieGenerator.returnObj(gameObject, myType);
            timer = 0;
        }
    }


}
