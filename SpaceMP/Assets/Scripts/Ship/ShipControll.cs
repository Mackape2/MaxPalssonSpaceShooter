using AsteroidsNamespace;
using Unity.Entities;
using UnityEngine;

namespace Ship
{
    public class ShipControll : MonoBehaviour
    {
        public GameObject BulletPrefab;

    }

        public class ShipBaker : Baker<ShipControll>
        {
            
            public override void Bake(ShipControll authoring)
            {
                AddComponent(new Bullet
                {
                    BulletPrefab = GetEntity(authoring.BulletPrefab)
                });
            }
        }
}




