using Unity.Entities;
using Unity.Mathematics;

namespace AsteroidsNamespace
{
    public struct Asteroids : IComponentData
    {
        public float2 FieldDimensions;
        public int NumberOfSpawnpoints;
        public Entity SpawnpointPrefab;
    }
}