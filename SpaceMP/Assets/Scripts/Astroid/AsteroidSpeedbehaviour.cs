using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class AsteroidSpeedbehaviour : MonoBehaviour
{
    public float AstroidSpeed;

}

//Attaches component responsible for the asteroids speed to the asteroid
public class AstroidBaker : Baker<AsteroidSpeedbehaviour>
{
    public override void Bake(AsteroidSpeedbehaviour authoring)
    {
        AddComponent(new AstroidSpeed {Value = authoring.AstroidSpeed} );
    }
}
