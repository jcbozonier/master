using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClearWarrior.Entities
{
    

    /// <summary>
    /// All rooms have an entry point. For now I assume they must
    /// also have an exit point.
    /// </summary>
    public class Room
    {
        /// <summary>
        /// The outer list is height units
        /// the inner is width units.
        /// </summary>
        private List<List<RoomUnit>> _FloorPlan;
        public int Width
        {
            get
            {
                return _FloorPlan[0].Count;
            }
        }

        public int Height
        {
            get
            {
                return _FloorPlan.Count;
            }
        }

        internal Room()
        {

        }

        internal Room(List<List<RoomUnit>> floorPlan)
        {
            _FloorPlan = floorPlan;
        }

        internal IEntity GetEntityAtCoordinate(int widthIndex, int heightIndex)
        {
            return _FloorPlan[heightIndex][widthIndex].Entity;
        }

        internal void PlaceAtEntryPoint(ISentientEntity TheWarrior)
        {
            foreach (var heightUnits in _FloorPlan)
            {
                foreach (var roomUnit in heightUnits)
                {
                    if (roomUnit is EntryPoint)
                    {
                        roomUnit.Entity = TheWarrior;
                        return;
                    }
                }
            }

            throw new GeneralGameException("No entry point could be found. Every room must have an entry point.");
        }

        internal IEntity GetEntityAtCoordinate(WorldCoordinates coordinateToCheck)
        {
            return GetEntityAtCoordinate(coordinateToCheck.Latitude, coordinateToCheck.Longitude);
        }

        /// <summary>
        /// Creates an empty room of the specified dimensions.
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static List<List<RoomUnit>> CreateEmptyRoom(int width, int height)
        {
            var unitRows = new List<List<RoomUnit>>();

            for (var row = 0; row < height; row++)
            {
                var rowUnits = new List<RoomUnit>();
                for (var col = 0; col < width; col++)
                {
                    if (col == 0 ||
                        row == 0)
                    {
                        rowUnits.Add(new RoomUnit(new Wall()));
                        continue;
                    }

                    if ((width - 1) == col ||
                        (height - 1) == row)
                    {
                        rowUnits.Add(new RoomUnit(new Wall()));
                        continue;
                    }

                    rowUnits.Add(new RoomUnit());
                }

                unitRows.Add(rowUnits);
            }

            return unitRows;
        }

        internal WorldCoordinates GetCoordinatesOf(IEntity entity)
        {
            for (var row = 0; row < _FloorPlan.Count; row++)
            {
                for (var col = 0; col < _FloorPlan[0].Count; col++)
                {
                    if (_FloorPlan[row][col].Entity == entity ||
                        _FloorPlan[row][col].PreviousEntity == entity)
                    {
                        return new WorldCoordinates(col, row);
                    }
                }
            }

            return null;
        }

        internal void MoveEntity(WorldCoordinates oldCoordinates, WorldCoordinates newCoordinates)
        {
            IEntity entityToMove;

            if (_FloorPlan[oldCoordinates.Longitude][oldCoordinates.Latitude].PreviousEntity != null)
            {
                entityToMove = _FloorPlan[oldCoordinates.Longitude][oldCoordinates.Latitude].PreviousEntity;
                _FloorPlan[oldCoordinates.Longitude][oldCoordinates.Latitude].PreviousEntity = null;
            }
            else
            {
                entityToMove = _FloorPlan[oldCoordinates.Longitude][oldCoordinates.Latitude].Entity;
                _FloorPlan[oldCoordinates.Longitude][oldCoordinates.Latitude].Entity = null;
            }

            var currentEntityInNewUnit = _FloorPlan[newCoordinates.Longitude][newCoordinates.Latitude].Entity;
            _FloorPlan[newCoordinates.Longitude][newCoordinates.Latitude].PreviousEntity = currentEntityInNewUnit;
            _FloorPlan[newCoordinates.Longitude][newCoordinates.Latitude].Entity = entityToMove;
        }

        internal RoomUnit GetUnitAt(WorldCoordinates currentCoordinates)
        {
            return _FloorPlan[currentCoordinates.Longitude][currentCoordinates.Latitude];
        }

        /// <summary>
        /// Removes entity from the object it is currently occupying.
        /// </summary>
        /// <param name="TheWarrior"></param>
        internal void Remove(Warrior TheWarrior)
        {
            var coordinates = GetCoordinatesOf(TheWarrior);
            var roomUnit = GetUnitAt(coordinates);

            roomUnit.Entity = null;
        }

        public void Place(ISentientEntity entity, WorldCoordinates coordinates)
        {
            _FloorPlan[coordinates.Longitude][coordinates.Latitude].Entity = entity;
        }
    }
}
