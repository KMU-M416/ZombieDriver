using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

[System.Serializable]
public class PlayerStatus
{

    [Header("Game Status")]
    public int hp = 500;
    [HideInInspector] public int curHp;

    [Header("Sensibility")]
    public float accelSpeed = 20;
    public float angleSpeed = 30;
    public int angleDegree = 60;

    [Header("Components")]
    public Image hpBar;
}

public class PlayerMover : MonoBehaviour
{
    public static PlayerMover instance;

    [Header("Components")]
    public Transform wheelL, wheelR;
    
    public PlayerStatus status;

    int way; // -1:left 0:forward 1:right


    private void Start()
    {
        instance = this;

        Init();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    void Init()
    {
        status.curHp = status.hp;
    }


    void Movement()
    {


        float ver = Input.GetAxis("Vertical");
        if (ver != 0)
            transform.Rotate((ver > 0 ? Vector3.up : Vector3.down) * (status.angleDegree * way) * Time.deltaTime);
        
        transform.Translate(Vector3.forward * ver * status.accelSpeed * Time.deltaTime, Space.Self);




        if (Input.GetKey(KeyCode.LeftArrow))
        {
            //wheel.Rotate(-Vector3.up * angleSpeed * Time.deltaTime);
            wheelL.localRotation = Quaternion.Euler(0, -status.angleDegree * 0.5f, 0);
            wheelR.localRotation = Quaternion.Euler(0, -status.angleDegree * 0.5f, 0);
            way = -1;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            //wheel.Rotate(Vector3.up * angleSpeed * Time.deltaTime);
            wheelL.localRotation = Quaternion.Euler(0, status.angleDegree * 0.5f, 0);
            wheelR.localRotation = Quaternion.Euler(0, status.angleDegree * 0.5f, 0);
            way = 1;
        }
        else
        {
            wheelL.localRotation = Quaternion.Euler(0, 0, 0);
            wheelR.localRotation = Quaternion.Euler(0, 0, 0);
            way = 0;
        }
    }

    /// <summary>
    /// 좀비에게 피격시 호출되는 함수
    /// </summary>
    /// <param name="damage">피해량(데미지)</param>
    public void Attacked(int damage)
    {
        status.curHp -= damage;

        status.hpBar.fillAmount = status.curHp / (float)status.hp;

        print($"[TEST] player attacked ({status.curHp}/{status.hp})");

        CamEffManager.instance.CallAttackedEff();
        

        if(status.curHp <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        CamEffManager.instance.CallGameOverEff();


        // 추가 플레이어 조작 불가능
        this.enabled = false;
    }
}
