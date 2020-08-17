using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public enum CamType
{
    Type1,
    Type2,
}

public class CamMover : MonoBehaviour
{
    
    public Transform target;

    [Header("Components")]
    public Camera cam;

    [Header("Sensibility")]
    public CamType camType = CamType.Type1;

    public float verticalDist = 25; // (ortho)15
    public float horizontalDist = 15; // (ortho)10
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
        cam.orthographic = false;
        cam.nearClipPlane = 0.1f;

        transform.position =
            Vector3.Lerp(transform.position,
            target.transform.position + target.up * verticalDist + -target.forward * horizontalDist,
            Time.deltaTime * camLerpSpeed);

        transform.LookAt(target);
    }

    void Cam2()
    {
        if(target.position.x >= -183.1f)
        {
            cam.orthographic = true;
            cam.orthographicSize = verticalDist;
            cam.nearClipPlane = -50f;

            transform.position = target.position + new Vector3(-horizontalDist, verticalDist, -horizontalDist);

            transform.LookAt(target);
        }
    }
}
