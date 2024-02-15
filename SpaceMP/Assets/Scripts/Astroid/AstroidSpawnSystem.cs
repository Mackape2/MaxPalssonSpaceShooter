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
        //Handles the Instantiation of asteroids
        
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            //Waits to update until the IComponents have started
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
            //Calls the job that Instantiates the asteroids
            new SpawnAstroidJob
            {
                Deltatime = timeDeltaTime,
                ECB = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged)
            }.Run();
        }
        
    } 
    
    [BurstCompile]
    //The job that handles the Instantiation of asteroids
    public partial struct SpawnAstroidJob : IJobEntity 
    {
          public float Deltatime;
          public EntityCommandBuffer ECB;
          private void Execute(SpaceAspect spaceAspect) 
          {
              //Decreases the time until the next asteroid
              spaceAspect.SpawnTimerFloat -= Deltatime;
              
              //Will return if an asteroid isn't supposed to spawn yet
              if(!spaceAspect.TimeToSpawnAsteroid)return;
              spaceAspect.SpawnTimerFloat = spaceAspect.AstroidSpawnRate;

              //Creates the asteroid
              var newAsteroid = ECB.Instantiate(spaceAspect.AsteroidPrefab);
              
              //Picks a random position and rotation for the asteroids and the changes it's transform
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
