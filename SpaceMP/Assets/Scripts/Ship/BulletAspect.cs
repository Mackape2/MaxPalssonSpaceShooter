using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

namespace AsteroidsNamespace
{
    public readonly partial struct BulletAspect : IAspect
    {
        public readonly Entity Entity;
        private readonly RefRW<LocalTransform> _transform;
        private readonly RefRW<BulletSpeed> _bulletSpeed;
        private readonly RefRW<BulletSurviveTimer> _surviveTimer;
        
        //Float for how long it is until the bullet shall be destroyed
        public float BulletSurviveFloat
        {
            get => _surviveTimer.ValueRO.TimeValue;
            set => _surviveTimer.ValueRW.TimeValue = value;
        }
        
        //If this bool is true, the bullet will be destroyed
        public bool BulletDeath => BulletSurviveFloat <= 0f;

        //Moves the bullet forward in it's facing direction 
        public void BulletMove(float deltatime)
        {
            _transform.ValueRW.Position += _transform.ValueRO.Up() * _bulletSpeed.ValueRO.Speed * deltatime;
        }

        //Used to calculate the distance between a bullet and the asteroids
        public float GetHitDistance(LocalTransform positionList)
        {
            float checkedDistance = 0;
            checkedDistance = Vector3.Distance(_transform.ValueRO.Position, positionList.Position);
            return checkedDistance;
        }

        //Returns the specific bullet entity to the code
        public Entity GetBullet()
        {
            return Entity;
        }

        //Decreases the timer that determine if the bullet is destroyed 
        public void DecreaseBulletTimer(float deltatime,EntityCommandBuffer.ParallelWriter ecb,int sortKey)
        {
            BulletSurviveFloat -= deltatime;
            if (BulletDeath)
            {
                //Destroys the bullet
                ecb.DestroyEntity(sortKey,Entity);
            }
        }

    }
    
    public struct BulletPrefabForShip : IComponentData
    {
        public Entity BulletPrefab;
    }

    public struct BulletSpeed : IComponentData
    {
        public float Speed;
    }
    public struct BulletSurviveTimer : IComponentData
    {
        public float TimeValue;
    }
}
