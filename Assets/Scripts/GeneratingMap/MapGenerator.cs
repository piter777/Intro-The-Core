using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;
using itc;
namespace itc
{

    public class MapGenerator : MonoBehaviour
    {

        private static readonly Vector2Int[] Neighbours = new[] {
            new Vector2Int(0, -1),
            new Vector2Int(1, 0),
            new Vector2Int(0, 1),
            new Vector2Int(-1, 0),
        };

        // Matrix length where we do all other operations.
        private static int gridRange = 43;
        // Distanse in unity editor between rooms.
        public float steepDistanse = 70;
        // Number of rooms we spawn from starting to last.
        public int numberOfRoomsToSpawn = 10;
        // Posible extra random range from -X to +X multiplay by 10.
        public int randomRangeRoom = 1;

        // Arrays of prefabs we use to create levels (Rooms,Tunells).
        public GameObject[] roomPrefab;
        public GameObject[] tunnellsPrefabs;
        public GameObject[] fogPrefab;
        public bool fogofWarshither;

        // A list with all spawned romms.
        private List<GameObject> roomList = new List<GameObject>();
        // A Multi array where first number its  id of room from list and second number its information about it, where 0-Romm and 1-4 Exits where 1-up  2-right 3-down 4-left.
        private GameObject[,] exitRoomsArray = new GameObject[gridRange, 5];
        // A map of grid where first variable its X, second variable its Y, and third variable its ID of room on thiscoordinates and 0 if empty.
        private int[,,] gridMap = new int[gridRange, gridRange, 1];

        // A same as grid map but much simple where first variable its room id and second its X and Y.
        private int[,] exitRoomsGrid = new int[gridRange, 2];
        // How matchabowe rooms spawn right now
        private int numberRoomsNow;
        // Where is old room was spawned to new 0-up 1-right 2-down 3-left.
        private int newRoomDestination;
        // Where the X and Y in array mast be for spawn a new room,start from center of array.
        private int gridNowX = gridRange / 2;
        private int gridNowY = gridRange / 2;
        // Where the X and Y incoordinates mast be for spawn a room if 50.50 its center.
        private float steepX = 50f;
        private float steepY = 50f;
        //A variable to store a reference to the transform of our Board object.
        private Transform boardHolder;
        private Transform fogHolder;
        // A sting that help show many debug information console only.
        private string debugString;


        // Use this for initialization
        void Start()
        {
            // Set a player starting health.
            DataHolder.PlayerHealth = 3;
            // Set a Holder for all spawned prefabs in future.
            //	boardHolder = new GameObject ("Board").transform;
            boardHolder = GameObject.FindGameObjectWithTag("Dynamic").transform;
            fogHolder = GameObject.Find("Fog").transform;

            //	boardHolder.SetParent (GameObject.FindGameObjectWithTag("Dynamic"));
            // Call a fucntion for spawn a s first room.
            SpawnFirstRoom();
            // Call a main method that generate most all.
            GeneratorOfRooms();
            // Call a method that spawn a extra room like (loot,secret ets.) on any awalible door;
            //	SpawnRandomPlaceToRoom ( roomPrefab [0] );
            SpawnRandomPlaceToRoom(roomPrefab[2]);
            SpawnRandomPlaceToRoom(roomPrefab[2]);
            SpawnBossRoom(roomPrefab[3], roomPrefab[1]);
            // method that hold a all exit prefabs for bilding doors and tunennls betwen.
            FindAllExits();
            // method thas close all nonusable doors.
            SpawnClosedDoors();
            // Call a last cllas that build tunennls between rooms and finish generating.
            BuildTunnels();
            if (fogofWarshither == true) SpawnRoomFogOfWar();
            Destroy(this.gameObject);

            // A debug algoritm for see all grid map
            //	for (int i = 0; i < gridRange; i++) {debugString = debugString + System.Environment.NewLine;for (int j = 0; j < gridRange; j++) {debugString = (debugString + gridMap [j, i, 0] + " ");}}Debug.Log (debugString );

        }

        //A method that spawn a first room 
        void SpawnFirstRoom()
        {
            // A first prefab that we use to span.
            GameObject toInstantiate = roomPrefab[0];
            GameObject instance = Instantiate(toInstantiate, new Vector3(steepX, 0f, steepY), Quaternion.identity) as GameObject;
            roomList.Add(instance); numberRoomsNow++;
            // Help hold hierarchy clean
            instance.transform.SetParent(boardHolder);
            // Fill map with new point a first room
            gridMap[gridNowX, gridNowY, 0] = 1;
            exitRoomsGrid[roomList.Count - 1, 0] = gridNowX;
            exitRoomsGrid[roomList.Count - 1, 1] = gridNowY;
            // Randomize a new plase to spawn
            int paternRandomiser = Random.Range(0, 4);
            switch (paternRandomiser)
            {
                case 0:
                    steepY += steepDistanse;
                    gridNowY--;
                    newRoomDestination = 0;
                    break;
                case 1:
                    steepY -= steepDistanse;
                    gridNowY++;
                    newRoomDestination = 2;
                    break;
                case 2:
                    steepX += steepDistanse;
                    gridNowX++;
                    newRoomDestination = 1;
                    break;
                case 3:
                    steepX -= steepDistanse;
                    gridNowX--;
                    newRoomDestination = 3;
                    break;

                default: break;
            }
        }


        // Return all avalible exits from prefab to int[].
        int[] RoomExits(int curentExitNumber)
        {
            int[] exitPoints = roomPrefab[curentExitNumber].GetComponent<StatasOfRoom>().thisRoomStats;
            return exitPoints;
        }


        // Look if next coordinate if free.
        bool[] HasDoorAndFreeSpace(int[] currentRoomExits)
        {
            bool[] result = new bool[Neighbours.Length];

            for (var i = 0; i < Neighbours.Length; i++)
            {
                var neighbour = Neighbours[i];
                result[i] = currentRoomExits[i] == 1 && gridMap[gridNowX + neighbour.x, gridNowY + neighbour.y, 0] == 0;
            }

            return result;
        }


