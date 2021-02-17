using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomNavigation : MonoBehaviour
{
    public Room currentRoom;                                                                                    // holds current room (initially start room), and its script

    Dictionary<string, Room> exitRoomsDictionary = new Dictionary<string, Room>();                              // creates room dictionary

    GameController gameController;                                                                      // creates gameController variable

    void Awake()
    {
        gameController = GetComponent<GameController>();                                                        // gets GameController script
    }

    public void UnpackExitsInRoom()                                                                 // function to build room list on inspector
    {
        for (int i = 0; i < currentRoom.exits.Length; i++)                                                      // gets lengtj of the exits array, from the Exits script
        {
            exitRoomsDictionary.Add(currentRoom.exits[i].keyString, currentRoom.exits[i].valueRoom);            // adds exits to dictionary

            gameController.interactionDescriptionsInRoom.Add(currentRoom.exits[i].exitDescription);             /*adds to the interactionDescriptionsInRoom list new 
                                                                                                                interactions, one for each exit on the room*/
        }
    }

    public void AttemptToChangeRooms(string directionNoun)                                          // script to change room
    {
        if (exitRoomsDictionary.ContainsKey(directionNoun))                                                     // check if the dictionary contains typed key
        {

            currentRoom = exitRoomsDictionary[directionNoun];                                                   // changes current room to new room
            gameController.LogStringWithReturn("You head off to the " + directionNoun);                         // prints movement to the key's value

            gameController.DisplayRoomText();                                                                   // calls text display function

        }
        else
        {
            gameController.LogStringWithReturn("There is no path to the " + directionNoun);                     // if the key doesn't exist, return this sentence
        }
    }

    public void ClearExits()                                                                        // function to reset dictionary after changing room
    {
        exitRoomsDictionary.Clear();
    }
}
