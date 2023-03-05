public class Map
{
    private DungeonRoom currentRoom;
    private int dungeonSize;

    private int mapDifficulty = 0; // Determines the rng ratio between empty room, battle room, and event room

    // Hard coded version of tutorial map
    public Map()
    {
        currentRoom = new DungeonRoom(new DungeonRoom[] { null }, new int[] { 2 }, new int[] { 4 }, 0, -1);
        DungeonRoom addRooms = currentRoom;
        DungeonRoom nextRoom = new DungeonRoom(new DungeonRoom[] { addRooms, null }, new int[] { 0, 3 }, new int[] { 4, 4 }, 1);
        addRooms.getConnections().Item1[0] = nextRoom;
        addRooms = nextRoom;
        nextRoom = new DungeonRoom(new DungeonRoom[] { addRooms, null }, new int[] { 1, 0 }, new int[] { 4, 4 }, 2);
        addRooms.getConnections().Item1[1] = nextRoom;
        addRooms = nextRoom;
        nextRoom = new DungeonRoom(new DungeonRoom[] { addRooms, null }, new int[] { 2, 3 }, new int[] { 4, 4 }, 3);
        addRooms.getConnections().Item1[1] = nextRoom;
        addRooms = nextRoom;
        nextRoom = new DungeonRoom(new DungeonRoom[] { addRooms, null }, new int[] { 1, 3 }, new int[] { 4, 4 }, 4);
        addRooms.getConnections().Item1[1] = nextRoom;
        addRooms = nextRoom;
        nextRoom = new DungeonRoom(new DungeonRoom[] { addRooms, null }, new int[] { 1, 2 }, new int[] { 4, 4 }, 5);
        addRooms.getConnections().Item1[1] = nextRoom;
        addRooms = nextRoom;
        nextRoom = new DungeonRoom(new DungeonRoom[] { addRooms, null }, new int[] { 0, 2 }, new int[] { 4, 4 }, 6);
        addRooms.getConnections().Item1[1] = nextRoom;
        addRooms = nextRoom;
        nextRoom = new DungeonRoom(new DungeonRoom[] { addRooms, null }, new int[] { 0, 1 }, new int[] { 4, 4 }, 7);
        addRooms.getConnections().Item1[1] = nextRoom;
        addRooms = nextRoom;
        nextRoom = new DungeonRoom(new DungeonRoom[] { addRooms, null }, new int[] { 3, 2 }, new int[] { 4, 4 }, 8);
        addRooms.getConnections().Item1[1] = nextRoom;
        addRooms = nextRoom;
        nextRoom = new DungeonRoom(new DungeonRoom[] { addRooms, null }, new int[] { 0, 1 }, new int[] { 4, 4 }, 9);
        addRooms.getConnections().Item1[1] = nextRoom;
        addRooms = nextRoom;
        nextRoom = new DungeonRoom(new DungeonRoom[] { addRooms, null }, new int[] { 3, 0 }, new int[] { 4, 4 }, 10);
        addRooms.getConnections().Item1[1] = nextRoom;
        addRooms = nextRoom;
        nextRoom = new DungeonRoom(new DungeonRoom[] { addRooms, null }, new int[] { 2, 1 }, new int[] { 4, 4 }, 11);
        addRooms.getConnections().Item1[1] = nextRoom;
        addRooms = nextRoom;
        nextRoom = new DungeonRoom(new DungeonRoom[] { addRooms, null }, new int[] { 3, 2 }, new int[] { 4, 4 }, 12);
        addRooms.getConnections().Item1[1] = nextRoom;
        addRooms = nextRoom;
        nextRoom = new DungeonRoom(new DungeonRoom[] { addRooms, null }, new int[] { 0, 2 }, new int[] { 4, 4 }, 13);
        addRooms.getConnections().Item1[1] = nextRoom;
        addRooms = nextRoom;
        nextRoom = new DungeonRoom(new DungeonRoom[] { addRooms, null }, new int[] { 0, 3 }, new int[] { 4, 4 }, 14);
        addRooms.getConnections().Item1[1] = nextRoom;
        addRooms = nextRoom;
        nextRoom = new DungeonRoom(new DungeonRoom[] { addRooms, null }, new int[] { 1, 3 }, new int[] { 4, 4 }, 15);
        addRooms.getConnections().Item1[1] = nextRoom;
        addRooms = nextRoom;
        nextRoom = new DungeonRoom(new DungeonRoom[] { addRooms }, new int[] { 1 }, new int[] { 4 }, 16, 0, true);
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
