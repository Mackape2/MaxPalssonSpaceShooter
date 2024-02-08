using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace AsteroidsNamespace
{
    [UpdateInGroup(typeof(InitializationSystemGroup))]
    public partial struct AstroidSpawnSystem : ISystem
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
            var ecbSingleton = SystemAPI.GetSingleton<BeginInitializationEntityCommandBufferSystem.Singleton>();
            state.CompleteDependency();
            new SpawnAstroidJob
            {
                Deltatime = timeDeltaTime,
                ECB = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged)
            }.Run();
            
            
        }
        
    } 
    
    [BurstCompile]
    public partial struct SpawnAstroidJob : IJobEntity 
    {
          public float Deltatime;
          public EntityCommandBuffer ECB;
          private void Execute(SpaceAspect spaceAspect) 
          {
              spaceAspect.SpawnTimerFloat -= Deltatime;
              
              if(!spaceAspect.TimeToSpawnAsteroid)return;
              spaceAspect.SpawnTimerFloat = spaceAspect.AstroidSpawnRate;

              var newAsteroid = ECB.Instantiate(spaceAspect.AsteroidPrefab);
              float3 randomPosition = spaceAspect.GetRandomPosition();
              Quaternion randomRotation = spaceAspect.GetRandomRotation();
              randomPosition.z = 0;
              ECB.SetComponent(newAsteroid, 
                  new LocalTransform
                  {
                      Position = randomPosition, Scale = 1,
                      Rotation = randomRotation
                  });
          }
    }
}
