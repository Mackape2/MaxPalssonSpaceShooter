using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class ShipControll : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float _speed;
    [SerializeField] private float _twist;

    // Update is called once per frame

    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            
            transform.position += transform.up * (_speed * Time.deltaTime);
        }
        if(Input.GetKey(KeyCode.A))
            transform.rotation *=  Quaternion.AngleAxis(_twist, Vector3.forward);
        if(Input.GetKey(KeyCode.D))
            transform.rotation *=  Quaternion.AngleAxis(-_twist, Vector3.forward);
    }
}



