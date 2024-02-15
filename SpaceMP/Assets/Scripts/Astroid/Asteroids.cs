using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace AsteroidsNamespace
{
    //Different components meant for the asteroids
    [UpdateInGroup(typeof(InitializationSystemGroup))]
    public struct Asteroids : IComponentData
    {
        public float2 FieldDimensions;
        public Entity AsteroidPrefab;
        public float AsteroidSpawnRate;
    }
   
    //Timer for when asteroids shall spawn
    public struct AsteroidSpawnTimer : IComponentData
    {
        public float TimeValue;
    }

   
   
}