using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/InputActions/Examine")]

public class Examine : InputAction
{
    public override void RespondToInput(GameController gameController, string[] separatedInputWords)
    {
        gameController.LogStringWithReturn(gameController.TestVerbDictionaryWithNoun(gameController.interactableItems.examineDictionary, separatedInputWords[0], separatedInputWords[1]));

    }
}
