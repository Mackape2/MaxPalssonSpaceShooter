using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace AsteroidsNamespace
{
    
    public readonly partial struct SpaceAspect : IAspect
    {
        private readonly RefRO<Asteroids> _asteroids;
        private readonly RefRW<SpaceRandomGen> _spaceRandomGen;
        private readonly RefRW<LocalTransform> _transform;
        private readonly RefRW<AsteroidSpawnTimer> _spawntimer;
        
        private LocalTransform Transform => _transform.ValueRW;
        

        //Gets a random position based upon a pre-chosen grid.
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

        //Float that keeps track of time until next spawned asteroid
         public float SpawnTimerFloat
         {
             get => _spawntimer.ValueRO.TimeValue;
             set => _spawntimer.ValueRW.TimeValue = value;
         }

         
        public bool TimeToSpawnAsteroid => SpawnTimerFloat <= 0f;
        public float AstroidSpawnRate => _asteroids.ValueRO.AsteroidSpawnRate;
        public Entity AsteroidPrefab => _asteroids.ValueRO.AsteroidPrefab;

        
        //Because the dimensions are based upon the center of the field,
        //the dimensions are halfed so the length is spread in both directions.
        public float3 HalfPosition => new()
        {
            x = _asteroids.ValueRO.FieldDimensions.x * 0.5f,
            y = _asteroids.ValueRO.FieldDimensions.y * 0.5f,
            z = 0f
        };
        
        //Creates two transform to use as coordinates at opposite corners of the square
        public float3 MinCorner => Transform.Position - HalfPosition;
        public float3 MaxCorner => Transform.Position + HalfPosition;

    }
}