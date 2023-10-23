using Unity.Entities;
using UnityEngine;
using Unity.Mathematics;
using Random = Unity.Mathematics.Random;

namespace AsteroidsNamespace
{
    public class SpaceBehaviour : MonoBehaviour
    {
        public float2 FieldDimensions;
        public int NumberOfSpawnpoints;
        public GameObject SpawnpointPrefab;
        public uint RandomSeed;
    }
    
    public class SpaceBaker : Baker<SpaceBehaviour>
    {
        public override void Bake(SpaceBehaviour authoring)
        {
            AddComponent(new Asteroids
            {
                FieldDimensions = authoring.FieldDimensions,
                NumberOfSpawnpoints = authoring.NumberOfSpawnpoints,
                SpawnpointPrefab = GetEntity(authoring.SpawnpointPrefab)

            });
            AddComponent(new SpaceRandomGen
            {
                Value = Random.CreateFromIndex(authoring.RandomSeed)
            });
            
        }
    }
}