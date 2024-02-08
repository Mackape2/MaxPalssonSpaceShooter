using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

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
        
        private readonly RefRW<AsteroidSpawnTimer> _spawntimer;
        private LocalTransform Transform => _transform.ValueRW;
        
       

       
        
        public struct SpawnPointsArray : IComponentData
        {
            public BlobAssetReference<SpawnPointsArrayBlob> Value;
        }
        
        public struct SpawnPointsArrayBlob
        {
            public BlobArray<float3> Value;
        }

        public float3 GetRandomPosition()
        {
            float3 randomPosition;
            randomPosition = _spaceRandomGen.ValueRW.RandomGenValue.NextFloat3(MinCorner,MaxCorner);
            return randomPosition;
            
        }
        
        public quaternion GetRandomRotation()
        {
            return quaternion.RotateZ(_spaceRandomGen.ValueRW.RandomGenValue.NextFloat(360));
        }

         public float SpawnTimerFloat
         {
             get => _spawntimer.ValueRO.TimeValue;
             set => _spawntimer.ValueRW.TimeValue = value;
         }

        public bool TimeToSpawnAsteroid => SpawnTimerFloat <= 0f;
        public float AstroidSpawnRate => _asteroids.ValueRO.AsteroidSpawnRate;
        public Entity AsteroidPrefab => _asteroids.ValueRO.AsteroidPrefab;

        public Entity SpaceShipPrefab => _asteroids.ValueRO.SpaceShipPrefab;
        
        

        public float3 HalfPosition => new()
        {
            x = _asteroids.ValueRO.FieldDimensions.x * 0.5f,
            y = _asteroids.ValueRO.FieldDimensions.y * 0.5f,
            z = 0f
        };
        public float3 MinCorner => Transform.Position - HalfPosition;
        public float3 MaxCorner => Transform.Position + HalfPosition;

    }
}