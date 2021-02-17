
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/InputActions/Take")]

public class Take : InputAction
{
    public override void RespondToInput(GameController gameController, string[] separatedInputWords)
    {
        Dictionary<string, string> takeDictionary = gameController.interactableItems.Take(separatedInputWords);

        if (takeDictionary != null)
        {
            gameController.LogStringWithReturn(gameController.TestVerbDictionaryWithNoun(takeDictionary, separatedInputWords[0], separatedInputWords[1]));
        }
    }
}
