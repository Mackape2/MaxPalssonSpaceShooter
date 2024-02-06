using AsteroidsNamespace;
using Unity.Burst;
using Unity.Entities;
using Random = Unity.Mathematics.Random;

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
        new SpawnAstroidJob()
        {
            DeltaTime = deltaTime
        }.Schedule();
    }
}

[BurstCompile]
public partial struct SpawnAstroidJob : IJobEntity
{

    public float DeltaTime;
    [BurstCompile]
    private void Execute(AstroidMovementAspect astroidMovementAspect)
    {
        Random randomDirection;

        randomDirection = new Random(1);
        int spinDirection = randomDirection.NextInt(0, 1);
        astroidMovementAspect.Move(DeltaTime);
    }
}
