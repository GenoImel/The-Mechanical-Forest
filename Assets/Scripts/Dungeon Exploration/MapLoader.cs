using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class MapLoader : MonoBehaviour
{
    Map map; // Map to generate the tileset for

    const float roomSize = 50f; // how large a room will be
    const float hallwaySize = 25f; // how large one section of a hallway will be

    Dictionary<int, Sprite> roomSprite = new Dictionary<int, Sprite>(); // Container for all the room images
    Sprite hallwayMinimapSprite; // Image of the hallway
    Sprite cursorSprite; // Image of the cursor

    // Delegates for when a room is clicked
    public delegate void OnRoomClick(DungeonRoom currentRoom, DungeonRoom room, float roomSize, float hallwaySize);
    public static event OnRoomClick onRoomClick;

    // Delegate for when a room is entered / hallway is left


    // Start is called before the first frame update
    void Start()
    {
        // Load in the map we are using
        map = new Map();

        // Load the images
        roomSprite.Add(0, Resources.Load<Sprite>("dungeon_map_room_unexplored"));
        roomSprite.Add(1, Resources.Load<Sprite>("dungeon_map_room_battle"));
        roomSprite.Add(2, Resources.Load<Sprite>("dungeon_map_room_event"));
        roomSprite.Add(3, Resources.Load<Sprite>("dungeon_map_room_empty"));
        roomSprite.Add(4, Resources.Load<Sprite>("dungeon_map_room_rest_active"));
        roomSprite.Add(5, Resources.Load<Sprite>("dungeon_map_room_rest_unactive"));
        roomSprite.Add(-1, Resources.Load<Sprite>("dungeon_map_room_explored"));

        hallwayMinimapSprite = Resources.Load<Sprite>("dungeon_map_hallway");
        cursorSprite = Resources.Load<Sprite>("dungeon_map_player");

        // Create the rooms and hallways 
        mapGeneration();

        // Create the cursor object and set it up
        GameObject cursor = new GameObject();
        cursor.name = "Cursor";
        cursor.transform.SetParent(gameObject.transform.parent);
        Image cursorImage = cursor.AddComponent<Image>();
        cursorImage.GetComponent<Image>().color = new Color32(255, 0, 0, 100);
        cursorImage.sprite = cursorSprite;
        cursor.transform.localPosition = new Vector3(-roomSize/2.0f,-roomSize/2.0f,0);
        cursor.GetComponent<RectTransform>().sizeDelta = new Vector2(roomSize, roomSize);
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Creates the Room buttons and the Hallways for the current floor of the dungeon
    void mapGeneration()
    {
        // Work on the closest rooms
        Queue<DungeonRoom> q = new Queue<DungeonRoom>();

        // Add the starting room
        q.Enqueue(map.getCurrentRoom());

        // Get the local position in the minimap
        Vector3 pos = new Vector3(roomSize / -2.0f, roomSize / -2.0f, 0);

        // While we still have a room
        while (q.Count > 0)
        {
            // Get the closest room
            DungeonRoom node = q.Dequeue();

            // Check to see if the node already has an object created for it
            // Should only be the first node
            if (gameObject.transform.Find("" + node.getId()) == null)
            {
                // Create the node and make it a child
                GameObject child = new GameObject();
                child.name = "" + node.getId();
                child.transform.SetParent(gameObject.transform);

                // Create the components
                Button button = child.AddComponent<Button>();
                button.onClick.AddListener(delegate { validNextRoom(child.name); });
                Image image = child.AddComponent<Image>();
                image.sprite = roomSprite[node.getState()];

                // Transform the child
                child.transform.localPosition = pos;
                child.GetComponent<RectTransform>().sizeDelta = new Vector2(roomSize, roomSize);

            }

            // Get the connections to the room we are working on
            DoublyLinkedList[] hallways = node.getConnections();

            // For each room we have a connection with
            for (int i = 0; i < hallways.Length; i++)
            {
                // Determine variables regarding whether we work with the head or the tail
                bool nodeNotCreated = false;
                DungeonRoom dungeonRoom;
                bool goForward = false;

                if (hallways[i].getPrev().getRoom() == null) // Starting from the tail
                {
                    nodeNotCreated = (hallways[i].getRoom().getId() != node.getId() && gameObject.transform.Find("" + hallways[i].getRoom().getId()) == null) || (hallways[i].getNext().getRoom() != null && hallways[i].getNext().getRoom().getId() != node.getId() && gameObject.transform.Find("" + hallways[i].getNext().getRoom().getId()) == null);
                    dungeonRoom = hallways[i].getNext().getRoom();
                } else
                {
                    nodeNotCreated = (hallways[i].getRoom().getId() != node.getId() && gameObject.transform.Find("" + hallways[i].getRoom().getId()) == null) || (hallways[i].getPrev().getRoom() != null && hallways[i].getPrev().getRoom().getId() != node.getId() && gameObject.transform.Find("" + hallways[i].getPrev().getRoom().getId()) == null);
                    dungeonRoom = hallways[i].getPrev().getRoom();
                    goForward = true;
                }

                // GET THIS WORKING AFTER I CHANGE HALLWAY SEGMENT CREATION

                // Check to see if the node already has an object created for it
                if (nodeNotCreated)
                {
                    // Create the node and make it a child
                    GameObject room = new GameObject();
                    room.name = "" + dungeonRoom.getId();
                    room.transform.SetParent(gameObject.transform);

                    // Create the components for it
                    Button button = room.AddComponent<Button>();
                    button.onClick.AddListener(delegate { validNextRoom(room.name); });
                    Image image = room.AddComponent<Image>();
                    image.sprite = roomSprite[dungeonRoom.getState()];

                    // Create the path between rooms
                    GameObject hallway = new GameObject();
                    hallway.transform.SetParent(room.transform);

                    // Iterate on the segments
                    DoublyLinkedList iter = hallways[i];

                    // Find the direction to travel
                    if (goForward)
                    {
                        iter = iter.getNext();
                    } else
                    {
                        iter = iter.getPrev();
                    }

                    // Keep track of the segment number for naming purposes
                    int segmentNum = 0;

                    // Keep track of the local hallway position
                    Vector3 hallwayPos = new Vector3(0, 0, 0);
                    
                    // Get the starting center point
                    switch (iter.getNextDirection())
                    {
                        case 0:
                            hallwayPos = new Vector3((roomSize + hallwaySize) / 2.0f, 0, 0);
                            pos.x -= roomSize;
                            break;
                        case 1:
                            hallwayPos = new Vector3(0, (roomSize + hallwaySize) / -2.0f, 0);
                            pos.y += roomSize;
                            break;
                        case 2:
                            hallwayPos = new Vector3((roomSize + hallwaySize) / -2.0f, 0, 0);
                            pos.x += roomSize;
                            break;
                        case 3:
                            hallwayPos = new Vector3(0, (roomSize + hallwaySize) / 2.0f, 0);
                            pos.y -= roomSize;
                            break;
                    }

                    // Iterate until we reach the other side
                    while (iter.getRoom() == null)
                    {

                        // Position the new room and hallway and scale the hallway based off of the direction between rooms
                        switch (iter.getNextDirection())
                        {
                            // Cardinal direction
                            case 0:
                                // Method that creates less gameobjects but harder to individually configure individual sections
                                //hallway.GetComponent<RectTransform>().sizeDelta = new Vector2(hallwaySize * sizes[i], hallwaySize);
                                //hallway.transform.localPosition = new Vector3((hallwaySize * sizes[i] + roomSize) / 2.0f, 0, 0);

                                // Create the segments positive horizontally
                                createHallwaySegment(hallway, hallwayPos.x, hallwayPos.y, segmentNum);

                                // Transform the position of the room
                                pos.x -= hallwaySize;
                                hallwayPos.x += hallwaySize;
                                break;
                            case 1:
                                // Create the segments negative vertically
                                createHallwaySegment(hallway, hallwayPos.x, hallwayPos.y, segmentNum);

                                // Transform the position of the room
                                pos.y += hallwaySize;
                                hallwayPos.y -= hallwaySize;
                                break;
                            case 2:
                                // Create the segments negative horizontally
                                createHallwaySegment(hallway, hallwayPos.x, hallwayPos.y, segmentNum);

                                // Transform the position of the room
                                pos.x += hallwaySize;
                                hallwayPos.x -= hallwaySize;
                                break;
                            case 3:
                                // Create the segments positive vertically
                                createHallwaySegment(hallway, hallwayPos.x, hallwayPos.y, segmentNum);

                                // Transform the posiiton of the room
                                pos.y -= hallwaySize;
                                hallwayPos.y += hallwaySize;
                                break;
                        }

                        // Increate the segement number
                        segmentNum++;

                        // Go to the next segment
                        if (goForward)
                        {
                            iter = iter.getNext();
                        } else
                        {
                            iter = iter.getPrev();
                        }

                    }



                    // Set the name of the root hallway
                    hallway.name = node.getId() + "-" + dungeonRoom.getId();

                    // Actually transform and scale the new room
                    room.transform.localPosition = pos;
                    room.GetComponent<RectTransform>().sizeDelta = new Vector2(roomSize, roomSize);

                    // Add this room to the queue
                    q.Enqueue(dungeonRoom);
                }
                else
                {
                    // Check to see if the path has already been created
                    if (gameObject.transform.Find("" + node.getId()).transform.Find(dungeonRoom.getId() + "-" + node.getId()) == null &&
                        gameObject.transform.Find("" + dungeonRoom.getId()).transform.Find(node.getId() + "-" + dungeonRoom.getId()) == null)
                    {
                        // Create the path between rooms
                        GameObject hallway = new GameObject();
                        hallway.name = node.getId() + "-" + dungeonRoom.getId();
                        hallway.transform.SetParent(gameObject.transform.Find("" + dungeonRoom.getId()).transform);
                        Image image2 = hallway.AddComponent<Image>();

                        // Iterate on the segments
                        DoublyLinkedList iter = hallways[i];

                        // Find the direction to travel
                        if (goForward)
                        {
                            iter = iter.getNext();
                        }
                        else
                        {
                            iter = iter.getPrev();
                        }

                        // Keep track of the segment number for naming purposes
                        int segmentNum = 0;

                        // Keep track of the local hallway position
                        Vector3 hallwayPos = new Vector3(0, 0, 0);

                        // Get the starting center point
                        switch (iter.getNextDirection())
                        {
                            case 0:
                                hallwayPos = new Vector3((roomSize + hallwaySize) / 2.0f, 0, 0);
                                pos.x -= roomSize;
                                break;
                            case 1:
                                hallwayPos = new Vector3(0, (roomSize + hallwaySize) / -2.0f, 0);
                                pos.y += roomSize;
                                break;
                            case 2:
                                hallwayPos = new Vector3((roomSize + hallwaySize) / -2.0f, 0, 0);
                                pos.x += roomSize;
                                break;
                            case 3:
                                hallwayPos = new Vector3(0,  (roomSize + hallwaySize) / 2.0f, 0);
                                pos.y -= roomSize;
                                break;
                        }

                        // Iterate until we reach the other side
                        while (iter.getRoom() == null)
                        {

                            // Position the new room and hallway and scale the hallway based off of the direction between rooms
                            switch (iter.getNextDirection())
                            {
                                // Cardinal direction
                                case 0:
                                    // Method that creates less gameobjects but harder to individually configure individual sections
                                    //hallway.GetComponent<RectTransform>().sizeDelta = new Vector2(hallwaySize * sizes[i], hallwaySize);
                                    //hallway.transform.localPosition = new Vector3((hallwaySize * sizes[i] + roomSize) / 2.0f, 0, 0);

                                    // Create the segments positive horizontally
                                    createHallwaySegment(hallway, hallwayPos.x, hallwayPos.y, segmentNum);

                                    // Transform the position of the room
                                    pos.x -= hallwaySize;
                                    hallwayPos.x += hallwaySize;
                                    break;
                                case 1:
                                    // Create the segments negative vertically
                                    createHallwaySegment(hallway, hallwayPos.x, hallwayPos.y, segmentNum);

                                    // Transform the position of the room
                                    pos.y += hallwaySize;
                                    hallwayPos.y -= hallwaySize;
                                    break;
                                case 2:
                                    // Create the segments negative horizontally
                                    createHallwaySegment(hallway, hallwayPos.x, hallwayPos.y, segmentNum);

                                    // Transform the position of the room
                                    pos.x += hallwaySize;
                                    hallwayPos.x -= hallwaySize;
                                    break;
                                case 3:
                                    // Create the segments positive vertically
                                    createHallwaySegment(hallway, hallwayPos.x, hallwayPos.y, segmentNum);

                                    // Transform the posiiton of the room
                                    pos.y -= hallwaySize;
                                    hallwayPos.y += hallwaySize;
                                    break;
                            }

                            // Increate the segement number
                            segmentNum++;

                            // Go to the next segment
                            if (goForward)
                            {
                                iter = iter.getNext();
                            }
                            else
                            {
                                iter = iter.getPrev();
                            }

                        }

                        // Add the room to the queue
                        q.Enqueue(dungeonRoom);
                    }
                }
            }
        }
    }

    // Create individual segments in the hallway
    private void createHallwaySegment(GameObject hallway, float xPos, float yPos, int segmentNumber)
    {
        // Create a new section game object
        GameObject section = new GameObject();
        section.AddComponent<RectTransform>();

        // Apply the hallway sprite
        Image image2 = section.AddComponent<Image>();
        image2.sprite = hallwayMinimapSprite;

        // Name, set parent, set size, and position the section
        section.name = "" + segmentNumber;
        section.transform.SetParent(hallway.transform);
        section.GetComponent<RectTransform>().sizeDelta = new Vector2(hallwaySize, hallwaySize);
        section.transform.localPosition = new Vector3(xPos, yPos);

        // POTENTIAL HALLWAY SCRIPT INSERTION


    }


    // Given a room id check to see if that room is connected our current one
    private void validNextRoom(string roomId)
    {
        foreach (DoublyLinkedList list in map.getCurrentRoom().getConnections())
        {
            if ("" + list.getRoom().getId() == roomId) // Shouldn't enter this one
            {
                changeRoom(list.getRoom());
                return;
            } else if ("" + list.getPrev().getRoom().getId() == roomId)
            {
                changeRoom(list.getPrev().getRoom());
                return;
            } else if ("" + list.getNext().getRoom().getId() == roomId)
            {
                changeRoom(list.getNext().getRoom());
                return;
            }
        }
    }

    // Invoke the event to change rooms for other scripts and disable room clicking checks
    private void changeRoom(DungeonRoom room)
    {
        // Invoke the delegate for the other classes
        onRoomClick?.Invoke(map.getCurrentRoom(), room, roomSize, hallwaySize);

        // Get all the buttons in the rooms
        Component[] buttons = GetComponentsInChildren<Button>();

        // Disable each of them
        foreach (Button button in buttons)
        {
            button.enabled = false;
        }

        // DungeonExploration.Delegate += enterRoom(room)
        // ^ To add function that will call once hallway is exited to a room
    }

    // Set the current room and enable all buttons once we enter a room
    // Called from a delegate event
    private void enterRoom(DungeonRoom room)
    {
        map.setCurrentRoom(room);

        // Get all the buttons in the rooms
        Component[] buttons = GetComponentsInChildren<Button>();

        // Disable each of them
        foreach (Button button in buttons)
        {
            button.enabled = true;
        }

        // Unsubscribe from the event since we aren't looking for it anymore currently
        // DungeonExploration.Delegate -= enterRoom(room)

    }
}
