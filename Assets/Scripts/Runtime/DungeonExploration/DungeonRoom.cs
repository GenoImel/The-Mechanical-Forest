using System;

namespace Akashic.Runtime.DungeonExploration
{
    internal sealed class DungeonRoom
    {
        private int roomId; 
        private DoublyLinkedList[] hallways;
        private RoomState roomState;  
        private bool endRoom = false;

        private enum RoomState
        {
            Unexplored = 0,
            UnexploredEnemy = 1,
            UnexploredEvent = 2,
            UnexploredEmpty = 3,
            RestStopActive = 4,
            RestStopInactive = 5,
            Explored = -1
        }

        /// <summary>
        /// Create dungeon room.
        /// </summary>
        public DungeonRoom(DoublyLinkedList[] hallway, int id, int state = 0, bool end = false)
        {
            hallways = hallway;
            roomId = id;
            roomState = (RoomState)state;
            endRoom = end;
        }

        /// <summary>
        /// Create dungeon room with all the properties bundled.
        /// Currently not implemented.
        /// </summary>
        public DungeonRoom(string JSONfileOrSomething)
        {
            // Json file will contain all the information about the current and future dungeon rooms
        }

        /// <summary>
        /// Modify the hallway list.
        /// </summary>
        public void ModifyHallways(DoublyLinkedList[] hallway)
        {
            hallways = hallway;
        }

        /// <summary>
        /// Get the unique id of the room.
        /// </summary>
        public int GetId()
        {
            return roomId;
        }

        /// <summary>
        /// Get the state of the room.
        /// </summary>
        public int GetState()
        {
            return (int)roomState;
        }

        /// <summary>
        /// Get all the relevant connections for traversal.
        /// </summary>
        public DoublyLinkedList[] GetConnections()
        {
            return hallways;
        }

        /// <summary>
        /// Return the index where a connection exists, -1 if it doesn't.
        /// Hallway should always be set up so the rooms are looped to be sequential.
        /// </summary>
        public int HasConnection(DungeonRoom room)
        {
            for (var i = 0; i < hallways.Length; i++)
            {
                if ((hallways[i].getPrev().getRoom() != null && hallways[i].getPrev().getRoom().GetId() == room.GetId())
                    || (hallways[i].getNext().getRoom() != null && hallways[i].getNext().getRoom().GetId() == room.GetId()))
                {
                    return i;
                }

            }
            return -1;
        }

        /// <summary>
        /// Returns the length of the hallway between room at given index.
        /// </summary>
        public int HallwayLength(int index)
        {
            var node = hallways[index];
            var count = 0;

            if (node.isNextRoom())
            {
                while (!node.isPrevRoom())
                {
                    count++;
                    node = node.getPrev();
                }
            } 
            else if (node.isPrevRoom())
            {
                while (!node.isNextRoom())
                {
                    count++;
                    node = node.getNext();
                }
            } 
            else
            {
                throw new Exception($"An error has occurred when " +
                                    $"retrieving the length of hallway between rooms.");
            }
            
            return count;
        }
    
    }
}