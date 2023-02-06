public class Map
{
    private DungeonRoom currentRoom;
    private int dungeonSize;

    // Hard coded version of tutorial map
    public Map()
    {
        currentRoom = new DungeonRoom(new DungeonRoom[] { null }, new int[] { 2 }, new int[] { 4 }, 0, 0.0, 0.0, 0.0, 0.0);
        DungeonRoom addRooms = currentRoom;
        DungeonRoom nextRoom = new DungeonRoom(new DungeonRoom[] { addRooms, null }, new int[] { 0, 3 }, new int[] { 4, 4 }, 1, 0.0, 0.0, 0.0, 0.0);
        addRooms.getConnections().Item1[0] = nextRoom;
        addRooms = nextRoom;
        nextRoom = new DungeonRoom(new DungeonRoom[] { addRooms, null }, new int[] { 1, 0 }, new int[] { 4, 4 }, 2, 0.0, 0.0, 0.0, 0.0);
        addRooms.getConnections().Item1[1] = nextRoom;
        addRooms = nextRoom;
        nextRoom = new DungeonRoom(new DungeonRoom[] { addRooms, null }, new int[] { 2, 3 }, new int[] { 4, 4 }, 3, 0.0, 0.0, 0.0, 0.0);
        addRooms.getConnections().Item1[1] = nextRoom;
        addRooms = nextRoom;
        nextRoom = new DungeonRoom(new DungeonRoom[] { addRooms, null }, new int[] { 1, 3 }, new int[] { 4, 4 }, 4, 0.0, 0.0, 0.0, 0.0);
        addRooms.getConnections().Item1[1] = nextRoom;
        addRooms = nextRoom;
        nextRoom = new DungeonRoom(new DungeonRoom[] { addRooms, null }, new int[] { 1, 2 }, new int[] { 4, 4 }, 5, 0.0, 0.0, 0.0, 0.0);
        addRooms.getConnections().Item1[1] = nextRoom;
        addRooms = nextRoom;
        nextRoom = new DungeonRoom(new DungeonRoom[] { addRooms, null }, new int[] { 0, 2 }, new int[] { 4, 4 }, 6, 0.0, 0.0, 0.0, 1.0);
        addRooms.getConnections().Item1[1] = nextRoom;
        addRooms = nextRoom;
        nextRoom = new DungeonRoom(new DungeonRoom[] { addRooms, null }, new int[] { 0, 1 }, new int[] { 4, 4 }, 7, 0.0, 0.0, 0.0, 0.0);
        addRooms.getConnections().Item1[1] = nextRoom;
        addRooms = nextRoom;
        nextRoom = new DungeonRoom(new DungeonRoom[] { addRooms, null }, new int[] { 3, 2 }, new int[] { 4, 4 }, 8, 0.0, 0.0, 0.0, 0.0);
        addRooms.getConnections().Item1[1] = nextRoom;
        addRooms = nextRoom;
        nextRoom = new DungeonRoom(new DungeonRoom[] { addRooms, null }, new int[] { 0, 1 }, new int[] { 4, 4 }, 9, 0.0, 0.0, 0.0, 0.0);
        addRooms.getConnections().Item1[1] = nextRoom;
        addRooms = nextRoom;
        nextRoom = new DungeonRoom(new DungeonRoom[] { addRooms, null }, new int[] { 3, 0 }, new int[] { 4, 4 }, 10, 0.0, 0.0, 0.0, 0.0);
        addRooms.getConnections().Item1[1] = nextRoom;
        addRooms = nextRoom;
        nextRoom = new DungeonRoom(new DungeonRoom[] { addRooms, null }, new int[] { 2, 1 }, new int[] { 4, 4 }, 11, 0.0, 0.0, 0.0, 0.0);
        addRooms.getConnections().Item1[1] = nextRoom;
        addRooms = nextRoom;
        nextRoom = new DungeonRoom(new DungeonRoom[] { addRooms, null }, new int[] { 3, 2 }, new int[] { 4, 4 }, 12, 0.0, 0.0, 0.0, 0.0);
        addRooms.getConnections().Item1[1] = nextRoom;
        addRooms = nextRoom;
        nextRoom = new DungeonRoom(new DungeonRoom[] { addRooms, null }, new int[] { 0, 2 }, new int[] { 4, 4 }, 13, 0.0, 0.0, 0.0, 0.0);
        addRooms.getConnections().Item1[1] = nextRoom;
        addRooms = nextRoom;
        nextRoom = new DungeonRoom(new DungeonRoom[] { addRooms, null }, new int[] { 0, 3 }, new int[] { 4, 4 }, 14, 0.0, 0.0, 0.0, 0.0);
        addRooms.getConnections().Item1[1] = nextRoom;
        addRooms = nextRoom;
        nextRoom = new DungeonRoom(new DungeonRoom[] { addRooms, null }, new int[] { 1, 3 }, new int[] { 4, 4 }, 15, 0.0, 0.0, 0.0, 0.0);
        addRooms.getConnections().Item1[1] = nextRoom;
        addRooms = nextRoom;
        nextRoom = new DungeonRoom(new DungeonRoom[] { addRooms }, new int[] { 1 }, new int[] { 4 }, 16, 0.0, 0.0, 0.0, 0.0, true);
        addRooms.getConnections().Item1[1] = nextRoom;
        dungeonSize = 17;
    }

    // Given the json file with the properties for each room the map should have
    public Map(string JSONfile)
    {
        
    }

    // Set the current room that we came from / are exploring
    public void setCurrentRoom(DungeonRoom room)
    {
        currentRoom = room;
    }

    // Get the current room
    public DungeonRoom getCurrentRoom()
    {
        return currentRoom;
    }

    // Get the number of rooms in the dungeon
    public int getSize()
    {
        return dungeonSize;
    }
}
