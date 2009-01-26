using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClearWarrior.Entities
{
    /// <summary>
    /// All interactions between the entities should be handled
    /// through the World object. It is the final judge of
    /// every outcome.
    /// </summary>
    public class World
    {
        private List<IEntity> _Entities;
        private Room _TheRoom;

        public Room CreateRoom(List<List<RoomUnit>> floorPlan)
        {
            _TheRoom = new Room(floorPlan);
            return _TheRoom;
        }

        public Warrior CreateWarrior()
        {
            var warrior = new Warrior(this);
            _AddToEntities(warrior);
            return warrior;
        }

        private void _AddToEntities(IEntity entity)
        {
            if(_Entities == null) _Entities = new List<IEntity>();
            _Entities.Add(entity);
        }

        public void CreateEntity(IEntity entity)
        {
            _AddToEntities(entity);
        }

        public void EnterRoom(ISentientEntity entity, Room TheRoom)
        {
            entity.CurrentDirection = AbsoluteDirections.North;
            TheRoom.PlaceAtEntryPoint(entity);
        }

        public void EnterRoom(ISentientEntity entity, Room room, WorldCoordinates coordinates)
        {
            entity.CurrentDirection = AbsoluteDirections.North;
            room.Place(entity, coordinates);
        }

        public void MoveForward(ISentientEntity TheWarrior, Room TheRoom)
        {
            var oldCoordinates = TheRoom.GetCoordinatesOf(TheWarrior);
            var newCoordinates = oldCoordinates.Compute(TheWarrior.CurrentDirection, 1);
            if(!(TheRoom.GetEntityAtCoordinate(newCoordinates) is Wall))
                TheRoom.MoveEntity(oldCoordinates, newCoordinates);
        }

        public AbsoluteDirections FindDirectionFacing(ISentientEntity entity)
        {
            return entity.CurrentDirection;
        }

        public IEntity Feel(RelativeDirections relativeDirection, Warrior TheWarrior, Room TheRoom)
        {
            var location = _GetCurrentLocation(TheWarrior, TheRoom);
            var direction = _ComputeAbsoluteDirection(TheWarrior, relativeDirection);
            var entity = _GetEntityAt(TheRoom, location, direction, 1);
            return entity;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="location"></param>
        /// <param name="direction"></param>
        /// <param name="unitDistance"></param>
        /// <returns></returns>
        private IEntity _GetEntityAt(Room theRoom, WorldCoordinates location, AbsoluteDirections direction, int unitDistance)
        {
            var coordinateToCheck = (WorldCoordinates)location.Compute(direction, unitDistance);
            return theRoom.GetEntityAtCoordinate(coordinateToCheck);
        }

        /// <summary>
        /// Can compute the direction you get by starting at an absolute direction (ie. North)
        /// using a relative direction (ie. left) and can compute the absolute direction
        /// that results (ie. West)
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="relativeDirection"></param>
        /// <returns></returns>
        private AbsoluteDirections _ComputeAbsoluteDirection(ISentientEntity entity, RelativeDirections relativeDirection)
        {
            var warriorDirection = FindDirectionFacing(entity);
            switch (warriorDirection)
            {
                case AbsoluteDirections.North:
                    switch (relativeDirection)
                    {
                        case RelativeDirections.InFront:
                            return AbsoluteDirections.North;
                        case RelativeDirections.ToTheLeftSide:
                            return AbsoluteDirections.West;
                        case RelativeDirections.Behind:
                            return AbsoluteDirections.South;
                        case RelativeDirections.ToTheRightSide:
                            return AbsoluteDirections.East;
                    }
                    break;
                case AbsoluteDirections.West:
                    switch (relativeDirection)
                    {
                        case RelativeDirections.InFront:
                            return AbsoluteDirections.West;
                        case RelativeDirections.ToTheLeftSide:
                            return AbsoluteDirections.South;
                        case RelativeDirections.Behind:
                            return AbsoluteDirections.East;
                        case RelativeDirections.ToTheRightSide:
                            return AbsoluteDirections.North;
                    }
                    break;
                case AbsoluteDirections.South:
                    switch (relativeDirection)
                    {
                        case RelativeDirections.InFront:
                            return AbsoluteDirections.South;
                        case RelativeDirections.ToTheLeftSide:
                            return AbsoluteDirections.East;
                        case RelativeDirections.Behind:
                            return AbsoluteDirections.North;
                        case RelativeDirections.ToTheRightSide:
                            return AbsoluteDirections.West;
                    }
                    break;
                case AbsoluteDirections.East:
                    switch (relativeDirection)
                    {
                        case RelativeDirections.InFront:
                            return AbsoluteDirections.East;
                        case RelativeDirections.ToTheLeftSide:
                            return AbsoluteDirections.North;
                        case RelativeDirections.Behind:
                            return AbsoluteDirections.West;
                        case RelativeDirections.ToTheRightSide:
                            return AbsoluteDirections.South;
                    }
                    break;
            }

            throw new GeneralGameException("The direction could not be computed. This makes no sense at all.");
        }

        private WorldCoordinates _GetCurrentLocation(Warrior TheWarrior, Room TheRoom)
        {
            WorldCoordinates warriorsLocation;

            var width = TheRoom.Width;
            var height = TheRoom.Height;

            for (var widthIndex = 0; widthIndex < width; widthIndex++)
            {
                for (var heightIndex = 0; heightIndex < height; heightIndex++)
                {
                    if (TheRoom.GetEntityAtCoordinate(widthIndex, heightIndex) == TheWarrior)
                    {
                        warriorsLocation = new WorldCoordinates(widthIndex, heightIndex);
                        return warriorsLocation;
                    }
                }
            }

            throw new EntityNotFoundException();
        }

        public WorldCoordinates GetCoordinatesOf(IEntity theWarrior, Room theRoom)
        {
            return theRoom.GetCoordinatesOf(theWarrior);
        }

        public void Turn(ISentientEntity TheWarrior, RelativeDirections relativeDirections)
        {
            TheWarrior.CurrentDirection = _ComputeAbsoluteDirection(TheWarrior, relativeDirections);
        }

        public void ExitRoom(Warrior TheWarrior, Room TheRoom)
        {
            var currentCoordinates = TheRoom.GetCoordinatesOf(TheWarrior);
            var currentUnit = TheRoom.GetUnitAt(currentCoordinates);
            if (currentUnit is ExitPoint)
            {
                TheRoom.Remove(TheWarrior);
            }
        }

        /// <summary>
        /// Steps the world forward in time by a given unit of time.
        /// In other words 1 step forward means every entity gets one turn
        /// to act in the game world.
        /// </summary>
        public void StepForwardOneTimeUnit()
        {
            // Get requests from entities
            var worldRequests = _GetRequestsFor(_Entities);

            // Validate requests by removing invalid requests
            var validRequests = _Validate(worldRequests);

            var responses = _Process(validRequests);

            // Send the responses to the validated entity requests
            _Send(responses);
        }

        private List<WorldResponse> _Process(List<WorldRequest> requests)
        {
            var responses = new List<WorldResponse>();

            foreach (var request in requests)
            {
                var response = _Process(request);
                responses.Add(response);
            }

            return responses;
        }

        private WorldResponse _Process(WorldRequest request)
        {
            switch (request.Request)
            {
                case ActionRequest.WalkForward:
                    // We should have a guarantee at this point that this cast
                    // is a valid one.
                    MoveForward(request.Entity as ISentientEntity, _TheRoom);
                    break;
                case ActionRequest.TurnLeft:
                    Turn(request.Entity as ISentientEntity, RelativeDirections.ToTheLeftSide);
                    break;
                default:
                    new GeneralGameException("This request can not be processed.");
                    break;
            }
            return new WorldResponse(request.Entity, request.TheRoom);
        }

        private void _Send(List<WorldResponse> responses)
        {
            foreach(var response in responses)
            {
                _Send(response);
            }
        }

        private void _Send(WorldResponse response)
        {
            var entity = (IEntity)response.Entity;
            var entityResponse = new EntityResponse(response);
            entity.ReceiveResponse(entityResponse);
        }


        private List<WorldRequest> _Validate(List<WorldRequest> requests)
        {
            var validRequests = new List<WorldRequest>();
            foreach(var request in requests)
            {
                if(_IsValid(request, requests)) 
                    validRequests.Add(request);
            }

            return validRequests;
        }

        private bool _IsValid(WorldRequest request, List<WorldRequest> proposedRequests)
        {
            switch(request.Request)
            {
                case ActionRequest.WalkForward:
                    var entity = request.Entity as ISentientEntity;
                    var entityLocation = GetCoordinatesOf(entity, _TheRoom);
                    var proposedUnit = _GetEntityAt(_TheRoom, entityLocation, entity.CurrentDirection, 1);
                    
                    // If no entity in unit then we're set!
                    if(proposedUnit == null) return true;
                    // If there's an entity in the unit but it's going to
                    // move then we're set!
                    if(proposedUnit != null && _WillMove(proposedUnit, proposedRequests))
                    {
                        return true;
                    }

                    return (proposedUnit == null);
                default:
                    return true;
            }
        }

        private bool _WillMove(IEntity unit, List<WorldRequest> proposedRequests)
        {
            WorldRequest unitRequest = null;

            foreach(var request in proposedRequests)
            {
                if(request.Entity == unit)
                {
                    unitRequest = request;
                    break;
                }
            }

            if(unitRequest == null) return false;

            if(unitRequest.Request == ActionRequest.WalkForward)
            {
                return true;
            }

            return false;
        }

        private List<WorldRequest> _GetRequestsFor(List<IEntity> entities)
        {
            var worldRequests = new List<WorldRequest>();
            foreach(var entity in _Entities)
            {
                var entityRequest = entity.GetRequest();
                worldRequests.Add(new WorldRequest(entity, entityRequest));
            }

            return worldRequests;
        }

        
    }

    public class EntityRequest
    {
        public ActionRequest Request;

        public EntityRequest(ActionRequest request)
        {
            Request = request;
        }
    }

    public enum ActionRequest
    {
        None,
        WalkForward,
        WalkLeft,
        WalkRight,
        WalkBackwards,
        TurnLeft,
        TurnRight,
        TurnAround
    }

    public class EntityResponse
    {
        private WorldResponse _Response;

        public EntityResponse(WorldResponse response)
        {
            _Response = response;
        }
    }

    public class WorldResponse
    {
        public IEntity Entity;
        public Room TheRoom;

        public WorldResponse(IEntity entity, Room theRoom)
        {
            Entity = entity;
            TheRoom = theRoom;
        }
    }

    public class WorldRequest
    {
        public IEntity Entity;
        public ActionRequest Request;
        public Room TheRoom;

        public WorldRequest(IEntity entity, EntityRequest request)
        {
            Entity = entity;
            Request = (request == null)
                          ? ActionRequest.None
                          : request.Request;
        }
    }

    internal class GeneralGameException : Exception
    {
        public GeneralGameException(string message)
            : base(message)
        {
        }
    }

    internal class EntityNotFoundException : Exception
    {

    }

    public class WorldCoordinates
    {
        int _Latitude;

        public int Latitude
        {
            get { return _Latitude; }
            set { _Latitude = value; }
        }
        int _Longitude;

        public int Longitude
        {
            get { return _Longitude; }
            set { _Longitude = value; }
        }

        public WorldCoordinates(int latitude, int longitude)
        {
            _Latitude = latitude;
            _Longitude = longitude;
        }

        internal WorldCoordinates Compute(AbsoluteDirections direction, int unitDistance)
        {
            switch (direction)
            {
                case AbsoluteDirections.North:
                    return new WorldCoordinates(_Latitude, _Longitude - unitDistance);
                case AbsoluteDirections.South:
                    return new WorldCoordinates(_Latitude, _Longitude + unitDistance);
                case AbsoluteDirections.West:
                    return new WorldCoordinates(_Latitude - unitDistance, _Longitude);
                case AbsoluteDirections.East:
                    return new WorldCoordinates(_Latitude + unitDistance, _Longitude);
            }

            throw new NotImplementedException();
        }

        public override bool Equals(object obj)
        {
            var coordinatesToCompare = obj as WorldCoordinates;
            if (coordinatesToCompare == null) return false;

            return coordinatesToCompare.Latitude == this.Latitude &&
                coordinatesToCompare.Longitude == this.Longitude;
        }

        public override string ToString()
        {
            return "Coordinates{x=" + _Latitude + ";y=" + _Longitude + "}";
        }
    }
}
