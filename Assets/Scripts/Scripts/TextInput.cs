using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextInput : MonoBehaviour
{
    public InputField inputField;                                                           // creates inputField variable

    GameController gameController;                                                              // creates GameController variable


    void Awake()
    {
        gameController = GetComponent<GameController>();                                        // assigns controller to GameController script
        inputField.onEndEdit.AddListener(AcceptStringInput);                                // assigns inputField to inputfield
    }

    void AcceptStringInput(string userInput)                                        // function to receive user input
    {
        userInput = userInput.ToLower();                                                        // turns input into lower case
        gameController.LogStringWithReturn(userInput);                                          // calls LogStringWithReturn function in controller, and applies it to userInput

        char[] delimiterCharacters = { ' ' };                                                   // creates array of delimiter character, with a space being a delimiter character
        string[] separatedInputWords = userInput.Split(delimiterCharacters);                    // separates characters in string, according to spaces

        for(int i = 0; i < gameController.inputActions.Length; i++)                             // go over the input actions array
        {
            InputAction inputAction = gameController.inputActions[i];                               // gets inputActions script
            if (inputAction.keyword == separatedInputWords[0])                                      // if the action inserted is the first word
            {
                inputAction.RespondToInput(gameController, separatedInputWords);                        // call RespondToInput, with gamecontroller script,
            }                                                                                           // and separated words as parameters
        }

        InputComplete();                                                                    // calls InputComplete function
    }

    void InputComplete()                                                        // function to print input
    {
        gameController.DisplayLoggedText();                                                     // calls DisplayLoggedText from controller
        inputField.ActivateInputField();                                                    // activates input field
        inputField.text = null;                                                             // removes inputfield text
    }
}
