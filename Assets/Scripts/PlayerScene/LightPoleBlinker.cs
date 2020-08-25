using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightPoleBlinker : MonoBehaviour
{

    GameObject[] lights = new GameObject[2];

    float timer = 0f;
    int randSec = 0;

    // Start is called before the first frame update
    private void Start()
    {
        lights[0] = transform.GetChild(0).gameObject;
        lights[1] = transform.GetChild(1).gameObject;
    }

    // Update is called once per frame
    private void Update()
    {
        timer += Time.deltaTime;


        if(timer > randSec)
        {
            randSec = Random.Range(1, 5);
            timer = 0;

            lights[0].SetActive(!lights[0].activeSelf);
            lights[1].SetActive(!lights[1].activeSelf);
        }
    }
    
}
