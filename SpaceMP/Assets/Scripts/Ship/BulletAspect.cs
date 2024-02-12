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
        private readonly RefRW<BulletSpeed> _bulletSpeed;

        public LocalTransform GetPosition()
        {
            var transform = _transform.ValueRO;
            return transform;
        }

        
        public void BulletMove(float deltatime)
        {
            Debug.Log("hi");
            _transform.ValueRW.Position += _transform.ValueRO.Up() * _bulletSpeed.ValueRO.Speed * deltatime;
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
}
