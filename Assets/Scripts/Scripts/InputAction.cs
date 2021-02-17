using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InputAction : ScriptableObject
{
    public string keyword;                                                                                      // input to respond to

    public abstract void RespondToInput (GameController gameController, string[] separatedInputWords);          /* call function to respond to input keyword,
                                                                                                                with parameters being the gameController script,
                                                                                                                and an array of strings, made up of separated input
                                                                                                                words*/
}
