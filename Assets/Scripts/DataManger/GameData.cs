using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * All data that needs to be save should be stored within this class. All subdata class 
 * for example CharacterData needs to be marked with [System.Serializable].
 */
[System.Serializable]
public class GameData
{
    [HideInInspector] public List<CharacterData> Characters;

    /*
     * Default values for a new game
     */
    public GameData()
    {
        Characters = new();
    }
}
