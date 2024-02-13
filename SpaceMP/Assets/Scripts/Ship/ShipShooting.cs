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
        Entity asd = Entity.Null;
        LocalTransform fds = new LocalTransform();
        foreach (var VARIABLE in SystemAPI.Query<LocalTransform>().WithAll<AstroidSpeed>().WithEntityAccess())
        {
            //Debug.Log(VARIABLE.Item2.ToString());
            asd = VARIABLE.Item2;
            fds = VARIABLE.Item1;
        }
        new BulletMovementJob()
        {
            Deltatime = deltaTime,
            HitEntity = asd,
            HitLocation = fds,
            EntityCommandBuffer = ecb.CreateCommandBuffer(state.WorldUnmanaged)
        }.Schedule();
        
        state.CompleteDependency();
        new ShootJob
        {
            DeltaTime = deltaTime
        }.Schedule();
        
        
    }
}


    [BurstCompile]
    public partial struct ShootJob : IJobEntity
    {
        public float DeltaTime;

        private void Execute(BulletAspect bulletAspect)
        {
            bulletAspect.BulletMove(DeltaTime);
        }
    }

    public partial struct BulletMovementJob : IJobEntity
    {
        public float Deltatime;
        public EntityCommandBuffer EntityCommandBuffer;
        public LocalTransform HitLocation;
        public Entity HitEntity;
        private void Execute(BulletAspect bulletAspect)
        {
            
             if (bulletAspect.BulletDeath)
             {
                 EntityCommandBuffer.DestroyEntity(bulletAspect.GetBullet());
             }
            float distance = bulletAspect.GetHitDistance(HitLocation, Deltatime);
             if(distance <= 1)
                 EntityCommandBuffer.DestroyEntity(HitEntity);
             
        }
    }

