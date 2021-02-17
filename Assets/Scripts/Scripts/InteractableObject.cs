using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/Interactable Object")]

public class InteractableObject : ScriptableObject
{
    public string objectNoun = "name";                                         // define object name variable
    [TextArea]
    public string objectDescription = "Description in room";                   // define object description variable

    public Interaction[] interactions;
}
