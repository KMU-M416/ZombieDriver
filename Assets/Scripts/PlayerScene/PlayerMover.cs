﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    public Transform wheel;

    [Header("Sensibility")]
    public float accelSpeed = 20;
    public float angleSpeed = 30;
    public int angleDegree = 60;

    int way; // -1:left 0:forward 1:right

    private void FixedUpdate()
    {
        Movement();
    }



    void Movement()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            //wheel.Rotate(-Vector3.up * angleSpeed * Time.deltaTime);
            wheel.localRotation = Quaternion.Euler(0, -angleDegree * 0.3f, 0);
            way = -1;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            //wheel.Rotate(Vector3.up * angleSpeed * Time.deltaTime);
            wheel.localRotation = Quaternion.Euler(0, angleDegree * 0.3f, 0);
            way = 1;
        }
        else
        {
            wheel.localRotation = Quaternion.Euler(0, 0, 0);
            way = 0;
        }



        float ver = Input.GetAxis("Vertical");

        if (ver != 0)
            transform.Rotate((ver > 0 ? Vector3.up : Vector3.down) * (angleDegree * way) * Time.deltaTime);
        transform.Translate(Vector3.forward * ver * accelSpeed * Time.deltaTime, Space.Self);
        


        /*
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Rotate(Vector3.up * (angleDegree * way) * Time.deltaTime);
            transform.Translate(Vector3.forward * accelSpeed * Time.deltaTime, Space.Self);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Rotate(Vector3.up * (angleDegree * -way) * Time.deltaTime);
            transform.Translate(Vector3.back * accelSpeed * Time.deltaTime, Space.Self);
        }
        */
    }


}