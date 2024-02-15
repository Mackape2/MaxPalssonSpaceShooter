using System;
using System.Collections;
using Unity.Entities;
using System.Collections.Generic;
using AsteroidsNamespace;
using Unity.Transforms;
using Unity.VisualScripting;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    public float BulletsSpeed;
    public float BulletSurviveTime;
}

//Attaches important components to the bullets
public class BulletBaker : Baker<BulletBehavior>
    {
        public override void Bake(BulletBehavior authoring)
        {
            AddComponent(new BulletSpeed{ Speed = authoring.BulletsSpeed });
            AddComponent(new BulletSurviveTimer{ TimeValue = authoring.BulletSurviveTime});
        }
    }

