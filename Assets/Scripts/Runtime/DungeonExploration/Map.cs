namespace Akashic.Runtime.DungeonExploration
{
    internal sealed class Map
    {
        //TODO: do some extreme refactoring, and get away from using a hard coded map.
        private DungeonRoom currentRoom;
        private int dungeonSize;

        private int mapDifficulty = 0; // Determines the rng ratio between empty room, battle room, and event room

        // Hard coded version of tutorial map
        public Map()
        {
            DoublyLinkedList head = new DoublyLinkedList(2);
            DoublyLinkedList segment1 = new DoublyLinkedList(2);
            DoublyLinkedList segment2 = new DoublyLinkedList(2);
            DoublyLinkedList segment3 = new DoublyLinkedList(2);
            DoublyLinkedList segment4 = new DoublyLinkedList(2);
            DoublyLinkedList tail = new DoublyLinkedList(2);
            head.setNext(segment1);
            head.setPrev(tail);
            segment1.setNext(segment2);
            segment1.setPrev(head);
            segment2.setNext(segment3);
            segment2.setPrev(segment1);
            segment3.setNext(segment4);
            segment3.setPrev(segment2);
            segment4.setNext(tail);
            segment4.setPrev(segment3);
            tail.setNext(head);
            tail.setPrev(segment4);


            currentRoom = new DungeonRoom(new DoublyLinkedList[] {null},  0, -1, true);
            DungeonRoom nextRoom = new DungeonRoom(new DoublyLinkedList[] { null, null }, 1);
            head.setRoom(currentRoom);
            tail.setRoom(nextRoom);
            currentRoom.GetConnections()[0] = head;
            nextRoom.GetConnections()[0] = tail;

            nextRoom = addHardCodedRoom(nextRoom, false, 2, 3);

            nextRoom = addHardCodedRoom(nextRoom, false, 3, 0);

            nextRoom = addHardCodedRoom(nextRoom, false, 4, 3);

            nextRoom = addHardCodedRoom(nextRoom, false, 5, 3);

            nextRoom = addHardCodedRoom(nextRoom, false, 6, 2);

            nextRoom = addHardCodedRoom(nextRoom, false, 7, 2);

            nextRoom = addHardCodedRoom(nextRoom, false, 8, 1);

            nextRoom = addHardCodedRoom(nextRoom, false, 9, 2);

            nextRoom = addHardCodedRoom(nextRoom, false, 10, 1);

            nextRoom = addHardCodedRoom(nextRoom, false, 11, 0);

            nextRoom = addHardCodedRoom(nextRoom, false, 12, 1);

            nextRoom = addHardCodedRoom(nextRoom, false, 13, 2);

            nextRoom = addHardCodedRoom(nextRoom, false, 14, 2);

            nextRoom = addHardCodedRoom(nextRoom, false, 15, 3);

            nextRoom = addHardCodedRoom(nextRoom, true, 16, 3);

            dungeonSize = 17;
        }

        // Given the json file with the properties for each room the map should have
        public Map(string JSONfile)
        {
        
        }

        public DungeonRoom addHardCodedRoom(DungeonRoom nextRoom, bool end, int id, int direction)
        {
            DungeonRoom addrRoom = nextRoom;

            if (end)
            {
            
                nextRoom = new DungeonRoom(new DoublyLinkedList[] { null}, id, 0, true);
            
            }
            else
            {
                nextRoom = new DungeonRoom(new DoublyLinkedList[] { null, null }, id);
            }

            DoublyLinkedList head = new DoublyLinkedList(direction);
            DoublyLinkedList segment1 = new DoublyLinkedList(direction);
            DoublyLinkedList segment2 = new DoublyLinkedList(direction);
            DoublyLinkedList segment3 = new DoublyLinkedList(direction);
            DoublyLinkedList segment4 = new DoublyLinkedList(direction);
            DoublyLinkedList tail = new DoublyLinkedList(direction);
            head.setNext(segment1);
            head.setPrev(tail);
            segment1.setNext(segment2);
            segment1.setPrev(head);
            segment2.setNext(segment3);
            segment2.setPrev(segment1);
            segment3.setNext(segment4);
            segment3.setPrev(segment2);
            segment4.setNext(tail);
            segment4.setPrev(segment3);
            tail.setNext(head);
            tail.setPrev(segment4);
            head.setRoom(addrRoom);
            tail.setRoom(nextRoom);
            addrRoom.GetConnections()[1] = head;
            nextRoom.GetConnections()[0] = tail;

            return nextRoom;
        }

        // Set the current room that we came from / are exploring
        public void setCurrentRoom(DungeonRoom room)
        {
            currentRoom = room;
        }

        public void setDifficulty(int dif)
        {
            mapDifficulty = dif;
        }

        // Get the current room
        public DungeonRoom getCurrentRoom()
        {
            return currentRoom;
        }

        public int getDifficulty()
        {
            return mapDifficulty;
        }

        // Get the number of rooms in the dungeon
        public int getSize()
        {
            return dungeonSize;
        }
    }
}
