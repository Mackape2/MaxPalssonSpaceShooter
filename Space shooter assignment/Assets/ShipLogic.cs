using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ShipLogic : MonoBehaviour
{
    [SerializeField] 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(5, 90);
    }
}
