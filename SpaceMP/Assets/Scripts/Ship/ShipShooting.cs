using System.Collections.Generic;
using System.Linq;
using AsteroidsNamespace;
using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

[BurstCompile]
[UpdateAfter(typeof(ShipContollSystem))]
[UpdateAfter(typeof(BulletAspect))]

public partial struct ShipShooting : ISystem
{
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<BulletSpeed>();
    }

    public void OnDestroy(ref SystemState state)
    {
        
    }
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var deltaTime = SystemAPI.Time.DeltaTime;
        var ecb = SystemAPI.GetSingleton<BeginInitializationEntityCommandBufferSystem.Singleton>();
        
        //Starts a job for each asteroid that checks if they've been hit by any bullet
        foreach (var VARIABLE in SystemAPI.Query<LocalTransform>().WithAll<AstroidSpeed>().WithEntityAccess())
        {
            new AsteroidHitJob()
        {
            //Asteroid enitity
            HitEntity = VARIABLE.Item2,
            
            //Asteroid localtransform
            HitLocation = VARIABLE.Item1,
            EntityCommandBuffer = ecb.CreateCommandBuffer(state.WorldUnmanaged)
        }.Schedule();
        }
        
        state.CompleteDependency();
        //Job that handles movement of bullets
        new BulletMovementJob
        {
            DeltaTime = deltaTime
        }.Schedule();
        
        
    }
}


    [BurstCompile]
    public partial struct BulletMovementJob : IJobEntity
    {
        public float DeltaTime;

        //Calls the script that moves the bullet forward
        private void Execute(BulletAspect bulletAspect)
        {
            bulletAspect.BulletMove(DeltaTime);
        }
    }

    public partial struct AsteroidHitJob : IJobEntity
    {
        
        public EntityCommandBuffer EntityCommandBuffer;
        public LocalTransform HitLocation;
        public Entity HitEntity;
        private void Execute(BulletAspect bulletAspect)
        {
            //The function measures the distance between the bullet and asteroids to determine
            //if it's a hit or not.
            float distance = bulletAspect.GetHitDistance(HitLocation);
             if(distance <= 1)
                 //Destroys the asteroid entity hit
                 EntityCommandBuffer.DestroyEntity(HitEntity);
             
             //Destroys the bullet if it's timer has run out
             if (bulletAspect.BulletDeath)
             {
                 EntityCommandBuffer.DestroyEntity(bulletAspect.GetBullet());
             }
             
        }
    }