        // Main method of all script that spawn a rooms.
        void GeneratorOfRooms()
        {
            // A way out from do while.
            int breakKode = 0;
            // for every room we need to spawn this for create 1 room for every iteration.
            for (int i = 0; i < numberOfRoomsToSpawn; i++)
            {
                // A variable to hold a random nubmer.
                int NumberOfPrefab;
                // We use this to spawn a prefabs.
                GameObject toInstantiate;
                // In every iteration we make 0 exit code.
                breakKode = 0;
                // one more loop breaking varible for sure.
                int loopBreking = 0;


                // Start of inner cirle or random spawing algoritm
                do
                {
                    // First pick a random room from 3(0,1,2 speciall rooms) to all prefabs range.
                    NumberOfPrefab = Random.Range(4, roomPrefab.Length);
                    // If its last room its pick a Boss room.
                    //	if(i == numberOfRoomsToSpawn-1) NumberOfPrefab = 1;
                    // Picked room is memorised.
                    toInstantiate = roomPrefab[NumberOfPrefab];
                    // Find all posible Exits for this room
                    int[] thisDoors = RoomExits(NumberOfPrefab);
                    // Counting for all posible exits
                    int TrueCount = 0;
                    // If old was from Down and new room have doors to up then truecoun++ else old room anywere else truecoun++.
                    if (newRoomDestination == 0)
                    {
                        if (thisDoors[2] == 1) TrueCount++;
                    }
                    else TrueCount++;

                    if (newRoomDestination == 1)
                    {
                        if (thisDoors[3] == 1) TrueCount++;
                    }
                    else TrueCount++;

                    if (newRoomDestination == 2)
                    {
                        if (thisDoors[0] == 1) TrueCount++;
                    }
                    else TrueCount++;

                    if (newRoomDestination == 3)
                    {
                        if (thisDoors[1] == 1) TrueCount++;
                    }
                    else TrueCount++;

                    // When we have doors from old room we breat this line as this is good prefab.
                    if (TrueCount == 4) { breakKode = 1; }
                    // If we cant fint a need room more than 100 times we breall loop.
                    loopBreking++;
                    if (loopBreking > 100)
                    {
                        breakKode = 1; Debug.Log("Help I cant FIND ROOM");
                    }
                    // Break code was 1 when we find a perfect prefab;
                } while (breakKode == 0);

                // Spawn a Room whas was chosen early oncoordinates +- RandomRangeRoom*10 for bot linear locking dungeon.
                GameObject instance = Instantiate(toInstantiate, new Vector3(steepX + Random.Range(-randomRangeRoom, randomRangeRoom) * 10, 0f, steepY + Random.Range(-randomRangeRoom, randomRangeRoom) * 10), Quaternion.identity) as GameObject;
                roomList.Add(instance);
                // Say we add a new room and remember this room on map.
                numberRoomsNow++;
                exitRoomsGrid[roomList.Count - 1, 0] = gridNowX;
                exitRoomsGrid[roomList.Count - 1, 1] = gridNowY;
                gridMap[gridNowX, gridNowY, 0] = NumberOfPrefab;
                // Hirerarhy sorthing helper. (console only).
                instance.transform.SetParent(boardHolder);
                // Then we try to find a best place to spawn a new room.
                FindPlaceToNextRoom(NumberOfPrefab);
                // On this moment we create 1 more room.
            }
            // On this moment we create all rooms.
        }


        void FindPlaceToNextRoom(int numberOfPrefab)
        {

            // Get exits of room.
            int[] roomStatsExits = roomPrefab[numberOfPrefab].GetComponent<StatasOfRoom>().thisRoomStats;
            // Create local varible for see if next doors is free
            bool[] nextInt = HasDoorAndFreeSpace(roomStatsExits);
            int breakCode = 0;
            // Create a loop that must find a random free spot to spawn.
            do
            {
                // Create a random side;
                int RandomSide = Random.Range(0, 4);

                // If there is no possible exits
                if (!nextInt[0] && !nextInt[1] && !nextInt[2] && !nextInt[3])
                {
                    Debug.Log("Plase to spawn is ended best posible plase to spawn is= ");
                    // Call a method that find a newcoordinates for spawn a room froom any other free room.
                    int[] XY = ReturningXY();
                    //Remember thiscoordinates
                    gridNowY = XY[1];
                    gridNowX = XY[0];
                    //Finish loop
                    breakCode++;
                }

                else
                    // If we have a posible room to spawn.
                    if (nextInt[RandomSide])
                {
                    // We remember where is old room spawned and makecoordinates for array and unity moving for new position to spawn.
                    if (0 == RandomSide)
                    {
                        steepY += steepDistanse;
                        gridNowY--;
                        breakCode++;
                        newRoomDestination = 0;
                    }
                    if (1 == RandomSide)
                    {
                        steepX += steepDistanse;
                        gridNowX++;
                        breakCode++;
                        newRoomDestination = 1;
                    }
                    if (2 == RandomSide)
                    {
                        steepY -= (steepDistanse);
                        gridNowY++; breakCode++;
                        newRoomDestination = 2;
                    }
                    if (3 == RandomSide)
                    {
                        steepX -= (steepDistanse);
                        gridNowX--;
                        breakCode++;
                        newRoomDestination = 3;
                    }
                }
            }
            // We have != when we found a room to spawn or create new if there is no posible to spawn.
            while (breakCode == 0);
        }


