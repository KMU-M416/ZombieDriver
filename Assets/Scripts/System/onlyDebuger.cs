using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onlyDebuger : MonoBehaviour
{
    public Transform Truck;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.R))
        {
            Truck.rotation = Quaternion.Euler(0, 0, 0);
            Truck.GetComponent<Rigidbody>().velocity = Vector3.zero;
            Truck.GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, 0);
            Truck.position = new Vector3(-10, 3, -3);
        }
    }
}
