using System;
using System.Collections;
using Unity.Entities;
using System.Collections.Generic;
using AsteroidsNamespace;
using Unity.Mathematics;
using UnityEngine;

namespace AsteroidsNamespace
{
    public class ShipControll : MonoBehaviour
    {
        public GameObject BulletPrefab;

        void Update()
        {
          
        }
    }

        public class ShipBaker : Baker<ShipControll>
        {
            
            public override void Bake(ShipControll authoring)
            {
                //AddComponent<ShipPosition>();
                AddComponent(new Bullet
                {
                    BulletPrefab = GetEntity(authoring.BulletPrefab)
                });
                
            }
        }
}




