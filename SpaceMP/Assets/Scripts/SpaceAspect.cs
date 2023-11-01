using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace AsteroidsNamespace
{
    public readonly partial struct SpaceAspect : IAspect
    {
        public readonly Entity Entity;

        //Mentalnote: RefRO = reference Read only
        //            RefRW = reference Read Write
        private readonly RefRO<Asteroids> _asteroids;
        private readonly RefRW<SpaceRandomGen> _spaceRandomGen;
        private readonly RefRW<LocalTransform> _transform;
        private LocalTransform Transform => _transform.ValueRW;

        public int NumberAsteroidsToSpawn => _asteroids.ValueRO.NumberOfSpawnpoints;
        public Entity AsteroidPrefab => _asteroids.ValueRO.SpawnpointPrefab;


        private float3 GetRandomPosition()
        {
            float3 randomPosition;
            do
            {
                randomPosition = _spaceRandomGen.ValueRW.Value.NextFloat3(MinCorner,MaxCorner);
                
            } while (math.distancesq(Transform.Position, randomPosition) <= SpawnFreeArea);


            return randomPosition;
            
        }
        private float3 MinCorner => Transform.Position - (_asteroids.ValueRO.FieldDimensions.x * (float)0.5);
        private float3 MaxCorner => Transform.Position + (_asteroids.ValueRO.FieldDimensions.y * (float)0.5);
        public float3 Position => Transform.Position;

        private const float SpawnFreeArea = 100;

    }
}