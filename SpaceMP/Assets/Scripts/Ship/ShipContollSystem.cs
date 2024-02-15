using System.Collections;
using System.Collections.Generic;
using AsteroidsNamespace;
using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

public partial struct ShipContollSystem : ISystem
{
    //System responsible for player control and player inputs

    [BurstCompile]
    void OnUpdate(ref SystemState state)
    {
        float deltatime = Time.deltaTime;
        
        //Commandbuffer used for parallel-scheduling timers for bulletdeaths
        EntityCommandBuffer.ParallelWriter ecbParallel = SystemAPI.GetSingleton<BeginInitializationEntityCommandBufferSystem.Singleton>()
            .CreateCommandBuffer(state.WorldUnmanaged).AsParallelWriter();
        
        //Regular Commandbuffer used when instanciating the bullets and controlling the ship
        EntityCommandBuffer ecb2Regular = SystemAPI.GetSingleton<BeginInitializationEntityCommandBufferSystem.Singleton>()
            .CreateCommandBuffer(state.WorldUnmanaged);
        
        //Runs through all entities to give the playerships entity and localtransform.
        //Always need to search for it to be able to update the localtransforms position
        foreach (var VARIABLE in SystemAPI.Query<LocalTransform>().WithAll<BulletPrefabForShip>().WithEntityAccess())
        {
            //Generates a variable with the ships transform
            LocalTransform transform = VARIABLE.Item1;
            
            //Forward controls
            if (Input.GetKey(KeyCode.W))
            {
                transform.Position += transform.Up() * (7 * deltatime);
            }

            //Steer left
            if (Input.GetKey(KeyCode.A))
            {
                float rotation = 2 * deltatime * 100;
                transform.Rotation *= Quaternion.AngleAxis(rotation, Vector3.forward);
            }

            //Steer right
            if (Input.GetKey(KeyCode.D))
            {
                float rotation = 2 * deltatime * 100;
                transform.Rotation *= Quaternion.AngleAxis(-rotation, Vector3.forward);
            }
            
            //Shoot controls
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //Instantiates the bullet
                Entity bullet = ecb2Regular.Instantiate(SystemAPI.GetComponentRO<BulletPrefabForShip>(VARIABLE.Item2).ValueRO.BulletPrefab);
                
                //Sets it's position and rotation to that of the ship
                ecb2Regular.SetComponent(bullet, new LocalTransform
                {
                    Position = transform.Position,
                    Scale = 1,
                    Rotation = transform.Rotation
                });
            }
            
            transform.Scale = 1;
            
            //Updates the ships own transform based on player input
            ecb2Regular.SetComponent(VARIABLE.Item2, transform);
            
            //Decreases the timer on the bullets until they die.
            //Placed the job here due to multiple bullets reducing
            //the overall time until it was zero.
            new BulletTimer()
            {
                Deltatime = deltatime,
                ECB = ecbParallel
                //Parallel scheduling to reduce impact on the game
            }.ScheduleParallel();
        }
    }
    
    //The job that decrease the bullets' death timers
    public partial struct BulletTimer : IJobEntity
    {
        public float Deltatime;
        public EntityCommandBuffer.ParallelWriter ECB;
    
        private void Execute(BulletAspect bulletAspect, [EntityIndexInQuery]int sortKey)
        {
            bulletAspect.DecreaseBulletTimer(Deltatime, ECB, sortKey);
        }
    }
}
