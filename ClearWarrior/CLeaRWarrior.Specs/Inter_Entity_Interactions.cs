using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClearWarrior.Entities;
using NUnit.Framework;

namespace ClearWarrior.Specs.Inter_Entity_Interactions
{

    [TestFixture]
    public class When_an_entity_tries_to_move_through_another : using_two_entities_in_simple_empty_room
    {
        [Test]
        public void It_should_not_move_any_further_units_in_that_direction()
        {
            
        }

        public override void Because()
        {
            throw new System.NotImplementedException();
        }
    }

    public abstract class using_two_entities_in_simple_empty_room
    {
        protected Room TheRoom;
        protected DumbBot TheWarrior;
        protected World TheWorld;

        [TestFixtureSetUp]
        public void Setup()
        {
            TheWorld = new World();

            // Create a 3 x 3 room
            // which is really a 4 x 4 because the walls occupy
            // a single square unit a piece.
            var floorPlan = Room.CreateEmptyRoom(5, 5);
            floorPlan[2][2] = new EntryPoint();
            floorPlan[1][2] = new RoomUnit(new DumbBot());

            TheRoom = TheWorld.CreateRoom(floorPlan);
            
            TheWarrior = new DumbBot();
            TheWorld.CreateEntity(TheWarrior);
            TheWorld.EnterRoom(TheWarrior, TheRoom);

            Context();
            Because();
        }

        public virtual void Context()
        {

        }

        public abstract void Because();
    }

    public class DumbBot : ISentientEntity
    {
        public bool WasAskedForRequest;
        public bool WasGivenResponse;
        private EntityRequest _RequestToSend;

        #region ISentientEntity Members

        public AbsoluteDirections CurrentDirection
        {
            get;
            set;
        }

        public bool FeelsSomething(RelativeDirections relativeDirections)
        {
            throw new NotImplementedException();
        }

        public object Look()
        {
            throw new NotImplementedException();
        }

        #endregion

        public void ReceiveResponse(EntityResponse response)
        {
            WasGivenResponse = true;
        }


        public EntityRequest GetRequest()
        {
            WasAskedForRequest = true;
            return _RequestToSend;
        }

        public void RequestToSend(EntityRequest request)
        {
            _RequestToSend = request;
        }
    }
}
