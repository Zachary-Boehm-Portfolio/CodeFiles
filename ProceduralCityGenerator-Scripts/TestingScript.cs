using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TestingScript : MonoBehaviour
{
    void Awake()
    {
        UnityEngine.Random.InitState(420);
    }
    // Start is called before the first frame update
    void Start()
    {
        int x = 0;
        x = x == 1 ? 0:1;
        Debug.Log(x);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