        // A method that return a X and Y of any epmty room and movecoordinates to this position.
        int[] ReturningXY()
        {
            // This we return.
            int[] returning = new int[2];
            // Loop breaking limits.
            int breakLimit = 0;
            bool breakKode = false;
            do
            {
                // Random pick any already spawned room
                int curentRoom = Random.Range(0, roomList.Count - 1);
                // Random pick any side of room
                int paternRandomiser = Random.Range(0, 4);
                // The distanse incoordinates between starting room and new room
                int xStepcoordinates = exitRoomsGrid[curentRoom, 0] - exitRoomsGrid[0, 0];
                int yStepcoordinates = exitRoomsGrid[curentRoom, 1] - exitRoomsGrid[0, 1];
                switch (paternRandomiser)
                {
                    // if we pick up side.
                    case 0:
                        // If new room is empty and new room have doors UP then.
                        if (gridMap[exitRoomsGrid[curentRoom, 0], exitRoomsGrid[curentRoom, 1] - 1, 0] == 0 && RoomExits(gridMap[exitRoomsGrid[curentRoom, 0], exitRoomsGrid[curentRoom, 1], 0])[0] == 1)
                        {
                            // We remember save values to return for new room.
                            returning[0] = exitRoomsGrid[curentRoom, 0];
                            returning[1] = exitRoomsGrid[curentRoom, 1] - 1;
                            // then we break loop and remeber that is old room was up.
                            breakKode = true;
                            newRoomDestination = 0;
                            // And for last we changecoordinates for new room where is was spawned.
                            steepX = roomList[0].transform.position.x + steepDistanse * xStepcoordinates;
                            steepY = (roomList[0].transform.position.z + steepDistanse * yStepcoordinates + steepDistanse);
                            if (yStepcoordinates != 0) steepY = (roomList[0].transform.position.z + (steepDistanse * yStepcoordinates * -1) + steepDistanse);
                        }
                        break;
                    // Same for other code different side
                    case 1:
                        if (gridMap[exitRoomsGrid[curentRoom, 0] + 1, exitRoomsGrid[curentRoom, 1], 0] == 0 && RoomExits(gridMap[exitRoomsGrid[curentRoom, 0], exitRoomsGrid[curentRoom, 1], 0])[1] == 1)
                        {
                            returning[0] = exitRoomsGrid[curentRoom, 0] + 1;
                            returning[1] = exitRoomsGrid[curentRoom, 1];
                            breakKode = true;
                            newRoomDestination = 1;
                            steepX = roomList[0].transform.position.x + steepDistanse * (xStepcoordinates) + steepDistanse;
                            steepY = roomList[curentRoom].transform.position.z;
                        }
                        break;
                    case 2:
                        if (gridMap[exitRoomsGrid[curentRoom, 0], exitRoomsGrid[curentRoom, 1] + 1, 0] == 0 && RoomExits(gridMap[exitRoomsGrid[curentRoom, 0], exitRoomsGrid[curentRoom, 1], 0])[2] == 1)
                        {
                            returning[0] = exitRoomsGrid[curentRoom, 0];
                            returning[1] = exitRoomsGrid[curentRoom, 1] + 1;
                            breakKode = true;
                            newRoomDestination = 2;
                            steepX = roomList[0].transform.position.x + steepDistanse * xStepcoordinates;
                            steepY = (roomList[0].transform.position.z + steepDistanse * yStepcoordinates - steepDistanse);
                            if (yStepcoordinates != 0) steepY = (roomList[0].transform.position.z + (steepDistanse * yStepcoordinates * -1) - steepDistanse);
                        }
                        break;
                    case 3:
                        if (gridMap[exitRoomsGrid[curentRoom, 0] - 1, exitRoomsGrid[curentRoom, 1], 0] == 0 && RoomExits(gridMap[exitRoomsGrid[curentRoom, 0], exitRoomsGrid[curentRoom, 1], 0])[3] == 1)
                        {
                            returning[0] = exitRoomsGrid[curentRoom, 0] - 1;
                            returning[1] = exitRoomsGrid[curentRoom, 1];
                            breakKode = true;
                            newRoomDestination = 3;
                            steepX = (roomList[0].transform.position.x + steepDistanse * (xStepcoordinates) - steepDistanse);
                            steepY = roomList[curentRoom].transform.position.z;
                        }
                        break;
                    default: break;
                }

                // IF we cant fint a new room in 100 iteration stop it.
                breakLimit++;
                if (breakLimit > 100) { breakKode = true; Debug.Log("All posible wariations END"); }
            }
            while (breakKode == false);
            // Return a value for new room in grid.
            return (returning);
        }


        // Spawn a random room in any free spase
        void SpawnRandomPlaceToRoom(GameObject TOspawn)
        {
            // We just find any free space in grid that have a close non free coordinates and open room and spawn there is room.
            // ALl of this code you can find in generator this is simple same jast for 1 room.
            int breakLimit = 0;
          //  bool returningWalue = false;
            bool breakKode = false;
            do
            {
                int curentRoom = Random.Range(0, roomList.Count - 1);
                int paternRandomiser = Random.Range(0, 4);
                switch (paternRandomiser)
                {
                    case 0:
                        if (gridMap[exitRoomsGrid[curentRoom, 0], exitRoomsGrid[curentRoom, 1] - 1, 0] == 0 && RoomExits(gridMap[exitRoomsGrid[curentRoom, 0], exitRoomsGrid[curentRoom, 1], 0])[0] == 1 && (TOspawn.GetComponent<StatasOfRoom>().thisRoomStats[2] == 1))
                        {
                            GameObject instance = Instantiate(TOspawn, new Vector3((roomList[curentRoom].transform.position.x), 0f, (roomList[curentRoom].transform.position.z + steepDistanse)), Quaternion.identity) as GameObject;
                            roomList.Add(instance);
                            numberRoomsNow++;
                            instance.transform.SetParent(boardHolder);
                            gridNowX = (exitRoomsGrid[curentRoom, 0]);
                            gridNowY = (exitRoomsGrid[curentRoom, 1] - 1);
                            gridMap[gridNowX, gridNowY, 0] = 1;
                            exitRoomsGrid[roomList.Count - 1, 0] = gridNowX;
                            exitRoomsGrid[roomList.Count - 1, 1] = gridNowY;
                            breakKode = true;
                            //returningWalue = true;
                        }
                        break;
                    case 1:
                        if (gridMap[exitRoomsGrid[curentRoom, 0] + 1, exitRoomsGrid[curentRoom, 1], 0] == 0 && RoomExits(gridMap[exitRoomsGrid[curentRoom, 0], exitRoomsGrid[curentRoom, 1], 0])[1] == 1 && (TOspawn.GetComponent<StatasOfRoom>().thisRoomStats[3] == 1))
                        {
                            GameObject instance = Instantiate(TOspawn, new Vector3((roomList[curentRoom].transform.position.x + steepDistanse), 0f, (roomList[curentRoom].transform.position.z)), Quaternion.identity) as GameObject;
                            roomList.Add(instance);
                            numberRoomsNow++;
                            instance.transform.SetParent(boardHolder);
                            gridNowX = (exitRoomsGrid[curentRoom, 0] + 1);
                            gridNowY = (exitRoomsGrid[curentRoom, 1]);
                            gridMap[gridNowX, gridNowY, 0] = 1;
                            exitRoomsGrid[roomList.Count - 1, 0] = gridNowX;
                            exitRoomsGrid[roomList.Count - 1, 1] = gridNowY;
                            breakKode = true;
                           // returningWalue = true;
                        }
                        break;
                    case 2:
                        if (gridMap[exitRoomsGrid[curentRoom, 0], exitRoomsGrid[curentRoom, 1] + 1, 0] == 0 && RoomExits(gridMap[exitRoomsGrid[curentRoom, 0], exitRoomsGrid[curentRoom, 1], 0])[2] == 1 && (TOspawn.GetComponent<StatasOfRoom>().thisRoomStats[0] == 1))
                        {
                            GameObject instance = Instantiate(TOspawn, new Vector3((roomList[curentRoom].transform.position.x), 0f, (roomList[curentRoom].transform.position.z - steepDistanse)), Quaternion.identity) as GameObject;
                            roomList.Add(instance);
                            numberRoomsNow++;
                            instance.transform.SetParent(boardHolder);
                            gridNowX = (exitRoomsGrid[curentRoom, 0]);
                            gridNowY = (exitRoomsGrid[curentRoom, 1] + 1);
                            gridMap[gridNowX, gridNowY, 0] = 1;
                            exitRoomsGrid[roomList.Count - 1, 0] = gridNowX;
                            exitRoomsGrid[roomList.Count - 1, 1] = gridNowY;
                            breakKode = true;
                          //  returningWalue = true;
                        }
                        break;
                    case 3:
                        if (gridMap[exitRoomsGrid[curentRoom, 0] - 1, exitRoomsGrid[curentRoom, 1], 0] == 0 && RoomExits(gridMap[exitRoomsGrid[curentRoom, 0], exitRoomsGrid[curentRoom, 1], 0])[3] == 1 && (TOspawn.GetComponent<StatasOfRoom>().thisRoomStats[1] == 1))
                        {
                            GameObject instance = Instantiate(TOspawn, new Vector3((roomList[curentRoom].transform.position.x - steepDistanse), 0f, (roomList[curentRoom].transform.position.z)), Quaternion.identity) as GameObject;
                            roomList.Add(instance);
                            numberRoomsNow++;
                            instance.transform.SetParent(boardHolder);
                            gridNowX = (exitRoomsGrid[curentRoom, 0] - 1);
                            gridNowY = (exitRoomsGrid[curentRoom, 1]);
                            gridMap[gridNowX, gridNowY, 0] = 1;
                            exitRoomsGrid[roomList.Count - 1, 0] = gridNowX;
                            exitRoomsGrid[roomList.Count - 1, 1] = gridNowY;
                            breakKode = true;
                         //   returningWalue = true;
                        }
                        break;


                    default: { break; }
                }

                breakLimit++;

                if (breakLimit > 25)
                {
                    breakKode = true;
                  //  returningWalue = false;
                }
            }
            while (breakKode == false);
            // If return value 0 we cant spawn a room.
            // Debug.Log("Room is spawned="+ returningWalue);

        }


