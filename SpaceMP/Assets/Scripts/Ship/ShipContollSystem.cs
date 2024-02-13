using System.Collections;
using System.Collections.Generic;
using AsteroidsNamespace;
using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

public partial struct ShipContollSystem : ISystem
{
    

    [BurstCompile]
    // Update is called once per frame
    void OnUpdate(ref SystemState state)
    {
        EntityCommandBuffer ecb = SystemAPI.GetSingleton<BeginInitializationEntityCommandBufferSystem.Singleton>()
            .CreateCommandBuffer(state.WorldUnmanaged);

        foreach (var VARIABLE in SystemAPI.Query<LocalTransform>().WithAll<Bullet>().WithEntityAccess())
        {
            LocalTransform transform = VARIABLE.Item1;
            if (Input.GetKey(KeyCode.W))
            {

                transform.Position += transform.Up() * (7 * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.A))
            {
                //float roatation = 2 * Time.deltaTime;
                transform.Rotation *= Quaternion.AngleAxis(1, Vector3.forward);
            }

            if (Input.GetKey(KeyCode.D))
            {
                
                transform.Rotation *= Quaternion.AngleAxis(-1, Vector3.forward);
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                
                Entity bullet = ecb.Instantiate(SystemAPI.GetComponentRO<Bullet>(VARIABLE.Item2).ValueRO.BulletPrefab);
                ecb.SetComponent(bullet, new LocalTransform
                {
                    Position = transform.Position,
                    Scale = 1,
                    Rotation = transform.Rotation
                });

            }
            transform.Scale = 1;
            ecb.SetComponent(VARIABLE.Item2, transform);
        }
    }
}
