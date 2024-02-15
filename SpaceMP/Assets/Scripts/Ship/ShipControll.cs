using AsteroidsNamespace;
using Unity.Entities;
using UnityEngine;

namespace Ship
{
    public class ShipControll : MonoBehaviour
    {
        public GameObject BulletPrefab;

    }

    //Attaches the bullets prefab to the ship to be able to instantiate bullets
        public class ShipBaker : Baker<ShipControll>
        {
            
            public override void Bake(ShipControll authoring)
            {
                AddComponent(new BulletPrefabForShip
                {
                    BulletPrefab = GetEntity(authoring.BulletPrefab)
                });
            }
        }
}




