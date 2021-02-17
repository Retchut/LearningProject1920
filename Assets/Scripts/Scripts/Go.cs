using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/InputActions/Go")]

public class Go : InputAction                                                                           // inherits from InputAction, has keyword variable
{                                                                                                       
    public override void RespondToInput(GameController gameController, string[] separatedInputWords)    // now needs to override the original function, with
    {
        gameController.roomNavigation.AttemptToChangeRooms(separatedInputWords[1]);                     // calls AttemptToChangeRooms in roomNavigation, with
    }                                                                                                   // the 2nd word being the room keyword
}
