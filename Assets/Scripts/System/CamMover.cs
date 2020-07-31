using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CamType
{
    Type1,
    Type2,
    Type3,
}

public class CamMover : MonoBehaviour
{
    
    public Transform target;

    [Header("Sensibility")]
    public CamType camType = CamType.Type1;

    public float verticalDist = 25;
    public float horizontalDist = 15;
    public float camLerpSpeed = 2;

    // === private variables ===

    private void FixedUpdate()
    {
        switch (camType)
        {
            case CamType.Type1:
                Cam1();
                break;
            case CamType.Type2:
                Cam2();
                break;
        }
    }

    void Cam1()
    {
        transform.position =
            Vector3.Lerp(transform.position,
            target.transform.position + target.up * verticalDist + -target.forward * horizontalDist,
            Time.deltaTime * camLerpSpeed);

        transform.LookAt(target);
    }

    void Cam2()
    {
        transform.position = target.position + new Vector3(-horizontalDist, verticalDist, -horizontalDist);

        transform.LookAt(target);
    }
}
