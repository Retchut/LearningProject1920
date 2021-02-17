using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/ActionResponses/ChangeRoom")]
public class ChangeRoomResponse : ActionResponse
{
    public Room destinationRoom;

    public override bool DoActionResponse(GameController gameController)
    {
        if (gameController.roomNavigation.currentRoom.roomName == requiredString)
        {
            gameController.roomNavigation.currentRoom = destinationRoom;
            gameController.DisplayRoomText();
            return true;
        }
        else
        {
            return false;
        }
    }


}
