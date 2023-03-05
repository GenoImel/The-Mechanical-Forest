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
    Sprite hallwaySprite; // Image of the hallway
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

        hallwaySprite = Resources.Load<Sprite>("dungeon_map_hallway");
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

                node.getState();
                image.sprite = roomSprite[node.getState()];

                // Transform the child
                child.transform.localPosition = pos;
                child.GetComponent<RectTransform>().sizeDelta = new Vector2(roomSize, roomSize);

            }

            // Get the connections to the room we are working on
            (DungeonRoom[], int[], int[]) temp = node.getConnections();
            DungeonRoom[] rooms = temp.Item1;
            int[] directions = temp.Item2;
            int[] sizes = temp.Item3;

            // For each room we have a connection with
            for (int i = 0; i < rooms.Length; i++)
            {
                // Check to see if the node already has an object created for it
                if (gameObject.transform.Find("" + rooms[i].getId()) == null)
                {
                    // Create the node and make it a child
                    GameObject room = new GameObject();
                    room.name = "" + rooms[i].getId();
                    room.transform.SetParent(gameObject.transform);

                    // Create the components for it
                    Button button = room.AddComponent<Button>();
                    button.onClick.AddListener(delegate { validNextRoom(room.name); });
                    Image image = room.AddComponent<Image>();
                    image.sprite = roomSprite[rooms[i].getState()];

                    // Create the path between rooms
                    GameObject hallway = new GameObject();
                    hallway.transform.SetParent(room.transform);

                    // Position the new room and hallway and scale the hallway based off of the direction between rooms
                    switch (directions[i])
                    {
                        // Cardinal direction
                        case 0:
                            // Method that creates less gameobjects but harder to individually configure individual sections
                            //hallway.GetComponent<RectTransform>().sizeDelta = new Vector2(hallwaySize * sizes[i], hallwaySize);
                            //hallway.transform.localPosition = new Vector3((hallwaySize * sizes[i] + roomSize) / 2.0f, 0, 0);

                            // Create the segments positive horizontally
                            createHallwaySegment(hallway, sizes[i], 0, 1, 1, true);

                            // Transform the position of the room
                            pos.x -= hallwaySize * sizes[i] + roomSize;
                            break;
                        case 1:
                            // Create the segments negative vertically
                            createHallwaySegment(hallway, 0, sizes[i], 1, -1, false);

                            // Transform the position of the room
                            pos.y += (float)hallwaySize * sizes[i] + roomSize;
                            break;
                        case 2:
                            // Create the segments negative horizontally
                            createHallwaySegment(hallway, sizes[i], 0, -1, 1, true);

                            // Transform the position of the room
                            pos.x += (float)hallwaySize * sizes[i] + roomSize;
                            break;
                        case 3:
                            // Create the segments positive vertically
                            createHallwaySegment(hallway, 0, sizes[i], 1, 1, false);

                            // Transform the posiiton of the room
                            pos.y -= (float)hallwaySize * sizes[i] + roomSize;
                            break;

                        // Intermediate direction focused on the y direction
                        case 4:


                            break;
                        case 5:
                            break;
                        case 6:
                            break;
                        case 7:
                            break;

                        // Intermediate direction focused on the x direction
                        case 8:
                            break;
                        case 9:
                            break;
                        case 10:
                            break;
                        case 11:
                            break;
                    }
                    // Set the name of the root hallway
                    hallway.name = node.getId() + "-" + rooms[i].getId();

                    // Actually transform and scale the new room
                    room.transform.localPosition = pos;
                    room.GetComponent<RectTransform>().sizeDelta = new Vector2(roomSize, roomSize);

                    // Add this room to the queue
                    q.Enqueue(rooms[i]);
                }
                else
                {
                    // Check to see if the path has already been created
                    if (gameObject.transform.Find("" + node.getId()).transform.Find(rooms[i].getId() + "-" + node.getId()) == null &&
                        gameObject.transform.Find("" + rooms[i].getId()).transform.Find(node.getId() + "-" + rooms[i].getId()) == null)
                    {
                        // Create the path between rooms
                        GameObject hallway = new GameObject();
                        hallway.name = node.getId() + "-" + rooms[i].getId();
                        hallway.transform.SetParent(gameObject.transform.Find("" + rooms[i].getId()).transform);
                        Image image2 = hallway.AddComponent<Image>();

                        // Position the new room and hallway and scale the hallway based off of the direction between rooms
                        switch (directions[i])
                        {
                            // Cardinal direction
                            case 0:
                                hallway.GetComponent<RectTransform>().sizeDelta = new Vector2(hallwaySize * sizes[i], hallwaySize);
                                hallway.transform.localPosition = new Vector3((hallwaySize * sizes[i] + roomSize) / 2.0f, 0, 0);
                                break;
                            case 1:
                                hallway.GetComponent<RectTransform>().sizeDelta = new Vector2(hallwaySize, hallwaySize * sizes[i]);
                                hallway.transform.localPosition = new Vector3(0, -(hallwaySize * sizes[i] + roomSize) / 2.0f, 0);
                                break;
                            case 2:
                                hallway.GetComponent<RectTransform>().sizeDelta = new Vector2(hallwaySize * sizes[i], hallwaySize);
                                hallway.transform.localPosition = new Vector3(-(hallwaySize * sizes[i] + roomSize) / 2.0f, 0, 0);
                                break;
                            case 3:
                                hallway.GetComponent<RectTransform>().sizeDelta = new Vector2(hallwaySize, hallwaySize * sizes[i]);
                                hallway.transform.localPosition = new Vector3(0, (hallwaySize * sizes[i] + roomSize) / 2.0f, 0);
                                break;

                            // Intermediate direction focused on the y direction
                            case 4:
                                break;
                            case 5:
                                break;
                            case 6:
                                break;
                            case 7:
                                break;

                            // Intermediate direction focused on the x direction
                            case 8:
                                break;
                            case 9:
                                break;
                            case 10:
                                break;
                            case 11:
                                break;
                        }

                        // Add the room to the queue
                        q.Enqueue(rooms[i]);
                    }
                }

            }
        }
    }

    // Create individual segments in the hallway
    private void createHallwaySegment(GameObject hallway, int horizontalLength, int verticalLength, int horizontalSign, int verticalSign, bool horizontalFirst)
    {
        // PROBABLY WILL NEED TO CHANGE THIS STRUCTURE
        // If horizontal is the focus
        if (horizontalFirst)
        {
            // Go through all of the horizontal spaces
            for (int i = 0; i < horizontalLength; i++)
            {
                // Create a new section game object
                GameObject section = new GameObject();
                section.AddComponent<RectTransform>();

                // Apply the hallway sprite
                Image image2 = section.AddComponent<Image>();
                image2.sprite = hallwaySprite;

                // Name, set parent, set size, and position the section
                section.name = "" + i;
                section.transform.SetParent(hallway.transform);
                section.GetComponent<RectTransform>().sizeDelta = new Vector2(hallwaySize, hallwaySize);
                section.transform.localPosition = new Vector3(hallwaySize * i * horizontalSign, 0, 0);

                // PROBABLY WILL ATTACH HALLWAY SCRIPT HERE

            }
            // Set the entire hallway to correct starting position
            hallway.transform.localPosition += new Vector3(horizontalSign * (roomSize + hallwaySize) / 2.0f, 0, 0);
        }
        else
        // Ditto but for vertically
        {
            for (int i = 0; i < verticalLength; i++)
            {
                GameObject section = new GameObject();
                section.AddComponent<RectTransform>();
                Image image2 = section.AddComponent<Image>();
                image2.sprite = hallwaySprite;
                section.name = "" + i;
                section.transform.SetParent(hallway.transform);
                section.GetComponent<RectTransform>().sizeDelta = new Vector2(hallwaySize, hallwaySize);
                section.transform.localPosition = new Vector3(0, hallwaySize * i * verticalSign, 0);

            }
            hallway.transform.localPosition += new Vector3(0, verticalSign * (roomSize + hallwaySize) / 2.0f, 0);
        }
    }


    // Given a room id check to see if that room is connected our current one
    private void validNextRoom(string roomId)
    {
        foreach (DungeonRoom currentConnections in map.getCurrentRoom().getConnections().Item1)
        {
            if ("" + currentConnections.getId() == roomId)
            {
                changeRoom(currentConnections);
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

        // Class.Delegate += enterRoom(room)
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
        // Class.Delegat -= enterRoom(room)

    }
}
