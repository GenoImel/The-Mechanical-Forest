using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class DungeonExploration : MonoBehaviour
{
    GameObject parent;
    bool exploreHallway = false;
    float exploreSpeed = 0.1f; // Every frame input is pushed, we move this percentage across one hallway segment
    float position;

    // Delegates for when a room is clicked
    public delegate void OnRoomEnter(DungeonRoom room);
    public static event OnRoomEnter onRoomEnter;

    // Sprites to load hallway sprites
    Sprite hallwaySprite;
    Sprite leftDoorSprite;
    Sprite rightDoorSprite;


    // OnEnable is called before Start
    private void OnEnable()
    {
        MapLoader.onRoomClick += enterHallway;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Get the parent of the rooms
        parent = gameObject.transform.GetChild(0).GetChild(0).gameObject;

        // Load the sprites
        hallwaySprite = Resources.Load<Sprite>("dungeon_hallway");
        leftDoorSprite = Resources.Load<Sprite>("dungeon_hallway_door_left");
        rightDoorSprite = Resources.Load<Sprite>("dungeon_hallway_door_right");

    }

    // Update is called once per frame
    void Update()
    {
        if (exploreHallway)
        {
            // Defaults to reading WASD and arrow keys
            float translation = Input.GetAxis("Horizontal");
            translation *= Time.deltaTime;

            position += translation * exploreSpeed;
        }

    }

    // Called when player transitions from a room into a hallway
    private void enterHallway(DungeonRoom currentRoom, DungeonRoom room, float roomSize, float hallwaySize)
    {
        Debug.Log(currentRoom.getId());
        Debug.Log(room.getId());

        // Check to see which room contains the hallway

        Debug.Log(parent.transform.Find("" + currentRoom.getId()).Find(currentRoom.getId() + "-" + room.getId()));
        Debug.Log(parent.transform.Find("" + currentRoom.getId()).Find(room.getId() + "-" + currentRoom.getId()));
        Debug.Log(parent.transform.Find("" + room.getId()).Find(currentRoom.getId() + "-" + room.getId()));
        Debug.Log(parent.transform.Find("" + room.getId()).Find(room.getId() + "-" + currentRoom.getId()));

        // IMPLEMENT HALLWAY RNG ONCE WE DETERMINE IF THE ROOM DETERMINES THE RNG OR THE SPECFIC HALLWAY
        int hallwayIndex = -1;
        GameObject roomObject = null;
        if (parent.transform.Find("" + currentRoom.getId()).Find(currentRoom.getId() + "-" + room.getId()) != null)
        {
            hallwayIndex = currentRoom.hasConnection(room);
            roomObject = parent.transform.Find("" + currentRoom.getId()).gameObject;
        } else if (parent.transform.Find("" + currentRoom.getId()).Find(room.getId() + "-" + currentRoom.getId()) != null)
        {
            hallwayIndex = currentRoom.hasConnection(room);
            roomObject = parent.transform.Find("" + currentRoom.getId()).gameObject;
        } else if (parent.transform.Find("" + room.getId()).Find(currentRoom.getId() + "-" + room.getId())) // This should be one of the one to run
        {
            hallwayIndex = room.hasConnection(currentRoom);
            roomObject = parent.transform.Find("" + room.getId()).gameObject;
        } else if (parent.transform.Find("" + room.getId()).Find(room.getId() + "-" + currentRoom.getId()) != null)
        {
            hallwayIndex = room.hasConnection(currentRoom);
            roomObject = parent.transform.Find("" + room.getId()).gameObject;
        } else
        {
            Debug.Log("THERE WAS AN ERROR SOMEWHERE, NO DUNGEON CONNECTION PRESENT");
            Application.Quit();
        }

        // Get the map position transform object
        Transform mapPosition = gameObject.transform.Find("Canvas").Find("Map Position");

        // Determine which direction the map position needs to offset to center the player position
        switch (currentRoom.getConnections()[currentRoom.hasConnection(room)].getNextDirection())
        {
            // Direction you go in for each room. 0 = left, 1 = up, 2 = right, 3 = down
            case 0:
                mapPosition.localPosition = new Vector3(mapPosition.localPosition.x, mapPosition.localPosition.y, 0) + new Vector3(roomSize / 2.0f + hallwaySize / 2.0f, 0, 0);
                break;
            case 1:
                mapPosition.localPosition = new Vector3(mapPosition.localPosition.x, mapPosition.localPosition.y, 0) + new Vector3(0, roomSize / 2.0f + hallwaySize / 2.0f, 0);
                break;
            case 2:
                mapPosition.localPosition = new Vector3(mapPosition.localPosition.x, mapPosition.localPosition.y, 0) + new Vector3(roomSize / -2.0f + hallwaySize / -2.0f, 0, 0);
                break;
            case 3:
                mapPosition.localPosition = new Vector3(mapPosition.localPosition.x, mapPosition.localPosition.y, 0) + new Vector3(0, roomSize / -2.0f + hallwaySize / -2.0f, 0);
                break;

            default:
                break;
        }

        // Scale the cursor down to match the hallways size
        gameObject.transform.Find("Canvas").Find("Cursor").GetComponent<RectTransform>().sizeDelta = new Vector2(hallwaySize, hallwaySize);

        // Notify that we are exploring the hallway to start checking for WASD input
        exploreHallway = true;
        
        // Create the background for the main hallway
        GameObject hallway = new GameObject();
        hallway.name = "Hallway";
        hallway.transform.SetParent(gameObject.transform.parent);
        Image image = hallway.AddComponent<Image>();
        image.sprite = hallwaySprite;
        hallway.transform.localPosition = new Vector3(0, 200, 0);
        hallway.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, Screen.height);

        // Create the left exit of the hallway
        GameObject leftDoor = new GameObject();
        leftDoor.name = "Left Door";
        leftDoor.transform.SetParent(hallway.transform);
        Image leftDoorImage = leftDoor.AddComponent<Image>();
        leftDoorImage.sprite = leftDoorSprite;
        leftDoor.transform.localPosition = new Vector3(0, 0, 0);
        leftDoor.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width / 5 , Screen.height);

        // Create the right exit of the hallway
        GameObject rightDoor = new GameObject();
        rightDoor.name = "Right Door";
        rightDoor.transform.SetParent(hallway.transform);
        Image rightDoorImage = rightDoor.AddComponent<Image>();
        rightDoorImage.sprite = rightDoorSprite;
        rightDoor.transform.localPosition = new Vector3((Screen.width * 6) - (Screen.width / 5), 0, 0);
        rightDoor.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width / 5, Screen.height);

        hallway.transform.SetAsFirstSibling();
    }

    // Calls when player transitions from a hallway into a room
    private void enterRoom(DungeonRoom roomToEnter, DungeonRoom previousRoom, float hallwaySize)
    {

        // PROBABLY MORE OR LESS THE SAME AS THE PREVIOUS FUNCTION


        // Invoke the delegate for the other classes
        onRoomEnter?.Invoke(roomToEnter);
    }
}
