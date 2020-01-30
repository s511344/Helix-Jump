using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[RequireComponent(typeof( BoxCollider))]
public class EventObj : MonoBehaviour
{
    Action callBack;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Ball")
        {
            callBack?.Invoke();
        }
    }
    public void AddTriggerEvent(Action a)
    {
        callBack += a;
    }
}
