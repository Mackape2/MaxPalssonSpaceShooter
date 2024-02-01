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
        var kl1 = SystemAPI.GetComponent<Transform>(Entity).position - Transform.Position.x;
        var kl2 = _transform.ValueRO.Position.z - Transform.Position.z;
        var kl3 = math.atan2(kl1, kl2) + math.PI;
        _transform.ValueRW.Position += kl3 * _astroidSpeed.ValueRO.Value * deltaTime;
        Debug.Log(kl3);
    }
    public void Spin()
    {
        //_transform.ValueRW.Rotation *= Quaternion.AngleAxis(0.2f, Vector3.forward);

        
    }
}
