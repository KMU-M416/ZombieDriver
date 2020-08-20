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

public static class TruckGameObject
{
   public static Transform Truck;
}

public class PlayerMover : MonoBehaviour
{
    public static PlayerMover instance;

    [Header("Components")]
    public Transform wheelL, wheelR;

    public PlayerStatus status;

    int way; // -1:left 0:forward 1:right

    AudioSource audioSource;

    private void Awake() => TruckGameObject.Truck = transform;

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

        audioSource = GetComponent<AudioSource>();
    }


    void Movement()
    {
        float ver = Input.GetAxis("Vertical");

        if (ver != 0)
        {
            audioSource.volume = Mathf.Lerp(audioSource.volume, 0.2f, 0.1f);
            transform.Rotate((ver > 0 ? Vector3.up : Vector3.down) * (status.angleDegree * way) * Time.deltaTime);

            // 엔진사운드
            audioSource.pitch = Mathf.Lerp(audioSource.pitch, ver * 1.7f, 0.05f);
        }
        else
        {
            // 멈춰 있으면 시동걸려 있는 소리를 내기위한 사운드 조절 후 피치 조절
            if (audioSource.pitch <= 0.3f)
            {
                audioSource.volume = Mathf.Lerp(audioSource.volume, 0.4f, 0.1f);
                audioSource.pitch = 0.3f;
            }
            else audioSource.pitch = Mathf.Lerp(audioSource.pitch, ver * 1.7f, 0.05f);
        }

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

        //print($"[TEST] player attacked ({status.curHp}/{status.hp})");

        CamEffManager.instance.CallAttackedEff();


        if (status.curHp <= 0)
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
