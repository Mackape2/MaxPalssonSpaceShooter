using System;
using AsteroidsNamespace;
using System.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;


public readonly partial struct AstroidMovementAspect : IAspect
{
    public readonly Entity Entity;
    
    private readonly RefRW<LocalTransform> _transform;
    private readonly RefRO<AstroidSpeed> _astroidSpeed;
    private LocalTransform Transform => _transform.ValueRO;

    public void Move(float deltaTime)
    {
        _transform.ValueRW.Position += _transform.ValueRO.Up() * _astroidSpeed.ValueRO.Value * deltaTime;
    }
   
}
