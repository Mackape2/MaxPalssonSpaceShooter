using System;
using Unity.Entities;
using UnityEngine;
using Unity.Mathematics;
using Random = Unity.Mathematics.Random;

namespace AsteroidsNamespace
{
    public class SpaceBehaviour : MonoBehaviour
    {
        public float2 FieldDimensions;
        public uint RandomSeed;
        public GameObject AstroidPrefab;
        public float AstroidSpawnRate;
        public Entity SpaceShipPrefab;
        
        private void OnDrawGizmos()
        {
            Gizmos.DrawWireCube(transform.position, new Vector3(FieldDimensions.x,FieldDimensions.y,0));
        }
    }
    
    public class SpaceBaker : Baker<SpaceBehaviour>
    {
        public override void Bake(SpaceBehaviour authoring)
        {
            
            AddComponent(new Asteroids
            {
                
                FieldDimensions = authoring.FieldDimensions,
                AsteroidPrefab = GetEntity(authoring.AstroidPrefab),
                AsteroidSpawnRate = authoring.AstroidSpawnRate,
                SpaceShipPrefab = authoring.SpaceShipPrefab
            });
            AddComponent<AsteroidSpawnTimer>();
            AddComponent(new SpaceRandomGen
            {
                RandomGenValue = new Random(authoring.RandomSeed)
            });

        }
    }
}