using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieGroupMaker : MonoBehaviour
{
    public List<GameObject> member = new List<GameObject>();

    private void OnEnable()
    {
        member.Clear();
        member.Add(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CheckedGroup"))
        {
            member.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("CheckedGroup"))
        {
            member.Remove(other.gameObject);
        }
    }

    // 좀비 비활성화시 onTriggerExit 함수가 미작동하여 아래 함수로 멤버 제거
    public void decreaseScore(GameObject removeMember)
    {
        for (int i = 0; i < member.Count; i++)
            member[i].GetComponent<ZombieGroupMaker>().member.Remove(removeMember);
    }
}
