using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace AsteroidsNamespace
{
    [UpdateInGroup(typeof(InitializationSystemGroup))]
    public struct Asteroids : IComponentData
    {
        public float2 FieldDimensions;
        public Entity AsteroidPrefab;
        public float AsteroidSpawnRate;
    }
   
    public struct AsteroidSpawnTimer : IComponentData
    {
        public float TimeValue;
    }

   
   
}