        // Spawn a 2X3 boss room
        void SpawnBossRoom(GameObject TOspawn, GameObject TOspawnBoss)
        {
            // We just find any free space in grid that have a close non free coordinates and open room and spawn there is room.
            // ALl of this code you can find in generator this is simple same jast for 1 room.
            int breakLimit = 0;
          //  bool returningWalue = false;
            bool breakKode = false;
            do
            {
                int curentRoom = Random.Range(0, roomList.Count - 1);
                int paternRandomiser = Random.Range(0, 4);
                switch (paternRandomiser)
                {
                    case 0:
                        if (gridMap[exitRoomsGrid[curentRoom, 0], exitRoomsGrid[curentRoom, 1] - 1, 0] == 0 && RoomExits(gridMap[exitRoomsGrid[curentRoom, 0], exitRoomsGrid[curentRoom, 1], 0])[0] == 1 && (TOspawn.GetComponent<StatasOfRoom>().thisRoomStats[2] == 1))
                        {
                            if ((gridMap[exitRoomsGrid[curentRoom, 0] - 1, exitRoomsGrid[curentRoom, 1] - 1, 0] == 0) && (gridMap[exitRoomsGrid[curentRoom, 0] + 1, exitRoomsGrid[curentRoom, 1] - 1, 0]) == 0)
                            {


                                GameObject instance = Instantiate(TOspawn, new Vector3((roomList[curentRoom].transform.position.x), 0f, (roomList[curentRoom].transform.position.z + steepDistanse)), Quaternion.identity) as GameObject;
                                roomList.Add(instance);
                                numberRoomsNow++;
                                instance.transform.SetParent(boardHolder);
                                gridNowX = (exitRoomsGrid[curentRoom, 0]);
                                gridNowY = (exitRoomsGrid[curentRoom, 1] - 1);
                                gridMap[gridNowX, gridNowY, 0] = 1;
                                exitRoomsGrid[roomList.Count - 1, 0] = gridNowX;
                                exitRoomsGrid[roomList.Count - 1, 1] = gridNowY;
                                //   steepDistanse;
                                //spawn boss room
                                GameObject instance1 = Instantiate(TOspawnBoss, new Vector3((roomList[numberRoomsNow - 1].transform.position.x), 0f, (roomList[numberRoomsNow - 1].transform.position.z + steepDistanse + 10)), Quaternion.identity) as GameObject;
                                roomList.Add(instance1);
                                numberRoomsNow++;
                                instance1.transform.SetParent(boardHolder);
                                gridNowX = (exitRoomsGrid[curentRoom, 0]);
                                gridNowY = (exitRoomsGrid[curentRoom, 1] - 2);
                                gridMap[gridNowX, gridNowY, 0] = 1;
                                exitRoomsGrid[roomList.Count - 1, 0] = gridNowX;
                                exitRoomsGrid[roomList.Count - 1, 1] = gridNowY;
                                breakKode = true;
                           //     returningWalue = true;
                            }
                        }
                        break;
                    case 1:
                        if (gridMap[exitRoomsGrid[curentRoom, 0] + 1, exitRoomsGrid[curentRoom, 1], 0] == 0 && RoomExits(gridMap[exitRoomsGrid[curentRoom, 0], exitRoomsGrid[curentRoom, 1], 0])[1] == 1 && (TOspawn.GetComponent<StatasOfRoom>().thisRoomStats[3] == 1))
                        {
                            if ((gridMap[exitRoomsGrid[curentRoom, 0] + 1, exitRoomsGrid[curentRoom, 1] - 1, 0] == 0) && (gridMap[exitRoomsGrid[curentRoom, 0] + 1, exitRoomsGrid[curentRoom, 1] + 1, 0]) == 0)
                            {
                                GameObject instance = Instantiate(TOspawn, new Vector3((roomList[curentRoom].transform.position.x + steepDistanse), 0f, (roomList[curentRoom].transform.position.z)), Quaternion.identity) as GameObject;
                                roomList.Add(instance);
                                numberRoomsNow++;
                                instance.transform.SetParent(boardHolder);
                                gridNowX = (exitRoomsGrid[curentRoom, 0] + 1);
                                gridNowY = (exitRoomsGrid[curentRoom, 1]);
                                gridMap[gridNowX, gridNowY, 0] = 1;
                                exitRoomsGrid[roomList.Count - 1, 0] = gridNowX;
                                exitRoomsGrid[roomList.Count - 1, 1] = gridNowY;
                                //spawn boss room
                                GameObject instance1 = Instantiate(TOspawnBoss, new Vector3((roomList[numberRoomsNow - 1].transform.position.x + (steepDistanse + 10)), 0f, (roomList[numberRoomsNow - 1].transform.position.z)), Quaternion.identity) as GameObject;
                                roomList.Add(instance1);
                                numberRoomsNow++;
                                instance1.transform.SetParent(boardHolder);
                                gridNowX = (exitRoomsGrid[curentRoom, 0] + 2);
                                gridNowY = (exitRoomsGrid[curentRoom, 1]);
                                gridMap[gridNowX, gridNowY, 0] = 1;
                                exitRoomsGrid[roomList.Count - 1, 0] = gridNowX;
                                exitRoomsGrid[roomList.Count - 1, 1] = gridNowY;
                                breakKode = true;
                            //    returningWalue = true;
                            }
                        }
                        break;
                    case 2:
                        if (gridMap[exitRoomsGrid[curentRoom, 0], exitRoomsGrid[curentRoom, 1] + 1, 0] == 0 && RoomExits(gridMap[exitRoomsGrid[curentRoom, 0], exitRoomsGrid[curentRoom, 1], 0])[2] == 1 && (TOspawn.GetComponent<StatasOfRoom>().thisRoomStats[0] == 1))
                        {
                            if ((gridMap[exitRoomsGrid[curentRoom, 0] - 1, exitRoomsGrid[curentRoom, 1] + 1, 0] == 0) && (gridMap[exitRoomsGrid[curentRoom, 0] + 1, exitRoomsGrid[curentRoom, 1] + 1, 0]) == 0)
                            {
                                GameObject instance = Instantiate(TOspawn, new Vector3((roomList[curentRoom].transform.position.x), 0f, (roomList[curentRoom].transform.position.z - steepDistanse)), Quaternion.identity) as GameObject;
                                roomList.Add(instance);
                                numberRoomsNow++;
                                instance.transform.SetParent(boardHolder);
                                gridNowX = (exitRoomsGrid[curentRoom, 0]);
                                gridNowY = (exitRoomsGrid[curentRoom, 1] + 1);
                                gridMap[gridNowX, gridNowY, 0] = 1;
                                exitRoomsGrid[roomList.Count - 1, 0] = gridNowX;
                                exitRoomsGrid[roomList.Count - 1, 1] = gridNowY;
                                //spawn boss room
                                GameObject instance1 = Instantiate(TOspawnBoss, new Vector3((roomList[numberRoomsNow - 1].transform.position.x), 0f, (roomList[numberRoomsNow - 1].transform.position.z - (steepDistanse + 10))), Quaternion.identity) as GameObject;
                                roomList.Add(instance1);
                                numberRoomsNow++;
                                instance1.transform.SetParent(boardHolder);
                                gridNowX = (exitRoomsGrid[curentRoom, 0]);
                                gridNowY = (exitRoomsGrid[curentRoom, 1] + 2);
                                gridMap[gridNowX, gridNowY, 0] = 1;
                                exitRoomsGrid[roomList.Count - 1, 0] = gridNowX;
                                exitRoomsGrid[roomList.Count - 1, 1] = gridNowY;
                                breakKode = true;
                           //     returningWalue = true;
                            }
                        }
                        break;
                    case 3:
                        if (gridMap[exitRoomsGrid[curentRoom, 0] - 1, exitRoomsGrid[curentRoom, 1], 0] == 0 && RoomExits(gridMap[exitRoomsGrid[curentRoom, 0], exitRoomsGrid[curentRoom, 1], 0])[3] == 1 && (TOspawn.GetComponent<StatasOfRoom>().thisRoomStats[1] == 1))
                        {
                            if ((gridMap[exitRoomsGrid[curentRoom, 0] - 1, exitRoomsGrid[curentRoom, 1] - 1, 0] == 0) && (gridMap[exitRoomsGrid[curentRoom, 0] - 1, exitRoomsGrid[curentRoom, 1] + 1, 0]) == 0)
                            {

                                GameObject instance = Instantiate(TOspawn, new Vector3((roomList[curentRoom].transform.position.x - steepDistanse), 0f, (roomList[curentRoom].transform.position.z)), Quaternion.identity) as GameObject;
                                roomList.Add(instance);
                                numberRoomsNow++;
                                instance.transform.SetParent(boardHolder);
                                gridNowX = (exitRoomsGrid[curentRoom, 0] - 1);
                                gridNowY = (exitRoomsGrid[curentRoom, 1]);
                                gridMap[gridNowX, gridNowY, 0] = 1;
                                exitRoomsGrid[roomList.Count - 1, 0] = gridNowX;
                                exitRoomsGrid[roomList.Count - 1, 1] = gridNowY;
                                //spawn boss room
                                GameObject instance1 = Instantiate(TOspawnBoss, new Vector3((roomList[numberRoomsNow - 1].transform.position.x - (steepDistanse + 10)), 0f, (roomList[numberRoomsNow - 1].transform.position.z)), Quaternion.identity) as GameObject;
                                roomList.Add(instance1);
                                numberRoomsNow++;
                                instance1.transform.SetParent(boardHolder);
                                gridNowX = (exitRoomsGrid[curentRoom, 0] - 2);
                                gridNowY = (exitRoomsGrid[curentRoom, 1]);
                                //   Debug.Log((exitRoomsGrid[numberRoomsNow - 1, 0] - 1)+"   "+ (exitRoomsGrid[curentRoom, 0] - 1));
                                gridMap[gridNowX, gridNowY, 0] = 1;
                                exitRoomsGrid[roomList.Count - 1, 0] = gridNowX;
                                exitRoomsGrid[roomList.Count - 1, 1] = gridNowY;
                                breakKode = true;
                          //      returningWalue = true;
                            }
                        }
                        break;


                    default: { break; }
                }

                breakLimit++;

                if (breakLimit > 25)
                {
                    breakKode = true;
                //    returningWalue = false;
                }
            }
            while (breakKode == false);
            // If return value 0 we cant spawn a room.
            // Debug.Log("Room is spawned="+ returningWalue);

        }

