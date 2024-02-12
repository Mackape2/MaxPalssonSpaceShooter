using AsteroidsNamespace;
using Unity.Burst;
using Unity.Entities;
using UnityEngine;

public partial struct AstroidSystem : ISystem
{
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<Asteroids>();
    }
        
    [BurstCompile]
    public void OnDestroy(ref SystemState state)
    {
             
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var deltaTime = SystemAPI.Time.DeltaTime;
        new MoveAstroidJob()
        {
            DeltaTime = deltaTime
        }.Schedule();
    }
}
[BurstCompile]
public partial struct MoveAstroidJob : IJobEntity
{
    public float DeltaTime;
    [BurstCompile]
    private void Execute(AstroidMovementAspect astroidMovementAspect)
    {
        astroidMovementAspect.Move(DeltaTime);
    }
}
