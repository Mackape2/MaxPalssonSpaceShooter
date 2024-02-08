using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace AsteroidsNamespace
{
    
    [UpdateInGroup(typeof(InitializationSystemGroup))]
    public partial struct ShipControlls : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<Asteroids>();
                
        }
            
        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {
                 
        }
            
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
                
            var timeDeltaTime = SystemAPI.Time.DeltaTime;
            Entity spaceShip = Entity.Null;
            var ecbSingleton = SystemAPI.GetSingleton<BeginInitializationEntityCommandBufferSystem.Singleton>();
            state.CompleteDependency();
            new ShipMovement()
            {
                SpaceShip = spaceShip,
                ECB = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged)    
            }.Run();
                
                
        }
            
    }
    public partial struct ShipMovement : IJobEntity
    {
        public Entity SpaceShip;
        public EntityCommandBuffer ECB;
        private void Execute(SpaceAspect spaceAspect)
        {
            float _speed = 2;
            float _twist = 2;
            if (SpaceShip == Entity.Null)
                SpaceShip = ECB.Instantiate(spaceAspect.SpaceShipPrefab);
            if (Input.GetKey(KeyCode.W))
            {
                
                SpaceShip.transform.position += tranform.up * _speed;
            }

            if (Input.GetKey(KeyCode.A))
            {
                transform.rotation *=  Quaternion.AngleAxis(_twist, Vector3.forward);
            }
            if (Input.GetKey(KeyCode.D))
            { 
                //transform.rotation *=  Quaternion.AngleAxis(-_twist, Vector3.forward);
                
            }
            
        }
    }
}