        // method for activate all doors that need to be closed
        void SpawnClosedDoors()
        {
            //Create a loop for all grid X and Y
            for (int i = 0; i < gridRange; i++)
            {

                for (int j = 0; j < gridRange; j++)
                {
                    // If this coordinates is not empty then.
                    if ((gridMap[i, j, 0]) != 0)
                    {
                        // We find what room is there.
                        GameObject lokingRoom = null;
                        for (int roomNubmer = 0; roomNubmer < numberRoomsNow; roomNubmer++)
                        {
                            if ((exitRoomsGrid[roomNubmer, 0] == i) && (exitRoomsGrid[roomNubmer, 1] == j))
                                lokingRoom = roomList[roomNubmer];
                        }
                        //if doors from up rooms a closed or empty room we close doors in this room
                        if ((gridMap[(i), (j - 1), 0]) == 0 || (RoomExits(gridMap[i, j - 1, 0])[2] == 0))
                        {
                            // IF room up empty then close rooms or room from up dont have door al all.
                            if (RoomExits(gridMap[i, j, 0])[0] == 1)
                            {
                                // If room have doors stats (if not prefab is broken)
                                if (lokingRoom.GetComponent<RoomDoorsSpawner>() != null)
                                    // Activate this doors 
                                    lokingRoom.GetComponent<RoomDoorsSpawner>().DoorsPrefab[0].SetActive(true);
                            }
                        }
                        // For right room.
                        if ((gridMap[(i + 1), (j), 0]) == 0 || (RoomExits(gridMap[i + 1, j, 0])[3] == 0))
                        {
                            if (RoomExits(gridMap[i, j, 0])[1] == 1)
                            {
                                if (lokingRoom.GetComponent<RoomDoorsSpawner>() != null)
                                    lokingRoom.GetComponent<RoomDoorsSpawner>().DoorsPrefab[1].SetActive(true);
                            }
                        }
                        // For down room.
                        if (((gridMap[i, (j + 1), 0]) == 0) || (RoomExits(gridMap[i, j + 1, 0])[0] == 0))
                        {
                            if (RoomExits(gridMap[i, j, 0])[2] == 1)
                            {
                                if (lokingRoom.GetComponent<RoomDoorsSpawner>() != null)
                                    lokingRoom.GetComponent<RoomDoorsSpawner>().DoorsPrefab[2].SetActive(true);
                            }
                        }
                        // For left room.
                        if (((gridMap[(i - 1), (j), 0]) == 0) || (RoomExits(gridMap[i - 1, j, 0])[1] == 0))
                        {
                            if ((RoomExits(gridMap[i, j, 0])[3] == 1))
                            {
                                if (lokingRoom.GetComponent<RoomDoorsSpawner>() != null)
                                    lokingRoom.GetComponent<RoomDoorsSpawner>().DoorsPrefab[3].SetActive(true);
                                else Debug.Log("weDOnt have rooms");
                            }
                        }
                    }
                }
            }
        }

