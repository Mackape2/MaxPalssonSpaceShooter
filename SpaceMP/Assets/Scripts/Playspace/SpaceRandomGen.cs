using Unity.Entities;
using Unity.Mathematics;

namespace AsteroidsNamespace
{
    [UpdateInGroup(typeof(InitializationSystemGroup))]
    public struct SpaceRandomGen : IComponentData
    {
        //Generates a random value
        public Random RandomGenValue;
    }
}