using System;
namespace ClearWarrior.Entities
{
    public interface ISentientEntity : IEntity
    {
        AbsoluteDirections CurrentDirection { get; set; }
        bool FeelsSomething(RelativeDirections relativeDirections);
        object Look();
    }
}
