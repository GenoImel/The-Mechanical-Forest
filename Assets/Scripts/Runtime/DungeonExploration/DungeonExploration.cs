using UnityEngine;
using UnityEngine.UI;

namespace Akashic.Runtime.DungeonExploration
{
    internal sealed class DungeonExploration : MonoBehaviour
    {
        //TODO: Do some extreme refactoring and break up the responsibility of this class
        private GameObject parent;
        private bool exploreHallway;
        private float exploreSpeed = 500f;

        // Delegates for when a room is clicked
        public delegate void OnRoomEnter(DungeonRoom room);
        public static event OnRoomEnter onRoomEnter;

        private DungeonRoom prevRoom;
        private DungeonRoom nextRoom;
        private int hallwayIndex = -1;

        // Sprites to load hallway sprites
        private Sprite hallwaySprite;
        private Sprite leftDoorSprite;
        private Sprite rightDoorSprite;
        private Sprite playerSprite;

        // Parameters to change as needed
        static float hallwayWidthPerSegment = Screen.width;
        static float hallwayHeight = Screen.height;
        static float doorWidth = Screen.width / 5;
        static float doorHeight = Screen.height;
        static float partyWidth = Screen.width / 10;
        static float partyHeight = Screen.height / 2;

        private Vector3 position;
        private GameObject player;
        private Transform cursor;
        private GameObject hallway;
        private float roomSize;
        private float hallwaySize;
        private int prevSegment;
        private DoublyLinkedList currentSegmentList;
        private int listGoForward;
        
        private void OnEnable()
        {
            AddListeners();
        }

        private void OnDisable()
        {
            RemoveListeners();
        }
        
        void Start()
        {
            // Get the parent of the rooms
            parent = gameObject.transform.GetChild(0).GetChild(0).gameObject;

            // Load the sprites
            hallwaySprite = Resources.Load<Sprite>("Dungeon Exploration/dungeon_hallway");
            leftDoorSprite = Resources.Load<Sprite>("Dungeon Exploration/dungeon_hallway_door_left");
            rightDoorSprite = Resources.Load<Sprite>("Dungeon Exploration/dungeon_hallway_door_right");
            playerSprite = Resources.Load<Sprite>("Dungeon Exploration/hungeon_hallway_player");
        }
        
