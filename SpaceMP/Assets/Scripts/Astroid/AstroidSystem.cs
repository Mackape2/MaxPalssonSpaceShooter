using AsteroidsNamespace;
using Unity.Burst;
using Unity.Entities;
using UnityEngine;

public partial struct AstroidSystem : ISystem
{
    //Script that controls asteroid movement
    
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
        //Calls the movement job and schedules it
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
    //Calls the move function from the asteroid aspect
    private void Execute(AstroidMovementAspect astroidMovementAspect)
    {
        astroidMovementAspect.Move(DeltaTime);
    }
}
