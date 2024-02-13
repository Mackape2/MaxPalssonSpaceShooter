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
        
        public float BulletSurviveFloat
        {
            get => _surviveTimer.ValueRO.TimeValue;
            set => _surviveTimer.ValueRW.TimeValue = value;
        }

        public float BulletTime
        {
            get => 2;
        }
        
        public bool BulletDeath => BulletSurviveFloat <= 0f;

        public void BulletMove(float deltatime)
        {
            _transform.ValueRW.Position += _transform.ValueRO.Up() * _bulletSpeed.ValueRO.Speed * deltatime;
        }

        public float GetHitDistance(LocalTransform positionList,float deltatime )
        {
            BulletSurviveFloat -= deltatime;
            if (BulletDeath)
            {
                BulletSurviveFloat = _surviveTimer.ValueRO.TimeValue;
            }
            float shortestDistance = 10;
            var checkedDistance = shortestDistance + 1;

            checkedDistance = Vector3.Distance(_transform.ValueRO.Position, positionList.Position);
            return checkedDistance;
        }

        public Entity GetBullet()
        {
            return Entity;
        }

    }
    
    public struct Bullet : IComponentData
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
