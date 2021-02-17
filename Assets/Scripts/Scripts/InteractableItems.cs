using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableItems : MonoBehaviour
{
    public List<InteractableObject> usableItemList;

    public Dictionary<string, string> examineDictionary = new Dictionary<string, string>();
    public Dictionary<string, string> takeDictionary = new Dictionary<string, string>();

    [HideInInspector] public List<string> nounsInRoom = new List<string>();

    Dictionary<string, ActionResponse> useDictionary = new Dictionary<string, ActionResponse>();
    List<string> nounsInInventory = new List<string>();
    GameController gameController;

    private void Awake()
    {
        gameController = GetComponent<GameController>();
    }


    public string GetObjectsNotInInventory(Room currentRoom, int i)                 // function to get objects not in inventory
    {
        InteractableObject interactableInRoom = currentRoom.interactableObjectsInRoom[i];       // get objects in room array, position i

        if (!nounsInInventory.Contains(interactableInRoom.objectNoun))
        {
            nounsInRoom.Add(interactableInRoom.objectNoun);
            return interactableInRoom.objectDescription;
        }
        return null;

    }

    public void AddActionResponsesToUseDictionary()                                                 // updates when you take an item
    {
        for (int i = 0; i < nounsInInventory.Count; i++)
        {
            string objectNoun = nounsInInventory[i];

            InteractableObject interactableObjectInInventory = GetInteractableObjectFromUsableList(objectNoun);
            if (interactableObjectInInventory == null)
                continue;
            for (int j = 0; j<interactableObjectInInventory.interactions.Length; j++)
            {
                Interaction interaction = interactableObjectInInventory.interactions[j];

                if (interaction.actionResponse == null)
                    continue;

                if (!useDictionary.ContainsKey(objectNoun))
                {
                    useDictionary.Add(objectNoun, interaction.actionResponse);
                }
            }
        }
    }

    InteractableObject GetInteractableObjectFromUsableList(string objectNoun)
    {
        for (int i = 0; i < usableItemList.Count; i++)
        {
            if (usableItemList[i].objectNoun == objectNoun)
            {
                return usableItemList[i];
            }
        }
        return null;
    }

    public void DisplayInventory()
    {
        gameController.LogStringWithReturn("You look in your backpack. Inside it you find:");

        if (nounsInInventory.Count > 0)
        {
            for (int i = 0; i < nounsInInventory.Count; i++)
            {
                gameController.LogStringWithReturn(nounsInInventory[i]);
            }
        }
        else
        {
            gameController.LogStringWithReturn("nothing.");
        }
    }

    public void ClearCollections()
    {
        examineDictionary.Clear();
        takeDictionary.Clear();
        nounsInRoom.Clear();
    }

    public Dictionary<string,string> Take(string[] separatedInputWords)
    {
        string objectNoun = separatedInputWords[1];

        if (nounsInRoom.Contains(objectNoun))
        {
            nounsInInventory.Add(objectNoun);
            AddActionResponsesToUseDictionary();
            nounsInRoom.Remove(objectNoun);
            return takeDictionary;
        }
        else
        {
            gameController.LogStringWithReturn("There is no " + objectNoun + " here.");
            return null;
        }
    }

    public void UseItem(string[] separatedInputWords)
    {
        string nounToUse = separatedInputWords[1];

        if (nounsInInventory.Contains(nounToUse))
        {
            if (useDictionary.ContainsKey(nounToUse))
            {
                bool actionResult = useDictionary[nounToUse].DoActionResponse(gameController);
                if (!actionResult)
                {
                    gameController.LogStringWithReturn("Nothing happened. Maybe you should try somewhere else.");
                }
            }
            else
            {
                gameController.LogStringWithReturn("You can't use the " + nounToUse);
            }
        }
        else
        {
            gameController.LogStringWithReturn("There is no " + nounToUse + " in your inventory.");
        }
    }
}