        void Update()
        {
            if (exploreHallway)
            {
                // Defaults to reading WASD and arrow keys
                float translation = Input.GetAxis("Horizontal");
                translation *= Time.deltaTime;

                // Get the hallway position transform object
                Transform hallwayPosition = gameObject.transform.parent.Find("Hallway");

                // Scroll the screen without the player's position moving
                if (position.x >= 0 && position.x <= (hallwayWidthPerSegment * (prevRoom.HallwayLength(hallwayIndex) - 1.5)))
                {
                    // Offset by distance player traveled
                    hallwayPosition.localPosition = new Vector3(hallwayPosition.localPosition.x - (translation * exploreSpeed), hallwayPosition.localPosition.y, 0);
                }

                // Move the hallwayPosition if we have passed a hallwaysegment
                position.x += translation * exploreSpeed;
                player.transform.localPosition = position;

                // Move the cursor to match which segment we are at
                int currentSegment = (int) ((position.x + doorWidth + partyWidth/2.0f) / (hallwayWidthPerSegment));

                // Get the map position
                Transform mapPosition = gameObject.transform.Find("Canvas").Find("Map Position");

                // Move the mini map cursor to reflect change in segment
                if (currentSegment > prevSegment)
                {
                    if (currentSegment != prevRoom.HallwayLength(hallwayIndex))
                    {
                        // Update the previous segment
                        prevSegment = currentSegment;

                        if (listGoForward == 1)
                        {
                            // Offset normally to the next direction
                            switch (currentSegmentList.getNextDirection())
                            {
                                case 0: mapPosition.localPosition += new Vector3(hallwaySize, 0, 0); break;
                                case 1: mapPosition.localPosition -= new Vector3(0, hallwaySize, 0); break;
                                case 2: mapPosition.localPosition -= new Vector3(hallwaySize, 0, 0); break;
                                case 3: mapPosition.localPosition += new Vector3(0, hallwaySize, 0); break;
                            }

                            // Go to the next correct segment
                            currentSegmentList = currentSegmentList.getNext();
                        } else
                        {
                            // Offset inversely to the next direction
                            switch (currentSegmentList.getNextDirection())
                            {
                                case 0: mapPosition.localPosition -= new Vector3(hallwaySize, 0, 0); break;
                                case 1: mapPosition.localPosition += new Vector3(0, hallwaySize, 0); break;
                                case 2: mapPosition.localPosition += new Vector3(hallwaySize, 0, 0); break;
                                case 3: mapPosition.localPosition -= new Vector3(0, hallwaySize, 0); break;
                            }

                            // Go to the next correct segment
                            currentSegmentList = currentSegmentList.getPrev();
                        }
                    }
                } else if (currentSegment < prevSegment)
                {
                    if (currentSegment != -1)
                    {
                        // Update the previous segment
                        prevSegment = currentSegment;

                        if (listGoForward == 1)
                        {
                            // Go the the previous correct segment
                            currentSegmentList = currentSegmentList.getPrev();

                            // Offset inversely to undo the next direction
                            switch (currentSegmentList.getNextDirection())
                            {
                                case 0: mapPosition.localPosition -= new Vector3(hallwaySize, 0, 0); break;
                                case 1: mapPosition.localPosition += new Vector3(0, hallwaySize, 0); break;
                                case 2: mapPosition.localPosition += new Vector3(hallwaySize, 0, 0); break;
                                case 3: mapPosition.localPosition -= new Vector3(0, hallwaySize, 0); break;
                            }

                        }
                        else
                        {
                            // Go to the previous correct segment
                            currentSegmentList = currentSegmentList.getNext();

                            // Offset correctly to undo the next direction
                            switch (currentSegmentList.getNextDirection())
                            {
                                case 0: mapPosition.localPosition += new Vector3(hallwaySize, 0, 0); break;
                                case 1: mapPosition.localPosition -= new Vector3(0, hallwaySize, 0); break;
                                case 2: mapPosition.localPosition -= new Vector3(hallwaySize, 0, 0); break;
                                case 3: mapPosition.localPosition += new Vector3(0, hallwaySize, 0); break;
                            }
                        }
                    }
                }


                // Defaults to WASD and arrow keys
                float enter = Input.GetAxis("Vertical");

                // If the player is at the edge of the screen cancel out the movement
                if (position.x < -Screen.width / 2.0f + partyWidth / 2.0f)
                {
                    position.x = -Screen.width / 2.0f + partyWidth / 2.0f;
                    player.transform.localPosition = position;
                } else if (position.x > (hallwayWidthPerSegment * (prevRoom.HallwayLength(hallwayIndex) - 1.5)) + (Screen.width / 2.0) - (partyWidth / 2.0)) {
                    position.x = (hallwayWidthPerSegment * (prevRoom.HallwayLength(hallwayIndex) - 1.5f)) + (Screen.width / 2.0f) - (partyWidth / 2.0f);
                    player.transform.localPosition = position;
                }

                // If the party's center is touching the door, potentially go in
                if (position.x >= -Screen.width / 2.0f && position.x <= (-Screen.width / 2) + doorWidth)
                {
                    if (enter >= 0.5f)
                    {
                        enterRoom(prevRoom);
                    }
                } else if (position.x <= (hallwayWidthPerSegment * (prevRoom.HallwayLength(hallwayIndex) - 0.5)) + (Screen.width / 2.0) && position.x >= (hallwayWidthPerSegment * (prevRoom.HallwayLength(hallwayIndex))) - doorWidth)
                {
                    if (enter >= 0.5f)
                    {
                        enterRoom(nextRoom);
                    }
                }
            }

        }

