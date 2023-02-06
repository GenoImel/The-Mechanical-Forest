using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapLoader : MonoBehaviour
{
    Map map; // Map to generate the tileset for
    const float roomSize = 50f; // how large a room will be
    const float hallwaySize = 25f; // how large one section of a hallway will be

    // Start is called before the first frame update
    void Start()
    {
        // Load in the map we are using
        map = new Map();

        // Create the rooms and hallways 
        mapGeneration();
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
        Vector3 pos = new Vector3(roomSize/-2.0f, roomSize/-2.0f, 0);

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
                Image image = child.AddComponent<Image>();

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
                    Image image = room.AddComponent<Image>();

                    // Create the path between rooms
                    GameObject hallway = new GameObject();
                    hallway.name = node.getId() + "-" + rooms[i].getId();
                    hallway.transform.SetParent(room.transform);
                    Image image2 = hallway.AddComponent<Image>();

                    // Position the new room and hallway and scale the hallway based off of the direction between rooms
                    switch (directions[i])
                    {
                        // Cardinal direction
                        case 0:
                            hallway.GetComponent<RectTransform>().sizeDelta = new Vector2(hallwaySize * sizes[i], hallwaySize);
                            hallway.transform.localPosition = new Vector3((hallwaySize * sizes[i] + roomSize) / 2.0f, 0, 0);

                            pos.x -= hallwaySize * sizes[i] + roomSize;
                            break;
                        case 1:
                            hallway.GetComponent<RectTransform>().sizeDelta = new Vector2(hallwaySize, hallwaySize * sizes[i]);
                            hallway.transform.localPosition = new Vector3(0, -(hallwaySize * sizes[i] + roomSize) / 2.0f, 0);

                            pos.y += (float) hallwaySize * sizes[i] + roomSize;
                            break;
                        case 2:
                            hallway.GetComponent<RectTransform>().sizeDelta = new Vector2(hallwaySize * sizes[i], hallwaySize);
                            hallway.transform.localPosition = new Vector3(-(hallwaySize * sizes[i] + roomSize) / 2.0f, 0, 0);

                            pos.x += (float) hallwaySize * sizes[i] + roomSize;
                            break;
                        case 3:
                            hallway.GetComponent<RectTransform>().sizeDelta = new Vector2(hallwaySize, hallwaySize * sizes[i]);
                            hallway.transform.localPosition = new Vector3(0, (hallwaySize * sizes[i] + roomSize) / 2.0f, 0);

                            pos.y -= (float) hallwaySize * sizes[i] + roomSize;
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

                    // Actually transform and scale the new room
                    room.transform.localPosition = pos;
                    room.GetComponent<RectTransform>().sizeDelta = new Vector2(roomSize, roomSize);

                    // Add this room to the queue
                    q.Enqueue(rooms[i]);
                } else {
                    // Check to see if the path has already been created
                    if (gameObject.transform.Find("" + node.getId()).transform.Find(rooms[i].getId() + "-" + node.getId()) == null && 
                        gameObject.transform.Find("" + rooms[i].getId()).transform.Find(node.getId() + "-" + rooms[i].getId()) == null)
                    {
                        // Should never get in here i think
                        // But just in case
                        Debug.Log("MAKE SURE THE LOGIC IN BOTH NODES EXIST BUT NOT THAT PATH BETWEEN THEM WORKS CORRECTLY BECAUSE IT HAPPENED HERE");


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
}
