using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClearWarrior.Entities;
using ClearWarrior.Specs.SpecUnit;
using NUnit.Framework;

namespace ClearWarrior.Specs.Inter_Entity_Interactions
{
    [TestFixture]
    public class When_two_entities_try_switch_positions : using_two_entities_in_simple_empty_room
    {
        [Test]
        public void Entity_A_should_not_move()
        {
            TheWorld.GetCoordinatesOf(Enemy, TheRoom).ShouldEqual(new WorldCoordinates(2, 1));
        }

        [Test]
        public void Entity_B_should_not_move()
        {
            TheWorld.GetCoordinatesOf(TheWarrior, TheRoom).ShouldEqual(new WorldCoordinates(2, 2));
        }

        public override void Context()
        {
            TheWarrior.CurrentDirection = AbsoluteDirections.North;
            Enemy.CurrentDirection = AbsoluteDirections.South;

            TheWarrior.RequestToSend(new EntityRequest(ActionRequest.WalkForward));
            Enemy.RequestToSend(new EntityRequest(ActionRequest.WalkForward));
        }

        public override void Because()
        {
            TheWorld.StepForwardOneTimeUnit();
        }
    }

    [TestFixture]
    public class When_an_entity_tries_to_move_to_spot_another_is_moving_from : using_two_entities_in_simple_empty_room
    {
        [Test]
        public void Other_entity_should_be_in_new_spot()
        {
            TheWorld.GetCoordinatesOf(Enemy, TheRoom).ShouldEqual(new WorldCoordinates(1, 1));
        }

        [Test]
        public void It_should_move_into_that_spot()
        {
            TheWorld.GetCoordinatesOf(TheWarrior, TheRoom).ShouldEqual(new WorldCoordinates(2, 1));
        }

        public override void Context()
        {
            TheWarrior.RequestToSend(new EntityRequest(ActionRequest.WalkForward));
            Enemy.RequestToSend(new EntityRequest(ActionRequest.WalkForward));
        }

        public override void Because()
        {
            TheWorld.StepForwardOneTimeUnit();
        }
    }

    [TestFixture]
    public class When_an_entity_tries_to_move_through_another : using_two_entities_in_simple_empty_room
    {
        [Test]
        public void It_should_not_move_any_further_units_in_that_direction()
        {
            TheWorld.GetCoordinatesOf(TheWarrior, TheRoom).ShouldEqual(new WorldCoordinates(2, 2));
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

    public abstract class using_two_entities_in_simple_empty_room
    {
        protected Room TheRoom;
        protected DumbBot TheWarrior;
        protected World TheWorld;
        protected DumbBot Enemy;

        [TestFixtureSetUp]
        public void Setup()
        {
            TheWorld = new World();
            // Create a 3 x 3 room
            // which is really a 4 x 4 because the walls occupy
            // a single square unit a piece.
            var floorPlan = Room.CreateEmptyRoom(5, 5);
            floorPlan[2][2] = new EntryPoint();

            TheRoom = TheWorld.CreateRoom(floorPlan);
            
            TheWarrior = new DumbBot();
            Enemy = new EnemyBot();

            TheWorld.CreateEntity(TheWarrior);
            TheWorld.CreateEntity(Enemy);

            TheWorld.EnterRoom(TheWarrior, TheRoom);
            TheWorld.EnterRoom(Enemy, TheRoom, new WorldCoordinates(2,1));

            Enemy.CurrentDirection = AbsoluteDirections.West;

            Context();
            Because();
        }

        public virtual void Context()
        {

        }

        public abstract void Because();
    }

    public class EnemyBot : DumbBot
    {
        
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
