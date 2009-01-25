using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using ClearWarrior.Specs.SpecUnit;
using ClearWarrior.Entities;

namespace ClearWarrior.Specs.Warrior_Movements
{
    [TestFixture]
    public class When_warrior_is_at_an_exit_point_and_chooses_to_exit : using_simple_empty_room
    {
        [Test]
        public void It_should_exit_the_room()
        {
            TheWorld.GetCoordinatesOf(TheWarrior, TheRoom).ShouldBeNull();
        }

        public override void Context()
        {
            TheWorld.MoveForward(TheWarrior, TheRoom);
            TheWorld.Feel(RelativeDirections.InFront, TheWarrior, TheRoom).ShouldBe(typeof(Wall));
        }

        public override void Because()
        {
            TheWorld.ExitRoom(TheWarrior, TheRoom);
        }
    }

    [TestFixture]
    public class When_warrior_faces_a_direction_and_walks : using_simple_empty_room
    {
        [Test]
        public void It_should_move_in_that_direction()
        {
            TheWorld.GetCoordinatesOf(TheWarrior, TheRoom).ShouldEqual(new WorldCoordinates(1, 2));
        }

        WorldCoordinates PreviousCoordinates;
        public override void Context()
        {
            PreviousCoordinates = TheWorld.GetCoordinatesOf(TheWarrior, TheRoom);
            PreviousCoordinates.ShouldEqual(new WorldCoordinates(2, 2));
        }

        public override void Because()
        {
            TheWorld.Turn(TheWarrior, RelativeDirections.ToTheLeftSide);
            TheWorld.MoveForward(TheWarrior, TheRoom);
        }
    }

    [TestFixture]
    public class When_warrior_tries_to_move_forward_while_facing_wall : using_simple_empty_room
    {
        [Test]
        public void It_should_not_move()
        {
            TheWorld.GetCoordinatesOf(TheWarrior, TheRoom).ShouldEqual(PreviousCoordinates);
        }

        [Test]
        public void It_should_feel_obstruction_in_front()
        {
            TheWorld.Feel(RelativeDirections.InFront, TheWarrior, TheRoom).ShouldBe(typeof(Wall));
        }

        WorldCoordinates PreviousCoordinates;
        public override void Context()
        {
            TheWorld.MoveForward(TheWarrior, TheRoom);
            PreviousCoordinates = TheWorld.GetCoordinatesOf(TheWarrior, TheRoom);
        }
        public override void Because()
        {
            TheWorld.MoveForward(TheWarrior, TheRoom);
        }
    }

    [TestFixture]
    public class When_warrior_is_facing_wall : using_simple_empty_room
    {
        [Test]
        public void It_should_feel_obstruction_in_front()
        {
            TheWorld.Feel(RelativeDirections.InFront, TheWarrior, TheRoom).ShouldBe(typeof(Wall));
        }

        public override void Because()
        {
            TheWorld.MoveForward(TheWarrior, TheRoom);
        }
    }

    [TestFixture]
    public class When_warrior_enters_room_and_looks : using_simple_empty_room
    {
        [Test]
        public void It_should_not_be_able_to_see_anything()
        {
            WhatsSeen.ShouldBeNull();
        }

        [Test]
        public void It_should_be_facing_north()
        {
            TheWorld.FindDirectionFacing(TheWarrior).ShouldEqual(AbsoluteDirections.North);
        }

        public object WhatsSeen;
        public override void Because()
        {
            WhatsSeen = TheWarrior.Look();
        }
    }

    public abstract class using_simple_empty_room
    {
        protected Room TheRoom;
        protected Warrior TheWarrior;
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
            TheWarrior = TheWorld.CreateWarrior();
            TheWorld.EnterRoom(TheWarrior, TheRoom);

            Context();
            Because();
        }

        public virtual void Context()
        {

        }

        public abstract void Because();
    }

}
