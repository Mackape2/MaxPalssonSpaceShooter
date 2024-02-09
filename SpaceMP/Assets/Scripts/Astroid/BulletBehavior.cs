using System;
using System.Collections;
using Unity.Entities;
using System.Collections.Generic;
using AsteroidsNamespace;
using Unity.Mathematics;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public class BulletBaker : Baker<BulletBehavior>
    {
        public override void Bake(BulletBehavior authoring)
        {
            AddComponent<BulletPosition>();
        }
    }
}
