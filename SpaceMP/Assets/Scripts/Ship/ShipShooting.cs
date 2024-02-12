using System.Collections.Generic;
using AsteroidsNamespace;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

[BurstCompile]
[UpdateAfter(typeof(ShipContollSystem))]
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







            // new BulletMovementJob()
            // {
            //     ECB = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged)
            // }.Schedule();
    // public partial struct BulletMovementJob : IJobEntity
    // {
    //     
    //     public EntityCommandBuffer ECB;
    //     private void Execute(BulletAspect bulletAspect)
    //     {
    //     }
    // }

