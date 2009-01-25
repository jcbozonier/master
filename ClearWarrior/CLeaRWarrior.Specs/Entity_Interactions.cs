using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using ClearWarrior.Specs.SpecUnit;
using ClearWarrior.Entities;

namespace ClearWarrior.Specs.World_Interactions
{
    [TestFixture]
    public class When_world_with_turning_entity_steps_forward_in_time : using_single_entity_in_simple_empty_room
    {
        [Test]
        public void It_should_face_the_correct_direction()
        {
            TheWorld.FindDirectionFacing(TheWarrior).ShouldEqual(AbsoluteDirections.West);
        }

        public override void Context()
        {
            TheWorld.FindDirectionFacing(TheWarrior).ShouldEqual(AbsoluteDirections.North);
            TheWarrior.RequestToSend(new EntityRequest(ActionRequest.TurnLeft));
        }

        public override void Because()
        {
            TheWorld.StepForwardOneTimeUnit();
        }
    }

    [TestFixture]
    public class When_world_with_moving_entity_steps_forward_in_time : using_single_entity_in_simple_empty_room
    {

        [Test]
        public void It_should_move_in_direction_its_facing()
        {
            TheWorld.GetCoordinatesOf(TheWarrior, TheRoom).ShouldEqual(new WorldCoordinates(2,1));
        }

        public override void Context()
        {
            TheWarrior.RequestToSend(new EntityRequest(ActionRequest.WalkForward));
        }

        public override void Because()
        {
            TheWorld.StepForwardOneTimeUnit();
        }
    }

    [TestFixture]
    public class When_world_with_entity_steps_forward_in_time : using_single_entity_in_simple_empty_room
    {
        [Test]
        public void It_should_ask_for_requests_from_all_entities()
        {
            TheWarrior.WasAskedForRequest.ShouldBeTrue();
        }

        [Test]
        public void It_should_provide_a_response_to_all_entities()
        {
            TheWarrior.WasGivenResponse.ShouldBeTrue();
        }

        public override void Context()
        {
            TheWarrior.RequestToSend(new EntityRequest(ActionRequest.None));
        }

        public override void Because()
        {
            TheWarrior.WasAskedForRequest.ShouldBeFalse();
            TheWarrior.WasGivenResponse.ShouldBeFalse();

            TheWorld.StepForwardOneTimeUnit();
        }
    }

    public abstract class using_single_entity_in_simple_empty_room
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
            floorPlan[1][2] = new ExitPoint();

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
            get; set;
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