        // A method that find all exit prefabs and remember it to global value.
        void FindAllExits()
        {
            // Find all exit to one array.
            // 0-room 1-4 exits all in array.
            for (int i = 0; i < roomList.Count; i++)
            {
                // Remember that room always is 1.
                int k = 1;
                exitRoomsArray[i, 0] = roomList[i];
                foreach (Transform child in roomList[i].transform)
                {
                    if (child.tag == "Exit")
                    {
                        // Write exit array with exit prefabs.
                        exitRoomsArray[i, k] = child.gameObject; k++;
                    }
                }
            }
        }

        // A method that spawn a tunnels between rooms.
        void BuildTunnels()
        {
            for (int i = 0; i < numberRoomsNow; i++)
            {

                //		Thread thre = new Thread (SpawnVerticalTunnels  );
                //		thre.Start(i);

                SpawnHorizontalTunnels(i);
                SpawnVerticalTunnels(i);
            }
            //Loop for all spawned rooms.

        }



        // A method for Spawn gradien FOG of war around rooms.
        void SpawnRoomFogOfWar()
        {

            for (int roomNow = 0; roomNow < numberRoomsNow; roomNow++)
            {
                float mostLeftPoint = exitRoomsArray[roomNow, 4].transform.position.x;
                float mostRightPoint = exitRoomsArray[roomNow, 2].transform.position.x;
                float mostUpPoint = exitRoomsArray[roomNow, 1].transform.position.z;
                float mostDownPoint = exitRoomsArray[roomNow, 3].transform.position.z;

                float distanseX = exitRoomsArray[roomNow, 4].transform.position.x - exitRoomsArray[roomNow, 2].transform.position.x;
                float distanseZ = exitRoomsArray[roomNow, 1].transform.position.z - exitRoomsArray[roomNow, 3].transform.position.z;
                Vector3 roomCord = new Vector3((mostLeftPoint + mostRightPoint) / 2, 11, (mostUpPoint + mostDownPoint) / 2);
                distanseX = Mathf.Abs(distanseX / 10) + 1;
                distanseZ = Mathf.Abs(distanseZ / 10) + 1;

                if (roomList[roomNow].GetComponent<RoomDoorsSpawner>().DoorActive == false)
                {
                    GameObject spawnedFogOfWar = Instantiate(fogPrefab[1], roomCord, transform.rotation * Quaternion.Euler(0, 0, 0));
                    spawnedFogOfWar.transform.localScale = new Vector3(distanseX / 5, 1f, distanseZ / 5);
                    roomList[roomNow].GetComponent<RoomDoorsSpawner>().fogOfWar.Add(spawnedFogOfWar);
                    spawnedFogOfWar.transform.SetParent(fogHolder);
                }


                for (int i = -1; i < distanseX + 1; i++)
                {
                    GameObject FogSpawner1 = Instantiate(fogPrefab[0], new Vector3(mostLeftPoint + (i * 10), 10.51f, mostUpPoint + 10), transform.rotation * Quaternion.Euler(90, 90, 0));
                    GameObject FogSpawner2 = Instantiate(fogPrefab[0], new Vector3(mostLeftPoint + (i * 10), 10.51f, mostDownPoint - 10), transform.rotation * Quaternion.Euler(90, 270, 0));
                    FogSpawner1.transform.SetParent(fogHolder); FogSpawner2.transform.SetParent(fogHolder);
                }

                for (int i = -1; i < distanseZ + 1; i++)
                {
                    GameObject FogSpawner1 = Instantiate(fogPrefab[0], new Vector3(mostLeftPoint - (10), 10.51f, mostDownPoint + (i * 10)), transform.rotation * Quaternion.Euler(90, 0, 0));
                    GameObject FogSpawner2 = Instantiate(fogPrefab[0], new Vector3(mostRightPoint + (10), 10.51f, mostDownPoint + (i * 10)), transform.rotation * Quaternion.Euler(90, 180, 0));
                    FogSpawner1.transform.SetParent(fogHolder); FogSpawner2.transform.SetParent(fogHolder);

                }
            }
        }
        void SpawnAFogForTunnels(Vector3 newFirstExitcoordinates, int roomNow, int roomNext)
        {
            GameObject instanceFog = Instantiate(fogPrefab[2], new Vector3(newFirstExitcoordinates.x, 11f, newFirstExitcoordinates.z), transform.rotation * Quaternion.Euler(90, 0, 0));
            instanceFog.transform.SetParent(fogHolder);
            roomList[roomNow].GetComponent<RoomDoorsSpawner>().fogOfWar.Add(instanceFog);
            roomList[roomNext].GetComponent<RoomDoorsSpawner>().fogOfWar.Add(instanceFog);
        }
        // void Update () {} plaсe for new method

