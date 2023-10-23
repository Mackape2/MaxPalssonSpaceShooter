using Unity.Entities;
using Unity.Mathematics;

namespace AsteroidsNamespace
{
    public struct SpaceRandomGen : IComponentData
    {
        public Random Value;
        
    }
}