using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class InitialPosition : MonoBehaviour
{
    private Vector3 initialPos;
    private Quaternion initialRot;
        
    // Start is called before the first frame update
    void Start()
    {
        initialPos = transform.position;
        initialRot = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void resetPosition()
    {
        transform.position = initialPos;
        transform.rotation = initialRot;
    }

}