        void SpawnHorizontalTunnels(object countI)
        {
            int i = (int)countI;
            for (int j = 0; j < numberRoomsNow; j++)
            {
                // Build tunennls from left to right
                // If this room have room from right and thay have door (where J left and i righ room)
                if ((roomList[i].GetComponent<StatasOfRoom>().thisRoomStats[3] == 1) && (roomList[j].GetComponent<StatasOfRoom>().thisRoomStats[1] == 1) && (exitRoomsGrid[i, 1] == (exitRoomsGrid[j, 1])) && ((exitRoomsGrid[i, 0]) == (exitRoomsGrid[j, 0] + 1)))
                {
                    // between I and J build tunel. Findcoordinates for both exits.
                    Vector3 newFirstExitcoordinates = exitRoomsArray[i, 2].transform.position;
                    Vector3 Range = exitRoomsArray[i, 4].transform.position - exitRoomsArray[j, 2].transform.position;
                    // Set step distanse for spaned tunlens parts.
                    float steepX = Range.x / 10 - 2;
                    float steepZ = Range.z / 10;
                    // multiplayer to change from grid tocoordinates values on unity engine.
                    int Multiplayer = (10);
                    // First buld For X Axis.
                    steepX++;
                    // For all parts on X line 
                    for (int X = 1; X < Mathf.Abs(steepX) + 1; X++)
                    {
                        // Getcoordinates of second Room.
                        newFirstExitcoordinates.x = exitRoomsArray[i, 4].transform.position.x - X * Multiplayer;
                        newFirstExitcoordinates.z = exitRoomsArray[i, 4].transform.position.z;
                        GameObject instance;
                        //If we cant fint right way spawn a normal coridor
                        if (X != Mathf.Abs(steepX))
                        {
                            // A spawn code.
                            instance = Instantiate(tunnellsPrefabs[0], newFirstExitcoordinates, transform.rotation * Quaternion.Euler(0, 90, 0));
                            instance.transform.SetParent(boardHolder);
                            //Spawn a fog of war for tunles;
                            SpawnAFogForTunnels(newFirstExitcoordinates, i, j);
                        }
                        // if Xcoordinates dont change. Last ones
                        if (X == Mathf.Abs(steepX) && steepZ == 0)
                        {
                            if (Mathf.Abs(steepX) == 1) instance = Instantiate(tunnellsPrefabs[0], newFirstExitcoordinates, transform.rotation * Quaternion.Euler(0, 90, 0));
                            else instance = Instantiate(tunnellsPrefabs[1], newFirstExitcoordinates, transform.rotation * Quaternion.Euler(0, 90, 0));
                            instance.transform.SetParent(boardHolder);
                            SpawnAFogForTunnels(newFirstExitcoordinates, i, j);
                        }
                        // if its first room we spawn a first room.
                        if (X == Mathf.Abs(steepX) && steepZ < 0)
                        {
                            instance = Instantiate(tunnellsPrefabs[5], newFirstExitcoordinates, transform.rotation * Quaternion.Euler(0, 180, 0));
                            instance.transform.SetParent(boardHolder);
                            SpawnAFogForTunnels(newFirstExitcoordinates, i, j); ;

                        }
                        // if its last room we spawn a last room
                        if (X == Mathf.Abs(steepX) && steepZ > 0)
                        {
                            instance = Instantiate(tunnellsPrefabs[4], newFirstExitcoordinates, transform.rotation * Quaternion.Euler(0, 270, 0));
                            instance.transform.SetParent(boardHolder);
                            SpawnAFogForTunnels(newFirstExitcoordinates, i, j);

                        }
                    }
                    // when we dont with X we build tunel in Ycoordinates  , where we build a tunel for first posible wariation last and all other

                    Multiplayer = (-10);
                    // If we worh with negativecoordinates we change it.
                    if (steepZ > 0)
                        Multiplayer = Multiplayer * -1;
                    // Now we do the same for Ycoordinates when we build From right to left room.
                    for (int Z = 1; Z < Mathf.Abs(steepZ) + 1; Z++)
                    {
                        // All code the same But its Y coordinate But unity engine see it as Z
                        newFirstExitcoordinates.z = exitRoomsArray[i, 4].transform.position.z - Z * Multiplayer;
                        if (Z != Mathf.Abs(steepZ))
                        {
                            GameObject instance = Instantiate(tunnellsPrefabs[2], newFirstExitcoordinates, transform.rotation * Quaternion.Euler(0, 0, 0));
                            instance.transform.SetParent(boardHolder);
                            SpawnAFogForTunnels(newFirstExitcoordinates, i, j);
                        }
                        if (Z == Mathf.Abs(steepZ) && steepX == 0)
                        {
                            GameObject instance = Instantiate(tunnellsPrefabs[2], newFirstExitcoordinates, transform.rotation * Quaternion.Euler(0, 0, 0));
                            instance.transform.SetParent(boardHolder);
                            SpawnAFogForTunnels(newFirstExitcoordinates, i, j);
                        }
                        if (Z == Mathf.Abs(steepZ) && steepZ < 0)
                        {
                            GameObject instance = Instantiate(tunnellsPrefabs[5], newFirstExitcoordinates, transform.rotation * Quaternion.Euler(0, 0, 0));
                            instance.transform.SetParent(boardHolder);
                            SpawnAFogForTunnels(newFirstExitcoordinates, i, j);
                        }
                        if (Z == Mathf.Abs(steepZ) && steepZ > 0)
                        {
                            GameObject instance = Instantiate(tunnellsPrefabs[4], newFirstExitcoordinates, transform.rotation * Quaternion.Euler(0, 90, 0));
                            instance.transform.SetParent(boardHolder);
                            SpawnAFogForTunnels(newFirstExitcoordinates, i, j);
                        }
                    }

                }

            }
        }
        void SpawnVerticalTunnels(object countI)
        {
            int i = (int)countI;
            for (int j = 0; j < numberRoomsNow; j++)
            {

                // Now We build Tunles from UP to Down between rooms ALL code hold same but change only X and Y
                // If this room have room from right and thay have door (where J UP and i Down room)
                if ((roomList[i].GetComponent<StatasOfRoom>().thisRoomStats[0] == 1) && (roomList[j].GetComponent<StatasOfRoom>().thisRoomStats[2] == 1) && (exitRoomsGrid[i, 0] == exitRoomsGrid[j, 0]) && (exitRoomsGrid[i, 1] == (exitRoomsGrid[j, 1] + 1)))
                {
                    Vector3 newFirstExitcoordinates = exitRoomsArray[i, 1].transform.position;
                    Vector3 Range = exitRoomsArray[i, 1].transform.position - exitRoomsArray[j, 3].transform.position;
                    GameObject instance;
                    float steepX = Range.x / 10;
                    float steepZ = Range.z / 10;
                    int Multiplayer = (-10);
                    steepZ++;
                    for (int Z = 1; Z < Mathf.Abs(steepZ) + 1; Z++)
                    {
                        newFirstExitcoordinates.z = exitRoomsArray[i, 1].transform.position.z - Z * Multiplayer;
                        newFirstExitcoordinates.x = exitRoomsArray[i, 1].transform.position.x;
                        if (Z != Mathf.Abs(steepZ))
                        {
                            instance = Instantiate(tunnellsPrefabs[0], newFirstExitcoordinates, transform.rotation * Quaternion.Euler(0, 0, 0));
                            instance.transform.SetParent(boardHolder); SpawnAFogForTunnels(newFirstExitcoordinates, i, j);
                        }
                        if (Z == Mathf.Abs(steepZ) && steepX == 0)
                        {
                            if (Mathf.Abs(steepZ) == 1) instance = Instantiate(tunnellsPrefabs[0], newFirstExitcoordinates, transform.rotation * Quaternion.Euler(0, 180, 0));
                            else instance = Instantiate(tunnellsPrefabs[1], newFirstExitcoordinates, transform.rotation * Quaternion.Euler(0, 180, 0));
                            instance.transform.SetParent(boardHolder); SpawnAFogForTunnels(newFirstExitcoordinates, i, j);
                        }
                        if (Z == Mathf.Abs(steepZ) && steepX < 0)
                        {
                            instance = Instantiate(tunnellsPrefabs[5], newFirstExitcoordinates, transform.rotation * Quaternion.Euler(0, 270, 0));
                            instance.transform.SetParent(boardHolder); SpawnAFogForTunnels(newFirstExitcoordinates, i, j);
                        }
                        if (Z == Mathf.Abs(steepZ) && steepX > 0)
                        {
                            instance = Instantiate(tunnellsPrefabs[4], newFirstExitcoordinates, transform.rotation * Quaternion.Euler(0, 0, 0));
                            instance.transform.SetParent(boardHolder); SpawnAFogForTunnels(newFirstExitcoordinates, i, j);
                        }
                    }

                    Multiplayer = 10;
                    if (steepX < 0) Multiplayer = Multiplayer * -1;
                    for (int X = 1; X < Mathf.Abs(steepX) + 1; X++)
                    {
                        newFirstExitcoordinates.x = exitRoomsArray[i, 1].transform.position.x - X * Multiplayer;
                        if (X != Mathf.Abs(steepX))
                        {
                            instance = Instantiate(tunnellsPrefabs[2], newFirstExitcoordinates, transform.rotation * Quaternion.Euler(0, 90, 0));
                            instance.transform.SetParent(boardHolder); SpawnAFogForTunnels(newFirstExitcoordinates, i, j);
                        }
                        if (X == Mathf.Abs(steepX) && steepZ == 0)
                        {
                            instance = Instantiate(tunnellsPrefabs[2], newFirstExitcoordinates, transform.rotation * Quaternion.Euler(0, 90, 0));
                            instance.transform.SetParent(boardHolder); SpawnAFogForTunnels(newFirstExitcoordinates, i, j);
                        }
                        if (X == Mathf.Abs(steepX) && steepX < 0)
                        {
                            instance = Instantiate(tunnellsPrefabs[5], newFirstExitcoordinates, transform.rotation * Quaternion.Euler(0, 90, 0));
                            instance.transform.SetParent(boardHolder); SpawnAFogForTunnels(newFirstExitcoordinates, i, j);
                        }
                        if (X == Mathf.Abs(steepX) && steepX > 0)
                        {
                            instance = Instantiate(tunnellsPrefabs[4], newFirstExitcoordinates, transform.rotation * Quaternion.Euler(0, 180, 0));
                            instance.transform.SetParent(boardHolder); SpawnAFogForTunnels(newFirstExitcoordinates, i, j);
                        }
                    }
                }
            }
        }

    }
}