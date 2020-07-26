using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssultController : MonoBehaviour
{
    public Transform target;

    private void Update()
    {
        Assult();
    }

    void Assult()
    {
        if (target == null) return;

        Debug.DrawLine(transform.position, target.position, Color.red);
    }
    
    private void OnTriggerStay(Collider other)
    {

        if (target != null) return;

        target = other.transform.root;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.root == target) target = null;
    }
}
