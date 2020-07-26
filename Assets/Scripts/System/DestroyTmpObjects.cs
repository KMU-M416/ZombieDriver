using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTmpObjects : MonoBehaviour
{
    public GameObject[] obj;

    private void Start()
    {
        foreach(var v in obj){
            Destroy(v);
        }
    }
}
