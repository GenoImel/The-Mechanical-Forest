using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonExploration : MonoBehaviour
{
    GameObject parent;
    bool exploreHallway = false;
    float exploreSpeed;
    float position;


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

    }

    // Update is called once per frame
    void Update()
    {
        if (exploreHallway)
        {
            // Defaults to reading WASD and arrow keys
            float translation = Input.GetAxis("Horizontal");
            translation *= Time.deltaTime;

            position += translation;
        }

    }

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

        Transform mapPosition = gameObject.transform.Find("Canvas").Find("Map Position");
        Transform cursor = gameObject.transform.Find("Canvas").Find("Cursor");

        if (mapPosition == null)
        {
            Debug.Log("MAP POSITION NOT FOUND");
            Application.Quit();
        }

        switch (currentRoom.getConnections().Item2[currentRoom.hasConnection(room)])
        {
            // Direction you go in for each room. 0 = left, 1 = up, 2 = right, 3 = down, 4 up-left, 5 up-right, 6 down-right, 7 down-left, 8 left-up, 9 left-down, 10 right-up, 11 right-down
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
            case 4:
                break;
            case 5:
                break;
            case 6:
                break;
            case 7:
                break;
            case 8:
                break;
            case 9:
                break;
            case 10:
                break;
            case 11:
                break;

            default:
                break;
        }

        cursor.GetComponent<RectTransform>().sizeDelta = new Vector2(hallwaySize, hallwaySize);

        exploreHallway = true;
    }

    private void enterRoom(DungeonRoom roomToEnter, DungeonRoom previousRoom, float hallwaySize)
    {

    }
}
