using AsteroidsNamespace;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public partial struct ShipShooting : ISystem
{
    // Start is called before the first frame update
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<Bullet>();
    }

    public void OnDestroy(ref SystemState state)
    {
        
    }
    // Update is called once per frame
    public void OnUpdate(ref SystemState state)
    {
        
            var deltaTime = SystemAPI.Time.DeltaTime;
            var ecbSingleton = SystemAPI.GetSingleton<BeginInitializationEntityCommandBufferSystem.Singleton>();
            // new BulletMovementJob()
            // {
            //     ECB = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged)
            // }.Schedule();
            new ShootJob
            {
                DeltaTime = deltaTime,
                ECB = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged)
            }.Schedule();
        } 

    }

    public partial struct ShootJob : IJobEntity
    {
        public EntityCommandBuffer ECB; 
        public float DeltaTime;

        private void Execute(BulletAspect bulletAspect)
        {
            bulletAspect.BulletMove(DeltaTime);
        
        }
    }

    // public partial struct BulletMovementJob : IJobEntity
    // {
    //     
    //     public EntityCommandBuffer ECB;
    //     private void Execute(BulletAspect bulletAspect)
    //     {
    //     }
    // }

