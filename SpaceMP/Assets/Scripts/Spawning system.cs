using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.VisualScripting;

namespace AsteroidsNamespace
{
    [BurstCompile]
    [UpdateInGroup(typeof(InitializationSystemGroup))]
    public partial struct SpawningSystem : ISystem
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
            state.Enabled = false;
            var spaceEntitiy = SystemAPI.GetSingletonEntity<Asteroids>();
            var space = SystemAPI.GetAspect<SpaceAspect>(spaceEntitiy);

            var ecb = new EntityCommandBuffer(Allocator.Temp);

            for (int i = 0; i < space.NumberAsteroidsToSpawn; i++)
            {
                ecb.Instantiate(space.AsteroidPrefab);
            }
            
            ecb.Playback(state.EntityManager);
        }

    }
}