        // Called when player transistions from a hallway into a room
        private void enterRoom(DungeonRoom roomToEnter)
        {
            // Stop allowing exploration input
            exploreHallway = false;

            // Resubscribe to event for entering hallway
            MapLoader.onRoomClick += enterHallway;

            // Remove the hallway game object
            Destroy(hallway);

            // Get the map position
            Transform mapPosition = gameObject.transform.Find("Canvas").Find("Map Position");
            Vector3 offset = new Vector3();

            if (listGoForward == 1)
            {
                int count = 0;
                while (!currentSegmentList.isNextRoom())
                {
                    count++;
                    currentSegmentList = currentSegmentList.getNext();
                }
            } else
            {
                int count = 0;
                while (!currentSegmentList.isPrevRoom())
                {
                    count++;
                    currentSegmentList = currentSegmentList.getPrev();
                }
            }

            // Determine which direction the map position needs to offset to center the player position
            switch (currentSegmentList.getNextDirection())
            {
                // Direction you go in for each room. 0 = left, 1 = up, 2 = right, 3 = down
                case 0:
                    offset =  new Vector3(roomSize / 2.0f + hallwaySize / 2.0f, 0, 0) * listGoForward;
                    break;
                case 1:
                    offset = new Vector3(0, roomSize / -1.0f + hallwaySize / 2.0f, 0) * listGoForward;
                    break;
                case 2:
                    offset = new Vector3(roomSize / -2.0f + hallwaySize / -2.0f, 0, 0) * listGoForward;
                    break;
                case 3:
                    offset = new Vector3(0, roomSize / 1.0f + hallwaySize / -2.0f, 0) * listGoForward;
                    break;

                default:
                    break;
            }

            // Offset the map to enter the room
            if (roomToEnter == prevRoom)
            {
                mapPosition.localPosition = new Vector3(mapPosition.localPosition.x, mapPosition.localPosition.y, 0) - offset;
            } else if (roomToEnter == nextRoom)
            {
                mapPosition.localPosition = new Vector3(mapPosition.localPosition.x, mapPosition.localPosition.y, 0) + offset;
            } else
            {
                Debug.Log("ERROR, ENTERING ROOM OUT OF SCOPE");
            }

            // Rescale the cursor for the room size
            cursor.GetComponent<RectTransform>().sizeDelta = new Vector2(roomSize, roomSize);

            // Tell other scripts we are ready to enter the room
            onRoomEnter?.Invoke(roomToEnter);
        }

