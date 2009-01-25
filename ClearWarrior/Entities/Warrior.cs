using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClearWarrior.Entities
{
    public class Warrior : ISentientEntity
    {
        private World TheWorld;
        public AbsoluteDirections CurrentDirection { get; set; }

        /// <summary>
        /// No one should create a warrior except the world.
        /// </summary>
        /// <param name="theWorld"></param>
        internal Warrior(World theWorld)
        {
            TheWorld = theWorld;
            CurrentDirection = AbsoluteDirections.North;
        }

        public object Look()
        {
            return null;
        }

        public bool FeelsSomething(RelativeDirections relativeDirections)
        {
            return false;
        }

        public void ReceiveResponse(EntityResponse response)
        {
            // Do something with response...
        }

        public EntityRequest GetRequest()
        {
            // Make a request on the world!
            return new EntityRequest(ActionRequest.None);
        }
    }
}
