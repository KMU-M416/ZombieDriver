using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMover : MonoBehaviour
{
    public Transform target;
    [Header("Sensibility")]
    public float verticalDist = 10;
    public float horizontalDist = 10;

    float timer = 0;

    private void FixedUpdate()
    {
        timer += Time.deltaTime;
        transform.position =
            Vector3.Lerp(transform.position,
            target.transform.position + target.up * verticalDist + -target.forward * horizontalDist,
            Time.deltaTime * 2);

        transform.LookAt(target);
    }
}
