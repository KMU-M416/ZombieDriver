using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    public Transform wheel;

    [Header("Sensibility")]
    public float speed;

    private void FixedUpdate()
    {
        Movement();
    }



    void Movement()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.Self);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(Vector3.back * speed * Time.deltaTime, Space.Self);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            wheel.Rotate(-Vector3.up * 30 * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            wheel.Rotate(Vector3.up * 30 * Time.deltaTime);
        }
    }
}