        // Called when player transitions from a room into a hallway
        private void enterHallway(DungeonRoom currentRoom, DungeonRoom room, float roomSize, float hallwaySize)
        {
            // Unsubscribe to the event
            MapLoader.onRoomClick -= enterHallway;

            // Store the room we came from and the room we are heading to
            prevRoom = currentRoom;
            nextRoom = room;

            // We are at the original segment
            prevSegment = 0;

            // Store the room and hallway minimap size
            this.roomSize = roomSize;
            this.hallwaySize = hallwaySize;

            // Check to see which room contains the hallway
            // IMPLEMENT HALLWAY RNG ONCE WE DETERMINE IF THE ROOM DETERMINES THE RNG OR THE SPECFIC HALLWAY
            GameObject roomObject = null;
            if (parent.transform.Find("" + currentRoom.GetId()).Find(currentRoom.GetId() + "-" + room.GetId()) != null)
            {
                hallwayIndex = currentRoom.HasConnection(room);
                roomObject = parent.transform.Find("" + currentRoom.GetId()).gameObject;
            } else if (parent.transform.Find("" + currentRoom.GetId()).Find(room.GetId() + "-" + currentRoom.GetId()) != null)
            {
                hallwayIndex = currentRoom.HasConnection(room);
                roomObject = parent.transform.Find("" + currentRoom.GetId()).gameObject;
            } else if (parent.transform.Find("" + room.GetId()).Find(currentRoom.GetId() + "-" + room.GetId())) // This should be one of the one to run
            {
                hallwayIndex = currentRoom.HasConnection(room);
                roomObject = parent.transform.Find("" + room.GetId()).gameObject;
            } else if (parent.transform.Find("" + room.GetId()).Find(room.GetId() + "-" + currentRoom.GetId()) != null)
            {
                hallwayIndex = room.HasConnection(currentRoom);
                roomObject = parent.transform.Find("" + room.GetId()).gameObject;
            } else
            {
                Debug.Log("THERE WAS AN ERROR SOMEWHERE, NO DUNGEON CONNECTION PRESENT");
                Application.Quit();
            }

            // Get the map position transform object
            Transform mapPosition = gameObject.transform.Find("Canvas").Find("Map Position");

            // Get the list to determine which direction is needed to move in for each segment
            currentSegmentList = currentRoom.GetConnections()[hallwayIndex];

            // Get the list to be centered on our current room
            if (currentSegmentList.isNextRoom() && currentSegmentList.getNext().getRoom().GetId() == currentRoom.GetId())
            {
                currentSegmentList = currentSegmentList.getNext();
                listGoForward = 1;
            }
            else if (currentSegmentList.isPrevRoom() && currentSegmentList.getPrev().getRoom().GetId() == currentRoom.GetId()) // This should be the only one besides an instantly exited else while loop
            {
                currentSegmentList = currentSegmentList.getPrev();
                listGoForward = -1;
            }
            else
            {
                while (currentSegmentList.getRoom() == null || currentSegmentList.getRoom().GetId() != currentRoom.GetId())
                {
                    currentSegmentList = currentSegmentList.getNext();
                }

                // Determine if we need to 'backtrack' to get to the destination
                if (currentSegmentList.isNextRoom())
                {
                    listGoForward = -1;
                } else
                {
                    listGoForward = 1;
                }
            }

            // Determine which direction the map position needs to offset to center the player position
            switch (currentSegmentList.getNextDirection())
            {
                // Direction you go in for each room. 0 = left, 1 = up, 2 = right, 3 = down
                case 0:
                    mapPosition.localPosition = new Vector3(
                        mapPosition.localPosition.x, 
                        mapPosition.localPosition.y, 
                        0) + (new Vector3(roomSize / 2.0f + hallwaySize / 2.0f, 0, 0) * listGoForward
                        );
                    break;
                case 1:
                    mapPosition.localPosition = new Vector3(
                        mapPosition.localPosition.x, 
                        mapPosition.localPosition.y, 
                        0) + (new Vector3(0, roomSize / -1.0f + hallwaySize / 2.0f, 0) * listGoForward
                        );
                    break;
                case 2:
                    mapPosition.localPosition = new Vector3(
                        mapPosition.localPosition.x, 
                        mapPosition.localPosition.y,
                        0) + (new Vector3(roomSize / -2.0f + hallwaySize / -2.0f, 0, 0) * listGoForward
                        );
                    break;
                case 3:
                    mapPosition.localPosition = new Vector3(
                        mapPosition.localPosition.x, 
                        mapPosition.localPosition.y, 
                        0) + (new Vector3(0, roomSize / 1.0f + hallwaySize / -2.0f, 0) * listGoForward
                        );
                    break;
            }

            // Enter the first hallway segment
            if (listGoForward == 1) {
                currentSegmentList = currentSegmentList.getNext();
            } 
            else
            {
                currentSegmentList = currentSegmentList.getPrev();
            }

            // Scale the cursor down to match the hallways size
            cursor = gameObject.transform.Find("Canvas").Find("Cursor");
            cursor.GetComponent<RectTransform>().sizeDelta = new Vector2(hallwaySize, hallwaySize);
        
            // Create the background for the main hallway
            hallway = new GameObject();
            hallway.name = "Hallway";
            hallway.transform.SetParent(gameObject.transform.parent);
            Image image = hallway.AddComponent<Image>();
            image.sprite = hallwaySprite;
            hallway.transform.localPosition = new Vector3(0, 0, 0);
            hallway.GetComponent<RectTransform>().sizeDelta = new Vector2(hallwayWidthPerSegment, hallwayHeight);

            // Create the left exit of the hallway
            GameObject leftDoor = new GameObject();
            leftDoor.name = "Left Door";
            leftDoor.transform.SetParent(hallway.transform);
            Image leftDoorImage = leftDoor.AddComponent<Image>();
            leftDoorImage.GetComponent<Image>().color = new Color32(0, 255, 0, 100);
            leftDoorImage.sprite = leftDoorSprite;
            leftDoor.transform.localPosition = new Vector3((-Screen.width / 2) + (doorWidth / 2), 0, 0);
            leftDoor.GetComponent<RectTransform>().sizeDelta = new Vector2(doorWidth, doorHeight);

            // Create the right exit of the hallway
            GameObject rightDoor = new GameObject();
            rightDoor.name = "Right Door";
            rightDoor.transform.SetParent(hallway.transform);
            Image rightDoorImage = rightDoor.AddComponent<Image>();
            rightDoorImage.GetComponent<Image>().color = new Color32(0, 0, 255, 100);
            rightDoorImage.sprite = rightDoorSprite;
            rightDoor.transform.localPosition = new Vector3((hallwayWidthPerSegment * (currentRoom.HallwayLength(hallwayIndex))) - (doorWidth / 2), 0, 0);
            rightDoor.GetComponent<RectTransform>().sizeDelta = new Vector2(doorWidth, doorHeight);

            // Create the player
            player = new GameObject();
            player.name = "Player";
            player.transform.SetParent(hallway.transform);
            Image playerImage = player.AddComponent<Image>();
            playerImage.GetComponent<Image>().color = new Color32(255, 0, 0, 255);
            playerImage.sprite = playerSprite;
            player.transform.localPosition = position;
            player.GetComponent<RectTransform>().sizeDelta = new Vector2(partyWidth, partyHeight);

            hallway.transform.SetAsFirstSibling();

            // Reset the player position
            position = new Vector3((-Screen.width / 2) + (doorWidth / 2), 0, 0);

            // Notify that we are exploring the hallway to start checking for WASD input
            exploreHallway = true;
        }
        
        private void AddListeners()
        {
            MapLoader.onRoomClick += enterHallway;
        }
        
        private void RemoveListeners()
        {
            MapLoader.onRoomClick -= enterHallway;
        }
    }
}
