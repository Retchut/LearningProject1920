using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public Text displayText;                                                                                        // create empty text to be displayed
    public InputAction[] inputActions;                                                                              // create empty array of input actions

    [HideInInspector] public RoomNavigation roomNavigation;                                                         // creates roomNavigation variable
    [HideInInspector] public List<string> interactionDescriptionsInRoom = new List<string>();                       // creates room interactions' descriptions' list
    [HideInInspector] public InteractableItems interactableItems;                                                   // 
    

    List<string> actionLog = new List<string>();                                                                    // creates action log string

    void Awake()
    {
        interactableItems = GetComponent<InteractableItems>();                                                      // fetches InteractableItems script
        roomNavigation = GetComponent<RoomNavigation>();                                                            // fetches RoomNavigation script
    }

    void Start()
    {
        DisplayRoomText();                                                                                          // calls function to display room text
        DisplayLoggedText();                                                                                        // calls function to display text on the log
    }



    public void DisplayLoggedText()                                                                     // function to display the text in the log
    {
        string logAsText = string.Join("\n", actionLog.ToArray());                                              /* defines logListAsText, which is a new string that holds
                                                                                                                       the action log, with each element on a new line*/
        displayText.text = logAsText;                                                                           //changes display text to logListAsText
    }

    public void DisplayRoomText()                                                                       // function to display room text
    {
        ClearForNewRoom();                                                                                          // calls function to clear lists before changing room

        UnpackRoom();                                                                                               // calls function to build room list

        string joinedInteractionDescriptions = string.Join("\n", interactionDescriptionsInRoom.ToArray());          /* makes string from the array of all interactions'
                                                                                                                        descriptions, each on a new line*/

        string combinedText = roomNavigation.currentRoom.roomDescription + "\n" + joinedInteractionDescriptions;    /* combines room description with joined interactiondescriptions*/

        LogStringWithReturn(combinedText);
    }

    void UnpackRoom() // function to build room list on editor
    {
        roomNavigation.UnpackExitsInRoom();                                                                         // gets function on roomNavigation to build room list
        PrepareObjectsToTakeOrExamine(roomNavigation.currentRoom);

    }

    void PrepareObjectsToTakeOrExamine(Room currentRoom)
    {
        for (int i = 0; i < currentRoom.interactableObjectsInRoom.Length; i++)
        {
            string descriptionNotInInventory = interactableItems.GetObjectsNotInInventory(currentRoom, i);
            
            if(descriptionNotInInventory != null)
            {
                interactionDescriptionsInRoom.Add(descriptionNotInInventory); 
            }

            InteractableObject interactableInRoom = currentRoom.interactableObjectsInRoom[i]; 

            for (int j = 0; j < interactableInRoom.interactions.Length; j++)
            {
                Interaction interaction = interactableInRoom.interactions[j];

                if (interaction.inputAction.keyword == "examine")
                {
                    interactableItems.examineDictionary.Add(interactableInRoom.objectNoun, interaction.textResponse);
                }

                if (interaction.inputAction.keyword == "take")
                {
                    interactableItems.takeDictionary.Add(interactableInRoom.objectNoun, interaction.textResponse);
                }
            }
        }
    }

    public string TestVerbDictionaryWithNoun(Dictionary<string,string> verbDictionary, string verb, string objectNoun)
    {
        if (verbDictionary.ContainsKey(objectNoun))
        {
            return verbDictionary[objectNoun];
        }

        return "You can't " + verb + " " + objectNoun;
    }

    void ClearForNewRoom()                                                                              // function to clear everything when switching rooms
    {
        interactableItems.ClearCollections();
        interactionDescriptionsInRoom.Clear();                                                                      // clear description list
        roomNavigation.ClearExits();                                                                                // call function to clear exits dictionary
    }


    public void LogStringWithReturn(string stringToAdd)                                                 // function to add strings to log
    {
        actionLog.Add(stringToAdd + "\n");                                                                          // adds string to log on a new line
    }
}
