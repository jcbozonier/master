using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClearWarrior.Entities
{
    public class RoomUnit
    {
        public IEntity Entity;

        public RoomUnit()
        {

        }

        public RoomUnit(IEntity entity)
        {
            Entity = entity;
        }
    }

    public class EntryPoint : RoomUnit
    {

    }

    public class ExitPoint : RoomUnit
    {

    }
}
