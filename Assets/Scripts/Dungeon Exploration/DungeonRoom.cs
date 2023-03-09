public class DungeonRoom
{
    private int roomId = 0; // Unique room id

    private DoublyLinkedList[] hallways; // Contains all hallway paths for this room

    private int roomState = 0; // State of the room. 0 = unexplored, 1 = unexplored enemy, 2 = unexplored event, 3 = unexplored empty, 4 = rest stop active, 5 = rest stop unactive, -1 = explored

    private bool endRoom = false; // Determine if this room is an exit to the next floor

    // Create dungeon room with all the properties
    public DungeonRoom(DoublyLinkedList[] hallway, int id, int state = 0, bool end = false)
    {
        hallways = hallway;
        roomId = id;
        roomState = state;
        endRoom = end;
    }

    // Create dungeon room with all the properties bundled
    public DungeonRoom(string JSONfileOrSomething)
    {
        // Json file will contain all the information about the current and future dungeonrooms
    }

    // Modify the hallway list
    public void modifyHallways(DoublyLinkedList[] hallway)
    {
        hallways = hallway;
    }

    // Get the unique id of the room
    public int getId()
    {
        return roomId;
    }

    // Get the state of the room
    public int getState()
    {
        return roomState;
    }

    // Get all the relevant connections for traversal
    public DoublyLinkedList[] getConnections()
    {
        return hallways;
    }

    // Return the index where a connection exists, -1 if it doesn't
    public int hasConnection(DungeonRoom room)
    {
        for (int i = 0; i < hallways.Length; i++)
        {
            if ((hallways[i].getPrev().getRoom() != null && hallways[i].getPrev().getRoom().getId() == room.getId()) || (hallways[i].getNext().getRoom() != null && hallways[i].getNext().getRoom().getId() == room.getId())) // Hallway should always be set up so the rooms are looped to be sequential
            {
                return i;
            }

        }
        return -1;
    }
    
}