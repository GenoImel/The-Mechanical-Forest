public class DungeonRoom
{
    private DungeonRoom[] connections; // List of rooms you can go to from this one
    private int[] connectionDirection; // Direction you go in for each room. 0 = left, 1 = up, 2 = right, 3 = down, 4 up-left, 5 up-right, 6 down-right, 7 down-left, 8 left-up, 9 left-down, 10 right-up, 11 right-down
    private int[] hallwaySize; // How long you have to go to reach each room
    private int roomId = 0; // Unique room id

    // FIX: Determine if the hallway is what sets the encounter rate or if the room sets it. If hallway needs to change to array
    private double randomEventHallwayRate = 0.0; // Chance of encountering an event in the hallway
    private double randomBattleHallwayRate = 0.0; // Chance of encountering a battle in the hallway
    
    private double randomEventRoomChance = 0.0; // Chance of encountering an event in the room
    private double randomBattleRoomChance = 0.0; // Chance of encountering a battle in the room

    private bool endRoom = false; // Determine if this room is an exit

    // Create dungeon room with all the properties
    public DungeonRoom(DungeonRoom[] adjacentRooms, int[] dir, int[] size, int id, double rehr, double rbhr, double rerc, double rbrc, bool end = false)
    {
        connections = adjacentRooms;
        connectionDirection = dir;
        hallwaySize = size;
        roomId = id;
        randomEventHallwayRate = rehr;
        randomBattleHallwayRate = rbhr;
        randomEventRoomChance = rerc;
        randomBattleRoomChance = rbrc;
        endRoom = end;
    }

    // Create dungeon room with all the properties bundled
    public DungeonRoom(string JSONfileOrSomething)
    {
        // Json file will contain all the information about the current and future dungeonrooms
    }

    // Change the connection to a room and update related information
    public void changeConnections(DungeonRoom[] rooms, int[] dir, int[] size)
    {
        connections = rooms;
        connectionDirection = dir;
        hallwaySize = size;

        //FIX: Determine if we want to have other dungeon room also update their lists with this addition or just have to call with this one as well
    }

    // Only change what room goes where, keeps related information
    public void changeConnections(DungeonRoom[] rooms)
    {
        connections = rooms;
    }

    // Get the unique id of the room
    public int getId()
    {
        return roomId;
    }

    // Get all the relevant connections for traversal
    public (DungeonRoom[], int[], int[]) getConnections()
    {
        return (connections, connectionDirection, hallwaySize);
    }
    
}
