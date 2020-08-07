using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        transform.parent.LookAt(other.transform);
    }
}
