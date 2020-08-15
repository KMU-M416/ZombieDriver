using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class ZombieHpBarController : MonoBehaviour
{

    Image hpBar;
    ZombieControler zc;

    private void Start()
    {
        zc = GetComponentInParent<ZombieControler>();
        hpBar = GetComponentInChildren<Image>();
    }

    private void Update()
    {
        transform.eulerAngles = new Vector3(45, 45, 0);
        hpBar.fillAmount = zc.status.currentHp / (float)zc.status.hp;
    }

}
