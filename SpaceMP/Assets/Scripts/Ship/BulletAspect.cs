using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

namespace AsteroidsNamespace
{
    public readonly partial struct BulletAspect : IAspect
    {
        private readonly RefRW<LocalTransform> _transform;
        private readonly RefRO<Bullet>_bullet;
        private readonly RefRW<BulletPosition> _bulletPosition;

        public Entity BulletPrefab => _bullet.ValueRO.BulletPrefab; 

        public LocalTransform GetPosition()
        {
            var transform = _transform.ValueRO;
            return transform;
        }

       
        public LocalTransform BulletPoition
        {
            get => _bulletPosition.ValueRO.Position;
            set => _bulletPosition.ValueRW.Position = value;
        }

        public void BulletMove(float deltatime)
        {
            _transform.ValueRW.Position = _transform.ValueRO.Up() * 5 * deltatime;
        }
    }
    
    
    public struct Bullet : IComponentData
    {
        public Entity BulletPrefab;
    }
    
    public struct BulletPosition : IComponentData
    {
        public LocalTransform Position;
    }
}
